using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace engVidWSCORE
{
    public class LessonItem
    {
        public string title { get; set; }
        public string short_desc { get; set; }
        public string full_desc { get; set; }

        public List<string> level_topics = new List<string>();

        public string link { get; set; }

        public string teacher_name { get; set; }
        public string teacher_avatar_url { get; set; }
        public BitmapImage TEACHER_AVATAR { get; set; }
    }
}
