using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class ArtistHandler : DirectiveHandler<ArtistDirective>
	{
        private ArtistHandler() { }

        public static ArtistHandler Instance { get; } = new ArtistHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new ArtistDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as ArtistDirective) != null?(directive as ArtistDirective).Text : null;
		}

        public override string LongName { get { return "meta: artist"; } }
        public override string ShortName { get { return "artist"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
