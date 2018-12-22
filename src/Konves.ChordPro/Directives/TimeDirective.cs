
namespace Konves.ChordPro.Directives
{
    public sealed class TimeDirective : Directive
	{
        public TimeDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
