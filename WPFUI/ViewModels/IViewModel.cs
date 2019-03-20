using System.Collections.ObjectModel;
using System.Windows.Input;
using DataLibrary.Models;
using DataLibrary.Utility;

namespace WPFUI.ViewModels
{
    public interface IViewModel<T> where T:IModel<T>
    {
        ObservableCollection<T> SourceModels { get; set; }
        T SelectedModel { get; set; }
        T DirtySelection { get; set; }

        bool IsEditMode { get; }
        bool IsReadOnly { get; set; }

        ICommand AddCommand { get; }
        ICommand FirstCommand { get; }
        ICommand LastCommand { get; }
        ICommand NextCommand { get; }
        ICommand PreviousCommand { get; }
        ICommand SaveCommand { get; }
        ICommand CancelCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand EditCommand { get; }
        

    }
}