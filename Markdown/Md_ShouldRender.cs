using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class Md_ShouldRender
    {
        private Md md;

        [SetUp]
        public void CreateMd() => md = new Md();

        [TestCase("_abc_", ExpectedResult = "<em>abc</em>", TestName = "Correct underscore should be with <em>")]
        [TestCase("_abc dfg_", ExpectedResult = "<em>abc dfg</em>", TestName = "Many words in underscores should be with <em>")]
        [TestCase("2_2_2", ExpectedResult = "2_2_2", TestName = "Underscore with number should be unchanged")]
        [TestCase(@"\_abc\_", ExpectedResult = "_abc_", TestName = "Underscore with backslash should be screened")]
        [TestCase("_ abc_", ExpectedResult = "_ abc_", TestName = "Space after start underscore shoud be unchanged")]
        [TestCase("_abc _", ExpectedResult = "_abc _", TestName = "Space before finish underscore shoud be unchanged")]
        public string RenderToHtml_WithSingleUnderscore(string mdString) => md.RenderToHtml(mdString);

        [TestCase("__abc__", ExpectedResult = "<strong>abc</strong>", TestName = "Correct double underscore should be with <strong>")]
        [TestCase("__abc dfg__", ExpectedResult = "<strong>abc dfg</strong>", TestName = "Many words in double underscores should be with <strong>")]
        [TestCase("__2__2", ExpectedResult = "__2__2", TestName = "Double underscore with number should be unchanged")]
        [TestCase(@"\__abc\__", ExpectedResult = "__abc__", TestName = "Double underscore with backslash should be screened")]
        [TestCase("__ abc__", ExpectedResult = "__ abc__", TestName = "Space after start double underscore shoud be unchanged")]
        [TestCase("__abc __", ExpectedResult = "__abc __", TestName = "Space before finish double underscore shoud be unchanged")]
        public string RenderToHtml_WithDoubleUnderscore(string mdString) => md.RenderToHtml(mdString);

        [TestCase("__abc_", ExpectedResult = "__abc_", TestName = "Unpaired underscore shoud be unchanged")]
        [TestCase("__abc _ab_ abc__", ExpectedResult = "<strong>abc <em>ab</em> abc</strong>", TestName = "Underscores in double underscores shoud be work")]
        [TestCase("__abc__ _abc_", ExpectedResult = "<strong>abc</strong> <em>abc</em>", TestName = "Underscores consistently shoud be work")]
        [TestCase("_abc __ab__ abc_", ExpectedResult = "<em>abc __ab__ abc</em>", TestName = "Double underscores in underscore shoud be not work")]
        [TestCase("_abc __ab__ abc", ExpectedResult = "_abc <strong>ab</strong> abc", TestName = "Double underscores in not close underscore shoud work")]
        public string RenderToHtml_WithDifferentUnderscore(string mdString) => md.RenderToHtml(mdString);

    }
}
