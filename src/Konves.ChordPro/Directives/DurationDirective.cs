
namespace Konves.ChordPro.Directives
{
    public sealed class DurationDirective : Directive
	{
        public DurationDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
