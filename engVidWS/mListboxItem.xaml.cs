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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace engVidWS
{
    public sealed partial class mListboxItem : UserControl
    {
        public mListboxItem()
        {
            this.InitializeComponent();
        }
        public mListboxItem(mListBoxItemOne left_item, mListBoxItemOne right_item)
        {
            this.InitializeComponent();

            this.left.Children.Add(left_item);
            this.right.Children.Add(right_item);
        }
    }
}
