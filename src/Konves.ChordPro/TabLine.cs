namespace Konves.ChordPro
{
	public sealed class TabLine : ILine
	{
        public TabLine(int lineNumber, string text)
        {
            LineNumber = lineNumber;
            Text = text;
        }

        public TabLine(string text): this(0, text){}

        public int LineNumber { get; set; }
		public string Text { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}
