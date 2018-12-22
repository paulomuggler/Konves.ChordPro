namespace Konves.ChordPro
{
	public sealed class Syllable
	{
		public Syllable(Chord chord, string text, int num)
		{
			Chord = chord;
			Text = text;
            number = num;
		}
		public Chord Chord { get; set; }
		public string Text { get; set; }
        public int number { get; set; }

		public override string ToString()
		{
			return $"{Chord?.ToString()}{Text}";
		}
	}
}
