
namespace Konves.ChordPro.Directives
{
    public sealed class LyricistDirective : Directive
	{
        public LyricistDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
