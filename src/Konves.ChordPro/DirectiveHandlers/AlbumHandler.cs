using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class AlbumHandler : DirectiveHandler<AlbumDirective>
	{
        private AlbumHandler() { }

        public static AlbumHandler Instance { get; } = new AlbumHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new AlbumDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as AlbumDirective) != null?(directive as AlbumDirective).Text : null;
		}

        public override string LongName { get { return "meta: album"; } }
        public override string ShortName { get { return "album"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
