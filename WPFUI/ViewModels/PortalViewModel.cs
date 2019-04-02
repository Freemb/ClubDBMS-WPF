using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class PortalViewModel: ObservableObject
	{
        public MembersPortalViewModel MemPortalVM { get; private set; }
        public EventsPortalViewModel EventPortalVM { get; private set; }
        public DeliveriesViewModel DelVM { get; private set; }

        public ICommand LoadMembersPortalCommand { get; set; }
		public ICommand LoadEventsPortalCommand { get; set; }
        public ICommand LoadDeliveriesCommand { get; set; }

        public PortalViewModel()
        {

            MemPortalVM = new MembersPortalViewModel();
            EventPortalVM = new EventsPortalViewModel();
            DelVM = new DeliveriesViewModel();
        }
		
	}
}
