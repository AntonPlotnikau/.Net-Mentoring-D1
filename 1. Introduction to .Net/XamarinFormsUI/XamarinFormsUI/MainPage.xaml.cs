using BLL;
using System;
using Xamarin.Forms;

namespace XamarinFormsUI
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void OnButtonClicked(object sender, EventArgs e)
		{
			string name = this.helloEntry.Text;
			this.helloLabel.Text = HelloService.SayHello(DateTime.Now, name);
		}
	}
}
