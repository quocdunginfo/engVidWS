using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class mImageComboboxItem : UserControl
    {
        public mImageComboboxItem()
        {
            this.InitializeComponent();
        }
        public mImageComboboxItem(BitmapImage img, String text)
        {
            this.InitializeComponent();
            this.img.Source = img;
            this.title.Text = text;
        }

        public string getValue()
        {
            return this.title.Text;
        }
    }
}
