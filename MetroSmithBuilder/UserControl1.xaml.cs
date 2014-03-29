using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net;

namespace SilverlightApplication2
{
	public partial class UserControl1 : UserControl
	{
		string imgSourceTag = string.Empty;
        System.Windows.Threading.DispatcherTimer myDispatcherTimer;
        int timCount = 0;
        int currentSelectionNum = -1;
        string selectedContentType;

		public UserControl1()
		{
			// Required to initialize variables
			InitializeComponent();
		}


		private void tLoader_clickedAnImage(object sender, System.EventArgs e)
		{
			if (tLoader.itemSelected == "rss")
			{
                feedDetailsGrid.Visibility = System.Windows.Visibility.Visible;
                img_type.Source = new BitmapImage(new Uri("Assets/news.png",UriKind.Relative));
				imgSourceTag = "Assets/news.png";
                selectedContentType = "rss";
			}
            else if (tLoader.itemSelected == "twitter")
            {
                feedDetailsGrid.Visibility = System.Windows.Visibility.Visible;
                img_type.Source = new BitmapImage(new Uri("Assets/twitter.png",UriKind.Relative));
				imgSourceTag = "Assets/twitter.png";
                selectedContentType = "twitter";
            }
            else if (tLoader.itemSelected == "fb")
            {
                feedDetailsGrid.Visibility = System.Windows.Visibility.Visible;
                img_type.Source = new BitmapImage(new Uri("Assets/facebook.png",UriKind.Relative));
				imgSourceTag = "Assets/facebook.png";
                selectedContentType = "facebook";
            }
            else if (tLoader.itemSelected == "morenews")
            {
                feedDetailsGrid.Visibility = System.Windows.Visibility.Visible;
                img_type.Source = new BitmapImage(new Uri("Assets/more.png",UriKind.Relative));
				imgSourceTag = "Assets/more.png";
                selectedContentType = "morenews";
            }
		}

		private void link_create_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var tempObj = App.Current as App;

            if ((text_sectionName.Text != string.Empty) && (text_sectionFeedUrl.Text != string.Empty))
            {
                sectionElementsDM kl = new sectionElementsDM();
                kl.sectionName = text_sectionName.Text;
                kl.feedurl = text_sectionFeedUrl.Text;
                kl.imgName = imgSourceTag;
                kl.sectionType = selectedContentType;

                tempObj.sectionElements.Add(kl);
                //textfeedParser mParser = new textfeedParser(text_sectionFeedUrl.Text);

                genPopup.IsOpen = true;
                lbox.Visibility = System.Windows.Visibility.Visible;
                stackButtons.Visibility = System.Windows.Visibility.Collapsed;

                popMsg.Text = "New section created. What would you like to do next ?";

                text_sectionName.Text = string.Empty;
                text_sectionFeedUrl.Text = string.Empty;
                feedDetailsGrid.Visibility = System.Windows.Visibility.Collapsed;


            }
            else
            {
                genPopup.IsOpen = true;
                popMsg.Text = "Snap ! Please enter all details to create a section.";

                lbox.Visibility = System.Windows.Visibility.Collapsed;
                stackButtons.Visibility = System.Windows.Visibility.Collapsed;

                 myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                 myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); // 100 Milliseconds 
                 myDispatcherTimer.Tick += new EventHandler(Each_Tick);
                 myDispatcherTimer.Start();
            }

            
        }

    
 

