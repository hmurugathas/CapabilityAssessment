using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PressfordConsultingNews.Extensions
{
    public static class StringExtensions
    {
        public static string Preview(this string content, int maxCharacter)
        {
            if (string.IsNullOrEmpty(content.Trim()))
            {
                return string.Empty;
            }

            if (content.Length > maxCharacter)
            {
                return content.Substring(0, maxCharacter) + "...";
            }

            return content;
        }
    }
}