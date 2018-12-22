using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Konves.ChordPro
{
    internal sealed class Parser
    {
        internal Parser(TextReader textReader) : this(textReader, null)
        {
        }

        internal Parser(TextReader textReader, IEnumerable<DirectiveHandler> customHandlers)
        {
            _textReader = textReader;
            _directiveParsers = DirectiveHandlerUtility.GetHandlersDictionaryByName(customHandlers);
        }

        internal IEnumerable<ILine> Parse()
        {
            string line;
            int lineNum = 0;
            while ((line = _textReader.ReadLine()) != null)
            {
                lineNum++;
                if (_isInTab)
                {
                    if (GetLineType(line) == LineType.Directive && ParseDirective(lineNum, line) is EndOfTabDirective)
                        yield return new EndOfTabDirective();
                    else
                        yield return new TabLine(lineNum, line);
                }
                else
                {
                    switch (GetLineType(line))
                    {
                        case LineType.Comment:
                            // Do nothing
                            break;
                        case LineType.Directive:
                            yield return ParseDirective(lineNum, line);
                            break;
                        case LineType.Text:
                            yield return ParseSongLine(lineNum, line);
                            break;
                        case LineType.Whitespace:
                            yield return new SongLine(lineNum);
                            break;
                    }
                }

                _lineNumber++;
            }
        }

        internal Directive ParseDirective(int lineNum, string line)
        {
            DirectiveComponents components;
            DirectiveHandler handler;
            Directive directive;
            if (DirectiveComponents.TryParse(line, out components) && _directiveParsers.TryGetValue(components.Key, out handler) && handler.TryParse(components, out directive))
            {
                if (directive is StartOfTabDirective)
                    _isInTab = true;

                if (directive is EndOfTabDirective)
                    _isInTab = false;

                directive.LineNumber = lineNum;
                return directive;
            }
            else
            {
                throw new FormatException($"Invalid directive at line {_lineNumber}.\n{line}");
            }
        }

        internal SongLine ParseSongLine(int lineNumber, string line)
        {
            return new SongLine(lineNumber, SplitIntoBlocks(line).Select(ParseBlock));
        }

        internal static IEnumerable<string> SplitIntoBlocks(string line)
        {
            //         string path = Application.dataPath;
            //         if (!File.Exists(path))
            //         {
            //             File.Create(path);
            //         }
            int blockNum = 1;
            if (line == null)
                throw new ArgumentNullException();

            int start = 0;
            bool isInBlock = false;
            bool isInChord = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (isInBlock && !isInChord)
                {
                    if (char.IsWhiteSpace(line[i]))
                    {
                        //Debug.Log(line.Substring(start, i - start));
                        yield return $"{blockNum++}_{line.Substring(start, i - start)}";
                        isInBlock = false;
                        continue;
                    }
                    else if (i >= line.Length - 1)
                    {
                        //Debug.Log(line.Substring(start, i - start + 1));
                        yield return $"{blockNum++}_{line.Substring(start, i - start + 1)}";
                        isInBlock = false;
                        continue;
                    }
                }

                if (!isInBlock && !char.IsWhiteSpace(line[i]))
                {
                    isInBlock = true;
                    start = i;

                    if (i >= line.Length - 1)
                    {
                        //i = line.Length - 1;
                        //Debug.Log(line.Substring(start, i - start + 1));
                        yield return $"{blockNum++}_{line.Substring(start, i - start + 1)}";
                        continue;
                    }
                }

                if (!isInChord && line[i] == '[')
                {
                    isInChord = true;
                }

                if (isInChord && line[i] == ']')
                {
                    isInChord = false;
                    if (i >= line.Length - 1)
                    {
                        //i = line.Length - 1;
                        //Debug.Log(line.Substring(start, i - start + 1));
                        yield return $"{blockNum++}_{line.Substring(start, i - start + 1)}";
                        continue;
                    }
                }
            }
        }

        internal static IEnumerable<string> SplitIntoSyllables(string block)
        {
            int start = 0;
            bool isInChord = false;
            int syllableNum = 1;
            for (int i = 0; i < block.Length; i++)
            {

                if (!isInChord && block[i] == '[')
                {
                    if (i > 0)
                    {
                        yield return $"{syllableNum++}_{block.Substring(start, i - start)}";
                        start = i;
                    }

                    isInChord = true;
                }

                if (isInChord && block[i] == ']')
                {
                    isInChord = false;
                }

                if (i == block.Length - 1)
                    yield return $"{syllableNum++}_{block.Substring(start, i - start + 1)}";
            }
        }

        internal Block ParseBlock(string _block)
        {
            var block = _block.Substring(_block.IndexOf('_') + 1);
            int blockNum = int.Parse(_block.Substring(0, _block.IndexOf('_')));
            List<string> syllables = SplitIntoSyllables(block).ToList();

            Chord chord;

            if (syllables.Count == 1 && TryParseChord(blockNum, syllables.Single().Substring(syllables.Single().IndexOf('_') + 1), out chord))
                return chord;

            return new Word(blockNum, syllables.Select(ParseSyllable));
        }

        internal static bool TryParseChord(int blockNum, string s, out Chord chord)
        {
            if (s.StartsWith("[") && s.EndsWith("]") && s.Length > 2)
            {
                chord = new Chord(blockNum, s.Substring(1, s.Length - 2).Trim());
                return true;
            }

            chord = null;
            return false;
        }

        internal Syllable ParseSyllable(string _syllable)
        {
            var syllable = _syllable.Substring(_syllable.IndexOf('_') + 1);
            int syllableNum = int.Parse(_syllable.Substring(0, _syllable.IndexOf('_')));
            int i = syllable.IndexOf("]");

            Chord chord = null;

            if (i > -1)
            {
                if (!TryParseChord(syllableNum, syllable.Substring(0, i + 1), out chord))
                    throw new FormatException($"Incorrect chord format at line {_lineNumber}.");
            }

            string text = syllable.Substring(i + 1);

            return new Syllable(chord, text.Length == 0 ? null : text, syllableNum);
        }

        internal static LineType GetLineType(string line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                    return LineType.Comment;
                else if (line[i] == '{')
                    return LineType.Directive;
                else if (!char.IsWhiteSpace(line[i]))
                    return LineType.Text;
            }

            return LineType.Whitespace;
        }

        internal enum LineType
        {
            Comment,
            Text,
            Directive,
            Whitespace
        }

        readonly TextReader _textReader;
        readonly IReadOnlyDictionary<string, DirectiveHandler> _directiveParsers;
        internal bool _isInTab = false;
        int _lineNumber = 0;
    }
}
