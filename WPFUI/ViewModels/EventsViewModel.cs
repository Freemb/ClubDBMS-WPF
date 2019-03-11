using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class EventsViewModel:ObservableObject
    {

        private List<EventSpecModel> _sourcemodels;

        public List<EventSpecModel> SourceModels
        {
            get { return _sourcemodels; }
            set { _sourcemodels = value; }
        }

        public EventsViewModel()
        {
            EventConnector conn = new EventConnector();
            SourceModels = conn.Load(null, true);
        }

    }
}
