using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using DataLibrary.Operations;
using WPFUI.ViewModels;

namespace WPFUI.Views
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : Window
	{
		private ShellViewModel _shell = ShellViewModel.GetInstance; //check this line
		

		public ShellView()
		{
			InitializeComponent();
			DataContext = _shell; // can set here or in xaml
		}		
	}

}
