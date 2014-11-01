using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using engVidWSCORE;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace engVidWS
{
    public sealed partial class mListBoxItemOne : UserControl
    {
        public mListBoxItemOne()
        {
            this.InitializeComponent();
        }
        private LessonItem lesson = null;
        public LessonItem LESSON
        {
            get
            {
                return lesson;
            }
        }
        public mListBoxItemOne(BitmapImage img, string title, string subTitle, LessonItem lesson=null)
        {
            this.InitializeComponent();
            this.img.Source = img;
            this.title.Text = title;
            this.subTitle.Text = subTitle;
            this.lesson = lesson;
        }
    }
}
