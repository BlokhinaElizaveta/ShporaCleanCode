using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public enum TagsName
    {
        Em, Strong, Screen
    }

    public enum TagsType
    {
        Open, Close
    }

    public static class SettingForTag
    {
        public static readonly Dictionary<TagsName, int> MdLengths =  new Dictionary<TagsName, int>
        {
            {TagsName.Em, 1}, {TagsName.Strong, 2}, { TagsName.Screen, 1 }
        };
        public static readonly Dictionary<TagsName, string> HtmlTags = new Dictionary<TagsName, string>
        {
            {TagsName.Em, "<em>"}, {TagsName.Strong, "<strong>"}, { TagsName.Screen, "" }
        };
    }
}
