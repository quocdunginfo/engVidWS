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
        public BitmapImage TEACHER_AVATAR
        {
            get
            {
                return mCACHE.get(teacher_avatar_url);
            }
        }
        private string youtube_link = null;
        public string YOUTUBE_LINK
        {
            get
            {
                if (youtube_link == null)
                {
                    youtube_link = mENGVID.getYoutubeLink(link).Result;
                }
                return youtube_link;
            }
        }

        public Boolean matchKeyWord(string key_word = "")
        {
            if (key_word == null || key_word.Equals(""))
            {
                return true;
            }
            if (title != null && title.ToLower().Contains(key_word))
            {
                return true;
            }
            if (teacher_name != null && teacher_name.ToLower().Contains(key_word))
            {
                return true;
            }
            foreach (var item in level_topics)
            {
                if (item != null && item.ToLower().Contains(key_word))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
