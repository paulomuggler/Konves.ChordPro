
namespace Konves.ChordPro.Directives
{
    public sealed class KeyDirective : Directive
	{
		public KeyDirective()
        {
            Text = "";
        }

        public KeyDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
