
namespace Konves.ChordPro.Directives
{
    public sealed class CapoDirective : Directive
	{
		public CapoDirective()
        {
            Text = "";
        }

        public CapoDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
