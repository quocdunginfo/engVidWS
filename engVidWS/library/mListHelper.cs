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
                //if (i % 2 == 0)
                //{
                //    mListBoxItemOne lo1 = new mListBoxItemOne(list[i].TEACHER_AVATAR, list[i].title, String.Join(" - ", list[i].level_topics));
                //    mListBoxItemOne lo2 = new mListBoxItemOne();
                //    if(i+1<list.Count)
                //    {
                //        lo2 = new mListBoxItemOne(list[i+1].TEACHER_AVATAR, list[i+1].title, String.Join(" - ", list[i+1].level_topics));
                //    }
                //    mListboxItem container = new mListboxItem(lo1,lo2);

                //    re.Add(container);
                //}
                mListBoxItemOne item = new mListBoxItemOne(list[i].TEACHER_AVATAR, list[i].title, String.Join(" - ", list[i].level_topics));

                re.Add(item);
            }
            return re;
        }
    }
}
