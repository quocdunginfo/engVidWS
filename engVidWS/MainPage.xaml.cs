using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using engVidWSCORE;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace engVidWS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            loadData();

            loadTeacherCombobox();
            loadTopicCombobox();
            loadLevelCombobox();
        }
        private async void loadData()
        {
            var tsk = await mENGVID.loadAll();
            displayData(mENGVID.LESSON_ITEMS);
        }
        /// <summary>
        /// Display list data to UI
        /// </summary>
        /// <param name="list"></param>
        private async void displayData(List<LessonItem> list)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    var re = mListHelper.convert(list);
                    //clear listbox first
                    if (listBox_main != null)
                    {
                        listBox_main.Items.Clear();
                    }

                    foreach (var item in re)
                    {
                        listBox_main.Items.Add(item);
                    }                
                }
            );
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void loadTeacherCombobox()
        {
            comBo_Teachers.Items.Add(
                new mImageComboboxItem(
                    new BitmapImage(),
                    "(All)"
                    )
                );

            foreach (var item in mENGVID.TEACHERS)
            {
                comBo_Teachers.Items.Add(
                    new mImageComboboxItem(
                        mCACHE.get(item.Value),
                        item.Key
                    ));
            }
        }
        private void loadTopicCombobox()
        {
            comBo_Topics.Items.Add(
                new mComboboxItem("(All)")
                );
            foreach (var item in mENGVID.TOPICS)
            {
                comBo_Topics.Items.Add(
                    new mComboboxItem(
                        item
                    ));
            }
        }

        private void loadLevelCombobox()
        {
            comBo_Levels.Items.Add(
                new mComboboxItem("(All)")
                );
            foreach (var item in mENGVID.LEVELS)
            {
                comBo_Levels.Items.Add(
                    new mComboboxItem(
                        item
                    ));
            }
        }
        
        private void comBo_Teachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var teacher = comBo_Teachers.SelectedItem as mImageComboboxItem;
            var topic = comBo_Topics.SelectedItem as mComboboxItem;
            var level = comBo_Levels.SelectedItem as mComboboxItem;
            
            List<LessonItem> re = mENGVID.search(
                getComboBoxValue(teacher),
                getComboBoxValue(topic),
                getComboBoxValue(level)
            );
            
            displayData(re);
        }
        /// <summary>
        /// Get value of Combobox
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string getComboBoxValue(mImageComboboxItem item)
        {
            if(item==null)
            {
                return "";
            }
            var value = item.getValue();
            if (value.Equals("(All)"))
            {
                value = "";
            }
            return value;
        }
        private string getComboBoxValue(mComboboxItem item)
        {
            if (item == null)
            {
                return "";
            }
            var value = item.getValue();
            if (value.Equals("(All)"))
            {
                value = "";
            }
            return value;
        }

        private void comBo_Topics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var teacher = comBo_Teachers.SelectedItem as mImageComboboxItem;
            var topic = comBo_Topics.SelectedItem as mComboboxItem;
            var level = comBo_Levels.SelectedItem as mComboboxItem;

            List<LessonItem> re = mENGVID.search(
                getComboBoxValue(teacher),
                getComboBoxValue(topic),
                getComboBoxValue(level)               
            );

            displayData(re);
        }

        private void comBo_Levels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var teacher = comBo_Teachers.SelectedItem as mImageComboboxItem;
            var topic = comBo_Topics.SelectedItem as mComboboxItem;
            var level = comBo_Levels.SelectedItem as mComboboxItem;

            List<LessonItem> re = mENGVID.search(
                getComboBoxValue(teacher),
                getComboBoxValue(topic),
                getComboBoxValue(level)                
            );

            displayData(re);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text;
            displayData(mENGVID.search_global(keyword));
        }

        private void listBox_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var item = listBox_main.SelectedItem as mListBoxItemOne;
            //displayLesson(item.LESSON);
            //btnLink.NavigateUri = new Uri(item.LESSON.YOUTUBE_LINK);
        }
        private void displayLesson(LessonItem item)
        {
            
        }

        private void web_main_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
