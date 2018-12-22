

namespace Konves.ChordPro.Directives
{
    public sealed class CopyrightDirective : Directive
	{
        public CopyrightDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
