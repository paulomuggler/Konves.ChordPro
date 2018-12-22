using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class LyricistHandler : DirectiveHandler<LyricistDirective>
	{
        private LyricistHandler() { }

        public static LyricistHandler Instance { get; } = new LyricistHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new LyricistDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as LyricistDirective) != null?(directive as LyricistDirective).Text : null;
		}

        public override string LongName { get { return "meta: lyricist"; } }
        public override string ShortName { get { return "lyricist"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
