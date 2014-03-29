using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication2
{
	public partial class previewControl : UserControl
	{
		public previewControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
            var tempObj = App.Current as App;
            textdataItems jk = new textdataItems() { titleText = "Create a new Section to preview", imgLink = new System.Uri("Assets/news.png", UriKind.Relative) };
            tempObj.Items.Add(jk);
            list_items.ItemsSource = tempObj.Items;
		}
	}
}