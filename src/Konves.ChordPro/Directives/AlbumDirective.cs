
namespace Konves.ChordPro.Directives
{
    public sealed class AlbumDirective : Directive
	{
        public AlbumDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}
}
