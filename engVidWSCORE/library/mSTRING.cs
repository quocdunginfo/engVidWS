using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engVidWSCORE
{
    public static class mSTRING
    {
        public static string fromHTML(String input = "")
        {
            string tmp = System.Net.WebUtility.HtmlDecode(input);
            tmp = tmp.Replace("\r\n", "");
            tmp = tmp.Replace("\n\r", "");
            tmp = tmp.Replace("•", "");
            tmp = tmp.Trim();
            return tmp;
        }
    }
}
