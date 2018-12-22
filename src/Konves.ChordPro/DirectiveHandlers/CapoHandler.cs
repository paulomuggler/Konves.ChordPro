using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class CapoHandler : DirectiveHandler<CapoDirective>
	{
        private CapoHandler() { }

        public static CapoHandler Instance { get; } = new CapoHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new CapoDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as CapoDirective) != null?(directive as CapoDirective).Text : null;
		}

        public override string LongName { get { return "meta: capo"; } }
        public override string ShortName { get { return "capo"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
