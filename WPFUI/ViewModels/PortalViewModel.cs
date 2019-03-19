using System.Windows.Input;
using WPFUI.Utility;

namespace WPFUI.ViewModels
{
    public class PortalViewModel: ObservableObject
	{
		public ICommand LoadMembersCommand { get; set; }
		public ICommand LoadVisitsCommand { get; set; }
        public ICommand LoadEventsCommand { get; set; }
        public ICommand LoadDeliveriesCommand { get; set; }

        public PortalViewModel()
		{
			
		}
		
	}
}
