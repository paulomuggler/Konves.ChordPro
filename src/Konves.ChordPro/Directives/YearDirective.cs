
namespace Konves.ChordPro.Directives
{
    public sealed class YearDirective : Directive
	{
        public YearDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
