﻿using Sdl.Community.SdlTmAnonymizer.ViewModel;

namespace Sdl.Community.SdlTmAnonymizer.Ui
{
	/// <summary>
	/// Interaction logic for AcceptWindow.xaml
	/// </summary>
	public partial class AcceptWindow 
	{
		public AcceptWindow()
		{
			InitializeComponent();
			DataContext = new AcceptWindowViewModel();
		}
	}
}
