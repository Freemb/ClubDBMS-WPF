using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUI.ViewModels
{
    public class MembersPortalViewModel
    {
        public VisitsViewModel VisVM { get; private set; }
        public MembersViewModel MemVM { get; private set; }

        public ICommand LoadMembersCommand { get; set; }
        public ICommand LoadVisitsCommand { get; set; }

        

        public MembersPortalViewModel()
        {
            MemVM = new MembersViewModel();
            VisVM = new VisitsViewModel();
        }

    }
}
