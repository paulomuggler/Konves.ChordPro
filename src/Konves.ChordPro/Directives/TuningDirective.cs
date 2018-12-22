
namespace Konves.ChordPro.Directives
{
    public sealed class TuningDirective : Directive
	{
        public TuningDirective()
        {
            Text = "";
        }

        public TuningDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
