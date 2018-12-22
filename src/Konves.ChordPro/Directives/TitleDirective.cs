namespace Konves.ChordPro.Directives
{
	public sealed class TitleDirective : Directive
	{
        public TitleDirective()
        {
            Text = "";
        }

		public TitleDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
