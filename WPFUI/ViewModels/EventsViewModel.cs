using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class EventsViewModel : ObservableObject
    {

        private ObservableCollection<EventSpecModel> _sourcemodels;
        private EventSpecModel _selectedModel;
        private EventSpecModel dirtySelection;

        public EventSpecModel SelectedModel { get => _selectedModel; set => _selectedModel = value; }

        public ObservableCollection<EventSpecModel> SourceModels
        {
            get { return _sourcemodels; }
            set { _sourcemodels = value; }
        }

        public bool IsEditMode { get => !IsReadOnly; }

        public bool IsReadOnly { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand AddCommand;
        public ICommand FirstCommand;
        public ICommand LastCommand;
        public ICommand NextCommand;
        public ICommand PreviousCommand ;
        public ICommand SaveCommand;
        public ICommand CancelCommand ;
        public ICommand DeleteCommand;
        public ICommand EditCommand;

        public EventsViewModel()
        {
            EventConnector conn = new EventConnector();
            SourceModels = new ObservableCollection<EventSpecModel>( conn.Load(null, true));
            LastCommand = new RelayCommand(Last, () => IsReadOnly);
            FirstCommand = new RelayCommand(First, () => IsReadOnly);
            PreviousCommand = new RelayCommand(Previous, () => IsReadOnly);
            NextCommand = new RelayCommand(Next, () => IsReadOnly);
            AddCommand = new RelayCommand(Add, () => IsReadOnly);
            SaveCommand = new RelayCommand(Save, () => IsEditMode);
            EditCommand = new RelayCommand(Edit, () => IsReadOnly);
            DeleteCommand = new RelayCommand(Delete, () => IsReadOnly);
            CancelCommand = new RelayCommand(Cancel, () => IsEditMode);

        }
        #region Navigation Bar Methods
        private void First()
        {
            EventSpecModel temp = SourceModels.FirstOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Last()
        {
            EventSpecModel temp = SourceModels.LastOrDefault();
            if (temp != null) SelectedModel = temp;
        }
        private void Previous()
        {
            EventSpecModel temp = SourceModels.Where((model) => model.ID < SelectedModel.ID).LastOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Next()
        {
            EventSpecModel temp = SourceModels.Where((model) => model.ID > SelectedModel.ID).FirstOrDefault();
            if (temp != null) SelectedModel = temp;

        }
        private void Add()
        {
            SourceModels.Add(new EventSpecModel());
            SelectedModel = SourceModels.Last();
            IsReadOnly = false;
        }
        private void Edit()
        {
            IsReadOnly = false;
            dirtySelection = SelectedModel?.Clone(); //not yet a deep clone, test  
        }
        private void Save()
        {
            //implement validation checks before saving
            try
            {
                EventConnector vconn = new EventConnector();
                if (SelectedModel?.ID == 0)
                {
                    SelectedModel.ID = vconn.Insert(SelectedModel);
                }
                else { vconn.Update(SelectedModel); }
                if (vconn.Ex == null)
                {
                    MessageBox.Show("Visit Added Successfully");
                }
                else { MessageBox.Show(vconn.Ex.Message); }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dirtySelection = null;
                IsReadOnly = true;
            }


        }
        private void Delete()
        {
            if (SelectedModel != null)
            {
                if (SelectedModel.ID != 0)
                {
                    EventConnector vconn = new EventConnector();
                    vconn.Delete(SelectedModel);
                    if (vconn.Ex == null) { MessageBox.Show("Event Deleted Successfully"); }
                    else { MessageBox.Show(vconn.Ex.Message); }
                }
                SourceModels.Remove(SelectedModel);
                SelectedModel = SourceModels.LastOrDefault();
            }

        }

        private void Cancel()
        {
            if (SelectedModel != null)
            {
                //Cancel new visit
                if (SelectedModel.ID == 0)
                {
                    SourceModels.Remove(SourceModels.Last());
                    SelectedModel = SourceModels.LastOrDefault();
                }
                //Cancel Edit Visit
                else if (dirtySelection != null)
                {
                    SourceModels[GetIndex(SelectedModel.ID)] = dirtySelection;
                    dirtySelection = null;
                }
            }
            IsReadOnly = true;
        }
        #endregion
        private int GetIndex(int? ID)
        {
            EventSpecModel temp = SourceModels.Where((model) => model.ID == ID).FirstOrDefault();
            return SourceModels.IndexOf(temp);
        }
    }
}
