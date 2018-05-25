using System.Collections.Generic;
using System.Linq;
using UniRx;
using Konves.ChordPro.Directives;
using _Common.Extensions;

namespace Konves.ChordPro
{
    public sealed class Document
    {
        public Document(IEnumerable<ILine> lines)
        {
            Lines = lines as List<ILine> ?? lines.ToList();

            SetSongSections();

            // initializing reactive models
            title = new ReactiveProperty<string>(GetMetaDirective<TitleDirective>().Text);
            title.Subscribe(v => GetMetaDirective<TitleDirective>().Text = v);

            subtitle = new ReactiveProperty<string>(GetMetaDirective<SubtitleDirective>().Text);
            subtitle.Subscribe(v => GetMetaDirective<SubtitleDirective>().Text = v);

            //artist = new ReactiveProperty<string>(GetMetaDirective<ArtistDirective>().Text);
            //artist.Subscribe(v => GetMetaDirective<ArtistDirective>().Text = v);

            //composer = new ReactiveProperty<string>(GetMetaDirective<ComposerDirective>().Text);
            //composer.Subscribe(v => GetMetaDirective<ComposerDirective>().Text = v);

            //lyricist = new ReactiveProperty<string>(GetMetaDirective<LyricistDirective>().Text);
            //lyricist.Subscribe(v => GetMetaDirective<LyricistDirective>().Text = v);

            //copyright = new ReactiveProperty<string>(GetMetaDirective<CopyrightDirective>().Text);
            //copyright.Subscribe(v => GetMetaDirective<CopyrightDirective>().Text = v);

            //album = new ReactiveProperty<string>(GetMetaDirective<AlbumDirective>().Text);
            //album.Subscribe(v => GetMetaDirective<AlbumDirective>().Text = v);

            //year = new ReactiveProperty<string>(GetMetaDirective<YearDirective>().Text);
            //year.Subscribe(v => GetMetaDirective<YearDirective>().Text = v);

            //key = new ReactiveProperty<string>(GetMetaDirective<KeyDirective>().Text);
            //key.Subscribe(v => GetMetaDirective<KeyDirective>().Text = v);

            //time = new ReactiveProperty<string>(GetMetaDirective<TimeDirective>().Text);
            //time.Subscribe(v => GetMetaDirective<TimeDirective>().Text = v);

            //tempo = new ReactiveProperty<string>(GetMetaDirective<TempoDirective>().Text);
            //tempo.Subscribe(v => GetMetaDirective<TempoDirective>().Text = v);

            //duration = new ReactiveProperty<string>(GetMetaDirective<DurationDirective>().Text);
            //duration.Subscribe(v => GetMetaDirective<DurationDirective>().Text = v);

            //capo = new ReactiveProperty<string>(GetMetaDirective<CapoDirective>().Text);
            //capo.Subscribe(v => GetMetaDirective<CapoDirective>().Text = v);

        }

        public List<ILine> Lines { get; set; }

        //
        // Meta directives reactive models
        //

        public ReactiveProperty<string> title { get; private set; }
        public ReactiveProperty<string> subtitle { get; private set; }
        //public ReactiveProperty<string> artist { get; private set; }
        //public ReactiveProperty<string> composer { get; private set; };
        //public ReactiveProperty<string> lyricist { get; private set; };
        //public ReactiveProperty<string> copyright { get; private set; };
        //public ReactiveProperty<string> album { get; private set; };
        //public ReactiveProperty<string> year { get; private set; };
        //public ReactiveProperty<string> key { get; private set; };
        //public ReactiveProperty<string> time { get; private set; };
        //public ReactiveProperty<string> tempo { get; private set; };
        //public ReactiveProperty<string> duration { get; private set; };
        //public ReactiveProperty<string> capo { get; private set; };

        //public List<ReactiveProperty<string>> meta
        //{
        //    get { return ((Lines.SelectMany(d => d is MetaDirective)); }
        //    //set { GetMetaDirective<MetaDirective>().Text = value; }
        //}

        T GetMetaDirective<T>() where T : Directive, new()
        {
            var dir = (Lines.Select(d => d is T) as T);
            if (dir == null)
            {
                dir = new T();
                Lines.Add(dir);
            }
            return dir;
        }

        // Song structure properties
        List<List<ILine>> _songSections = new List<List<ILine>>();
        public List<List<ILine>> songSections { get { return _songSections; } }

        void SetSongSections()
        {
            //int section = 0;
            List<ILine> currentSection = new List<ILine>();
            songSections.Add(currentSection);
            foreach (ILine ln in Lines)
            {
                if (ln is SongLine)
                {
                    if (ln.ToString().IsNullOrWhiteSpace())
                    {
                        //end current section
                        //begin new section
                        currentSection = new List<ILine>();
                        songSections.Add(currentSection);
                    }
                    else
                    {
                        currentSection.Add(ln);
                    }
                }
                else if (ln is StartOfTabDirective)
                {
                    currentSection = new List<ILine>();
                    songSections.Add(currentSection);
                }
                else if (ln is TabLine)
                {
                    currentSection.Add(ln);
                }
                else if (ln is EndOfTabDirective)
                {
                    currentSection = new List<ILine>();
                }
            }
        }
    }
}