// Fires every 100 miliseconds while the DispatcherTimer is active

       public void Each_Tick(object o, EventArgs sender)
        {
            timCount += 1;

            if (timCount == 2)
            {
                timCount = 0;
                genPopup.IsOpen = false;
                myDispatcherTimer.Stop();
            }
        }



       private void link_clear_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO: Add event handler implementation here.
		}

		private void link_cancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO: Add event handler implementation here.
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var tempObj = App.Current as App;
            listSections.ItemsSource = tempObj.sectionElements;
        }

        private void lbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbox.SelectedIndex == 0)
            {
                genPopup.IsOpen = false;
            }
        }

        private void minusClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            genPopup.IsOpen = true;
            popMsg.Text = "Confirm deletion ?";
            lbox.Visibility = System.Windows.Visibility.Collapsed;
            stackButtons.Visibility = System.Windows.Visibility.Visible;

        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelectionNum != -1)
            {
                var tempObj = App.Current as App;
                tempObj.sectionElements.RemoveAt(currentSelectionNum);
                genPopup.IsOpen = false;
                listSections.ItemsSource = null;
                listSections.ItemsSource = tempObj.sectionElements;

            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            genPopup.IsOpen = false;
        }

        private void listSections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelectionNum = listSections.SelectedIndex;
        }

        private void upClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((listSections.Items.Count > 1) && (currentSelectionNum > 0))
            {
                var tempObj = App.Current as App;
                sectionElementsDM temp;
                                
                temp = tempObj.sectionElements[currentSelectionNum];
                tempObj.sectionElements.RemoveAt(currentSelectionNum);
                tempObj.sectionElements.Insert(currentSelectionNum - 1, temp);

                listSections.ItemsSource = null;
                listSections.ItemsSource = tempObj.sectionElements;
            }

            if (currentSelectionNum == 0)
            {
                var tempObj = App.Current as App;
                sectionElementsDM temp;
                int tempCount = listSections.Items.Count - 1;
                
                temp = tempObj.sectionElements[currentSelectionNum];
                tempObj.sectionElements.RemoveAt(currentSelectionNum);
                tempObj.sectionElements.Insert(tempCount, temp);

                listSections.ItemsSource = null;
                listSections.ItemsSource = tempObj.sectionElements;
            }
        }

        private void downClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((listSections.Items.Count > 1) && (currentSelectionNum != listSections.Items.Count - 1))
            {
                var tempObj = App.Current as App;
                sectionElementsDM temp;

                temp = tempObj.sectionElements[currentSelectionNum];
                tempObj.sectionElements.RemoveAt(currentSelectionNum);
                tempObj.sectionElements.Insert(currentSelectionNum + 1, temp);

                listSections.ItemsSource = null;
                listSections.ItemsSource = tempObj.sectionElements;

            }

            if (currentSelectionNum == listSections.Items.Count - 1)
            {
                var tempObj = App.Current as App;
                sectionElementsDM temp;

                temp = tempObj.sectionElements[currentSelectionNum];
                tempObj.sectionElements.RemoveAt(currentSelectionNum);
                tempObj.sectionElements.Insert(0, temp);

                listSections.ItemsSource = null;
                listSections.ItemsSource = tempObj.sectionElements;
            }
        }

        private void editClick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (currentSelectionNum != -1)
            {
                editSectionGrid.Visibility = System.Windows.Visibility.Visible;
                
                var tempObj = App.Current as App;
                text_sectionName_edit.Text = tempObj.sectionElements[currentSelectionNum].sectionName;
                text_sectionUrl_edit.Text = tempObj.sectionElements[currentSelectionNum].feedurl;
                combolist.SelectedIndex = 0;


            }
        }

        private void editOk_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelectionNum != -1)
            {
                if ((text_sectionName_edit.Text != "") && (text_sectionUrl_edit.Text != ""))
                {
                    var tempObj = App.Current as App;
                    tempObj.sectionElements[currentSelectionNum].sectionName = text_sectionName_edit.Text;
                    tempObj.sectionElements[currentSelectionNum].feedurl = text_sectionUrl_edit.Text;
                    tempObj.sectionElements[currentSelectionNum].sectionType = combolist.SelectedItem.ToString();

                    switch (combolist.SelectedIndex)
                    {
                        case 0:
                            tempObj.sectionElements[currentSelectionNum].imgName = "Assets/facebook.png";
                            break;

                        case 1:
                            tempObj.sectionElements[currentSelectionNum].imgName = "Assets/twitter.png";
                            break;

                        case 2:
                            tempObj.sectionElements[currentSelectionNum].imgName = "Assets/more.png";
                            break;
                    }

                    listSections.ItemsSource = null;
                    listSections.ItemsSource = tempObj.sectionElements;

                    editSectionGrid.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    genPopup.IsOpen = true;
                    popMsg.Text = "Snap ! Please enter all details to create a section.";

                    lbox.Visibility = System.Windows.Visibility.Collapsed;
                    stackButtons.Visibility = System.Windows.Visibility.Collapsed;

                    myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); 
                    myDispatcherTimer.Tick += new EventHandler(Each_Tick);
                    myDispatcherTimer.Start();
 
                }
            }
        }

        private void canceledit_Click(object sender, RoutedEventArgs e)
        {
            editSectionGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void smalltileBtn_Click(object sender, RoutedEventArgs e)
        {

            openDialog("small", "png");

        }

        private void openDialog(string labelname, string formatFile)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;

          
            if (formatFile == "png")
            {
                dlg.Filter = "PNG Images (*.png)|*.png";
            }
            else
            {
                dlg.Filter = "JPG Images (*.jpg)|*.jpg";
            }
            

            bool? retval = dlg.ShowDialog();

            if (retval != null && retval == true)
            {
                   UploadFile(dlg.File.Name, dlg.File.OpenRead());
            }
           

        }
        private void UploadFile(string fileName, Stream data)
        {
            UriBuilder ub = new UriBuilder("http://localhost:25761/receiver.ashx");
            ub.Query = string.Format("filename={0}", fileName);
            WebClient c = new WebClient();

            c.OpenWriteCompleted += (sender, e) =>
            {
                PushData(data, e.Result);
                e.Result.Close();
                data.Close();
            };
            c.OpenWriteAsync(ub.Uri);
        }

        private void PushData(Stream input, Stream output)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

     	
	}
}