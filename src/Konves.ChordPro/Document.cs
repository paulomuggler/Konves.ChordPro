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
            title = new ReactiveProperty<string>(GetMetaDirective<TitleDirective>()?.Text);
			title.Subscribe(v => {if(!string.IsNullOrWhiteSpace(v))GetOrAddMetaDirective<TitleDirective>().Text = v;});

            subtitle = new ReactiveProperty<string>(GetMetaDirective<SubtitleDirective>()?.Text);
			subtitle.Subscribe(v => {if(!string.IsNullOrWhiteSpace(v))GetOrAddMetaDirective<SubtitleDirective>().Text = v;});

            artist = new ReactiveProperty<string>(GetMetaDirective<ArtistDirective>()?.Text);
			artist.Subscribe(v => {if(!string.IsNullOrWhiteSpace(v))GetOrAddMetaDirective<ArtistDirective>().Text = v;});

            tuning = new ReactiveProperty<string>(GetMetaDirective<TuningDirective>()?.Text);
			tuning.Subscribe(v => {if(!string.IsNullOrWhiteSpace(v))GetOrAddMetaDirective<TuningDirective>().Text = v;});

            key = new ReactiveProperty<string>(GetMetaDirective<KeyDirective>()?.Text);
			key.Subscribe(v => {if(!string.IsNullOrWhiteSpace(v))GetOrAddMetaDirective<KeyDirective>().Text = v;});

            capo = new ReactiveProperty<string>(GetMetaDirective<CapoDirective>()?.Text);
			capo.Subscribe(v => {if(!string.IsNullOrWhiteSpace(v))GetOrAddMetaDirective<CapoDirective>().Text = v;});

        }

        public List<ILine> Lines { get; set; }

        //
        // Meta directives reactive models
        //

        public ReactiveProperty<string> title { get; private set; }
        public ReactiveProperty<string> subtitle { get; private set; }
        public ReactiveProperty<string> artist { get; private set; }
        public ReactiveProperty<string> key { get; private set; }
        public ReactiveProperty<string> tuning { get; private set; }
        public ReactiveProperty<string> capo { get; private set; }

        public T GetMetaDirective<T>() where T : Directive
        {
            var dir = (Lines.Where(d => d is T).FirstOrDefault() as T);
            //if (dir == null)
            //{
            //    dir = new T();
            //    Lines.Add(dir);
            //}
            return dir;
        }

		T GetOrAddMetaDirective<T>() where T : Directive, new()
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

		public string ToString()
        {
            return string.Join("\n", Lines.Select(l => l.ToString()));
        }
    }
}
