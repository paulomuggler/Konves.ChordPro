using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro
{
	public sealed class Word : Block
	{
		public Word(int blockNum, string text)
		{
            BlockNumber = blockNum;
			Syllables = new List<Syllable> { new Syllable(null, text, 1) };
		}

        public Word(int blockNum, IEnumerable<Syllable> syllables)
		{
            BlockNumber = blockNum;
			Syllables = syllables as List<Syllable> ?? syllables.ToList();
		}

		public List<Syllable> Syllables { get; set; }

		public override string ToString()
		{
			return string.Join("", Syllables?.Select(s => s.ToString()) ?? Enumerable.Empty<string>());
		}
	}
}
