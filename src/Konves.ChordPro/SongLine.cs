using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro
{
	public sealed class SongLine : ILine
	{

		public SongLine(int lineNumber, IEnumerable<Block> blocks)//, bool isChordTextPairedLine = false)
        {
            LineNumber = lineNumber;
			//IsChordTextPairedLine = isChordTextPairedLine;
            if(blocks != null)
                Blocks = blocks as List<Block> ?? blocks.ToList();
            else
                Blocks = new List<Block>();
        }

        public SongLine() : this(0, null){}
        public SongLine(int lineNum) : this(lineNum, null) { }

        public SongLine(IEnumerable<Block> blocks): this(0, blocks){}

        public int LineNumber { get; set; }
		public List<Block> Blocks { get; set; }
		//public bool IsChordTextPairedLine { get; set; }

		public override string ToString()
		{
			return string.Join("   ", Blocks?.Select(s => s.ToString()) ?? Enumerable.Empty<string>());
		}
	}
}
