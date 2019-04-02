using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUI.ViewModels
{
    public class EventsPortalViewModel
    {
        
        public EventsViewModel EventVM { get; private set; }
        public EventBookingsViewModel BookVM { get; private set; }

        public ICommand LoadEventsCommand { get; set; }
        public ICommand LoadBookingsCommand { get; set; }

        public EventsPortalViewModel()
        {
            
            EventVM = new EventsViewModel();
            BookVM = new EventBookingsViewModel();
            
        }
    }
}
