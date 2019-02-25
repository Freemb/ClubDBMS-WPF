using WPFUI.Views;
using DataLibrary.Models;
using DataLibrary.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFUI.Utility;
using System.Windows.Input;

namespace WPFUI.ViewModels
{
	public class PortalViewModel: ObservableObject
	{
		public ShellViewModel ShellDerived { get; private set; }
		public PortalViewModel()
		{

		}
		public PortalViewModel(ShellViewModel shellderived)
		{
			ShellDerived = shellderived;
		}
		


	}
}
