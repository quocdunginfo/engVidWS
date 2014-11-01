using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engVidWSCORE;

namespace engVidWS
{
    public static class mListHelper
    {
        public static List<mListBoxItemOne> convert(List<LessonItem> list)
        {
            List<mListBoxItemOne> re = new List<mListBoxItemOne>();
            for (int i = 0; i < list.Count; i++)
            {
                mListBoxItemOne item = new mListBoxItemOne(list[i].TEACHER_AVATAR, list[i].title, String.Join(" - ", list[i].level_topics),list[i]);

                re.Add(item);
            }
            return re;
        }
    }
}
