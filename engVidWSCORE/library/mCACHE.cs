﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace engVidWSCORE
{
    /// <summary>
    /// Hỗ trợ cache
    /// </summary>
    public static class mCACHE
    {
        /// <summary>
        /// relative string: http://haivl.com/photo/232347
        /// </summary>
        private static Dictionary<String, BitmapImage> collection = new Dictionary<string, BitmapImage>();
        private static HashSet<String> fail_url = new HashSet<string>();

        public static BitmapImage get(String url)
        {
            if (url == null)
            {
                return null;
            }
            //Nếu nằm trong fail list thì return ảnh mặc định
            if (fail_url.Contains(url))
            {
                return null;
            }
            //tìm kiếm trong cache list
            if (collection.ContainsKey(url))
            {
                return collection[url];
            }
            //neu khong co thi tu register luon
            else
            {
                register(url, mHTTP.getImage(url));
                return get(url);
            }
        }
        /// <summary>
        /// Đánh dấu url fail, lần get kế tiếp từ CACHE sẽ trả về hình mặc định
        /// </summary>
        /// <param name="url"></param>
        public static void mark_url_fail(String url)
        {
            fail_url.Add(url);
        }
        /// <summary>
        /// Đăng ký ảnh vào cache list, OVERRIDE MODE ON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="image"></param>
        private static void register(String url, BitmapImage image)
        {
            if (url == null)
            {
                return;
            }
            if (collection.ContainsKey(url))
            {
                collection.Remove(url);
            }
            collection.Add(url, image);
            Debug.WriteLine("mCACHE: "+collection.Count);
        }
        /// <summary>
        /// clear memory
        /// </summary>
        public static void release()
        {
            if (collection != null)
            {
                collection.Clear();
            }
            if (fail_url != null)
            {
                fail_url.Clear();
            }
        }
    }
}
