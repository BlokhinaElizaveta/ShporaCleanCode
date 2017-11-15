using System.Collections.Generic;

namespace Markdown
{
    public class Md
	{
	    private readonly Stack<Tag> stackOfTags;
	    private readonly List<Tag> validTags;

	    public Md()
	    {
	        stackOfTags = new Stack<Tag>();
	        validTags = new List<Tag>();
	    }

	    public string RenderToHtml(string markdown)
		{
		    for (var i = 0; i < markdown.Length; i++)
		    {
		        var tag = CreateTag(markdown, i);
		        if (tag == null)
                    continue;

		        stackOfTags.Push(tag);
		        i += tag.MdLength;

                if (tag.Name == TagsName.Screen)
                    validTags.Add(tag);
		        if (tag.Type == TagsType.Close)
		            UpdateTags();
		    }
			return ReplaceMdToHtmlTags(markdown);
		}

	    private Tag CreateTag(string line, int i)
	    {
            if (Tag.IsStrongTag(line, i) && Tag.IsCloseTag(line, i))
                return new Tag(TagsName.Strong, TagsType.Close, i);

	        if (Tag.IsStrongTag(line, i) && Tag.IsOpenTag(line, i+1))
	            return new Tag(TagsName.Strong, TagsType.Open, i);

	        if (Tag.IsEmTag(line, i) && Tag.IsCloseTag(line, i))
	            return new Tag(TagsName.Em, TagsType.Close, i);

	        if (Tag.IsEmTag(line, i) && Tag.IsOpenTag(line, i))
	            return new Tag(TagsName.Em, TagsType.Open, i);

            if (Tag.IsScreenTag(line, i))
                return new Tag(TagsName.Screen, TagsType.Open, i);

	        return null;
	    }

	    private void UpdateTags()
	    {
	        var closeTag = stackOfTags.Pop();
	        while (stackOfTags.Count != 0)
	        {
	            if (stackOfTags.Peek().Name == closeTag.Name)
	                break;
	            stackOfTags.Pop();
	        }

	        if (stackOfTags.Count == 0) return;

	        var openTag = stackOfTags.Pop();
	        validTags.Add(openTag);
	        validTags.Add(closeTag);
	    }

	    private string ReplaceMdToHtmlTags(string line)
	    {
	        validTags.Sort((t1, t2) => t2.Position.CompareTo(t1.Position));
	        var inEmTag = false;
	        foreach (var tag in validTags)
	        {
	            if (inEmTag && tag.Name == TagsName.Strong)
	                continue;

	            if (tag.Name == TagsName.Em)
	                inEmTag = (tag.Type == TagsType.Close);

	            line = line
	                .Remove(tag.Position, tag.MdLength)
	                .Insert(tag.Position, tag.HtmlTag);
	        }
	        return line;
	    }
    }	
}