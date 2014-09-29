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
        public static List<LessonItem> LESSON_ITEMS = new List<LessonItem>();

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
                        }
                        var title = item.Descendants().Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(_ITEM_TITLE_CLASS)).FirstOrDefault();
                        if (title != null)
                        {
                            obj.title = mSTRING.fromHTML(title.InnerText);
                        }
                        var cats = item.Descendants().Where(c => c.Attributes.Contains("class") && c.Attributes["class"].Value.ToLower().Equals(_ITEM_CATS_CLASS));
                        for (int i = 0; i < cats.Count();i++ )
                        {
                            obj.level_topics.Add(mSTRING.fromHTML( cats.ElementAt(i).InnerText) );
                        }
                    }
                    
                    //add to re
                    LESSON_ITEMS.Add(obj);
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
