using System.Text.RegularExpressions;

namespace api_wov.Services
{
    public class Utilities
    {
        public class Status
        {
            public static string Active = "active";
            public static string InActive = "inactive";
        } 
        public static string ConvertToUnsign(string str)
        {
            const string pattern =
                @"([àáạảãâầấậẩẫăằắặẳẵ])|([èéẹẻẽêềếệểễ])|([ìíịỉĩ])|([òóọỏõôồốộổỗơờớợởỡ])|([ùúụủũưừứựửữ])|([ỳýỵỷỹ])|([đ])";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = regex.Replace(str, delegate (Match match)
            {
                if (match.Groups[1].Value.Length > 0)
                    return "a";
                if (match.Groups[2].Value.Length > 0)
                    return "e";
                if (match.Groups[3].Value.Length > 0)
                    return "i";
                if (match.Groups[4].Value.Length > 0)
                    return "o";
                if (match.Groups[5].Value.Length > 0)
                    return "u";
                if (match.Groups[6].Value.Length > 0)
                    return "y";
                if (match.Groups[7].Value.Length > 0)
                    return "d";
                return "";
            });
            //string regExp = "[^\w\d]";
            //result = Regex.Replace(result, "[^0-9a-zA-Z]+", "-");
            return result;
        }

        public static string ConvertToUnsignUrl(string str)
        {
            const string pattern =
                @"([àáạảãâầấậẩẫăằắặẳẵ])|([èéẹẻẽêềếệểễ])|([ìíịỉĩ])|([òóọỏõôồốộổỗơờớợởỡ])|([ùúụủũưừứựửữ])|([ỳýỵỷỹ])|([đ])";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = regex.Replace(str, delegate (Match match)
            {
                if (match.Groups[1].Value.Length > 0)
                    return "a";
                if (match.Groups[2].Value.Length > 0)
                    return "e";
                if (match.Groups[3].Value.Length > 0)
                    return "i";
                if (match.Groups[4].Value.Length > 0)
                    return "o";
                if (match.Groups[5].Value.Length > 0)
                    return "u";
                if (match.Groups[6].Value.Length > 0)
                    return "y";
                if (match.Groups[7].Value.Length > 0)
                    return "d";
                return "";
            });
            //string regExp = "[^\w\d]";
            result = Regex.Replace(result, "[^0-9a-zA-Z]+", "-");
            return result;
        }

        public static String UnsignNoSpace(String str)
        {
            string result = ConvertToUnsign(str).Trim().Replace(" ", "-").ToLower();
            return result;
        }
    }
}
