
namespace Konves.ChordPro.Directives
{
    public sealed class ArtistDirective : Directive
	{
		public ArtistDirective()
        {
            Text = "";
        }

        public ArtistDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
