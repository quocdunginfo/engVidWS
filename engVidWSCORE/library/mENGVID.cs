using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace engVidWSCORE
{
    public static class mENGVID
    {
        public delegate void mDataLoading(int current_index, int total);
        public static event mDataLoading onDataLoading;

        public static List<LessonItem> LESSON_ITEMS = new List<LessonItem>();
        public static List<string> LEVELS =
            new List<string>((new string[] { "1-Beginner", "2-Intermediate", "3-Advanced" }).ToList());
        public static List<string> TOPICS =
            new List<string>((new string[] {
            "business english",
            "comprehension",
            "culture & tips",
            "Exams",
            "expressions",
            "grammar",
            "IELTS",
            "pronunciation",
            "slang",
            "speaking",
            "TOEFL",
            "TOEIC",
            "vocabulary",
            "writing"
            }).ToList());
        public static Dictionary<string, string> TEACHERS;

        static mENGVID()
        {
            TEACHERS = new Dictionary<string, string>();

            TEACHERS.Add("Adam", "http://gravatar.com/avatar/e7f5b219fec097a7843b9b2bdce11962");
            TEACHERS.Add("Alex", "http://gravatar.com/avatar/a696b2b37d4593bf82f402e905d5c1ae");
            TEACHERS.Add("Benjamin", "http://gravatar.com/avatar/bd04f00e0b8307d1bfbcf364c14183f6");
            TEACHERS.Add("Emma", "http://gravatar.com/avatar/51ce2a2d46c539df546860882ccb7bab");
            TEACHERS.Add("Jade", "http://gravatar.com/avatar/c07d8ac0b28c62a895f134a3dda02d1c");
            TEACHERS.Add("James", "http://gravatar.com/avatar/20934db50db6aac85d751755abb4f88b");
            TEACHERS.Add("Jon", "http://gravatar.com/avatar/39b4801bf8b7aba81ed0e375b6af03a6");
            TEACHERS.Add("Rebecca", "http://gravatar.com/avatar/fa89abfe7f076aedbd3387d306770da8");
            TEACHERS.Add("Ronnie", "http://gravatar.com/avatar/2a596bc022df8bd33b2e81ee7e917e99");
            TEACHERS.Add("Valen", "http://gravatar.com/avatar/7ac67909afe6f765c7ca561b83e7ebd1");
        }

        public static string _LESSON_BROWSER_GATEWAY = "http://www.engvid.com/english-lesson-browser/";
        public static string _ITEM_CLASS = "lessonlinks_all_row";
        public static string _ITEM_TITLE_CLASS = "lessonlinks_all_lessontitle";
        public static string _ITEM_CATS_CLASS = "lessonlinks_all_category_item";
        #region Business
        public static async Task<int> loadAll()
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                string html = await mHTTP.getHTML(_LESSON_BROWSER_GATEWAY).ConfigureAwait(false);
                //get html fail
                if(html==null || html.Equals(""))
                {
                    return -1;
                }
                //parse html
                doc.LoadHtml(html);
                //query
                var list = doc.DocumentNode.Descendants("div")
                    .Where(
                    c =>
                        c.Attributes.Contains("class")
                        &&
                        c.Attributes["class"].Value.ToLower().Equals(_ITEM_CLASS)
                    ).ToList();
                int count = 0;
                foreach (var item in list)
                {
                    LessonItem obj = new LessonItem();

                    var link = item.Descendants().Where(c =>c.Name.Equals("a")).FirstOrDefault();
                    if (link != null)
                    {
                        obj.link = link.Attributes["href"].Value;

                        var img = item.Descendants().Where(c => c.Name.Equals("img") && c.Attributes.Contains("src")).FirstOrDefault();
                        if (img != null)
                        {
                            obj.teacher_avatar_url = img.Attributes["src"].Value;
                            if (obj.teacher_avatar_url != null && !obj.teacher_avatar_url.Equals(""))
                            {
                                try
                                {
                                    obj.teacher_name = TEACHERS.Where(c => obj.teacher_avatar_url.ToLower().Contains(c.Value.ToLower())).FirstOrDefault().Key;
                                }
                                catch (Exception rt)
                                {
                                    Debug.WriteLine(rt);
                                    obj.teacher_name = "";
                                }
                            }

                        }
                        var title = item.Descendants().Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(_ITEM_TITLE_CLASS)).FirstOrDefault();
                        if (title != null)
                        {
                            obj.title = mSTRING.fromHTML(title.InnerText);
                        }
                        var cats = item.Descendants().Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(_ITEM_CATS_CLASS));
                        for (int i = 0; i < cats.Count();i++ )
                        {
                            obj.level_topics.Add(mSTRING.fromHTML( cats.ElementAt(i).InnerText));
                        }
                    }

                    count++;
                    //add to re
                    LESSON_ITEMS.Add(obj);
                    //raise event
                    if (onDataLoading != null)
                    {
                        onDataLoading(count, list.Count);
                    }
                }
                return 1;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return 1;
            }
        }

        #endregion
    }
}
