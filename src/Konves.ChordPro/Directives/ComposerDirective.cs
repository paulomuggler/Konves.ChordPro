
namespace Konves.ChordPro.Directives
{
    public sealed class ComposerDirective : Directive
	{
        public ComposerDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
