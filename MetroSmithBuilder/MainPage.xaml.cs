using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication2
{
	public partial class MainPage : UserControl
	{
		string currentState = "content";
       
		public MainPage()
		{
			InitializeComponent();
            
		}

		private void contentLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			SolidColorBrush tempBrush = new SolidColorBrush();
			tempBrush.Color = Colors.Black;
			contentLabel.Foreground =  tempBrush;
			currentState = "content";
			
			tempBrush.Color = Colors.LightGray;
			designLabel.Foreground = tempBrush;
		}

		private void contentLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SolidColorBrush tempBrush = new SolidColorBrush();
			tempBrush.Color = Colors.Blue;
			contentLabel.Foreground =  tempBrush;
			
		}

		private void contentLabel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (currentState == "content")
			{
				SolidColorBrush tempBrush = new SolidColorBrush();
				tempBrush.Color = Colors.Black;
				contentLabel.Foreground = tempBrush;
			}
			else
			{
				SolidColorBrush tempBrush = new SolidColorBrush();
				tempBrush.Color = Colors.LightGray;
				contentLabel.Foreground = tempBrush;
			}
		}

		private void designLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SolidColorBrush tempBrush = new SolidColorBrush();
			tempBrush.Color = Colors.Blue;
			designLabel.Foreground =  tempBrush;
		}

		private void designLabel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (currentState == "design")
			{
				SolidColorBrush tempBrush = new SolidColorBrush();
				tempBrush.Color = Colors.Black;
				designLabel.Foreground = tempBrush;
			}
			else
			{
				SolidColorBrush tempBrush = new SolidColorBrush();
				tempBrush.Color = Colors.LightGray;
				designLabel.Foreground = tempBrush;
			}
		}

		private void designLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			SolidColorBrush tempBrush = new SolidColorBrush();
			tempBrush.Color = Colors.Black;
			designLabel.Foreground =  tempBrush;
			currentState = "design";
			
			tempBrush.Color = Colors.LightGray;
			contentLabel.Foreground = tempBrush;
		}
	}
	
	
}