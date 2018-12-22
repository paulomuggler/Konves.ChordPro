
namespace Konves.ChordPro.Directives
{
    public sealed class TempoDirective : Directive
	{
        public TempoDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
