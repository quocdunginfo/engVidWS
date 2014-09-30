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

        private async void displayData(List<LessonItem> list)
        {
            var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            await dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    var re = mListHelper.convert(list);
                    //clear listbox first
                    if (listBox1 != null)
                    {
                        listBox1.Items.Clear();
                    }

                    foreach (var item in re)
                    {
                        listBox1.Items.Add(item);
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
            var item = (sender as ComboBox).SelectedItem as mImageComboboxItem;
            string value = item.getValue();
            if (value == null)
            {
                return;
            }

            List<LessonItem> re =null;
            if (!value.ToLower().Contains("all"))
            {
                re = mENGVID.LESSON_ITEMS.Where(c => c.teacher_name != null && c.teacher_name.ToLower().Contains(value.ToLower())).ToList();

            }
            else
            {
                re = mENGVID.LESSON_ITEMS;
            }
            //re = mENGVID.LESSON_ITEMS.Where(c => c.teacher_name == null).ToList();
            displayData(re);
        }
    }
}
