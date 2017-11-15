using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    class Tag
    {
        public readonly string HtmlTag;
        public readonly TagsName Name;
        public readonly TagsType Type;
        public int MdLength => SettingForTag.MdLengths[Name];
        public readonly int Position;

        public Tag(TagsName name, TagsType type, int position)
        {
            Name = name;
            Type = type;
            Position = position;
            HtmlTag = GetHtmlTag();
        }

        private string GetHtmlTag()
        {
            var htmlTag = SettingForTag.HtmlTags[Name];

            if (Type == TagsType.Close)
                htmlTag = htmlTag.Insert(1, "/");

            return htmlTag;
        }

        public static bool IsOpenTag(string line, int i)
        {
            return i + 1 < line.Length && line[i + 1] != ' ' && !char.IsNumber(line, i + 1);
        }

        public static bool IsCloseTag(string line, int i)
        {
            return i > 0 && line[i - 1] != ' ' && !char.IsNumber(line, i - 1); 
        }

        public static bool IsEmTag(string line, int i) => line[i] == '_';

        public static bool IsStrongTag(string line, int i)
        {
            return i < line.Length - 1 && line[i] == '_' && line[i + 1] == '_';
        }

        public static bool IsScreenTag(string line, int i)
        {
            return i < line.Length - 1 && line[i] == '\\' && line[i + 1] == '_';
        }
    }
}
