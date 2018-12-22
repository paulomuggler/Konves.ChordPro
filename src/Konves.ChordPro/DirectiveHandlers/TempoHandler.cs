using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class TempoHandler : DirectiveHandler<TempoDirective>
	{
        private TempoHandler() { }

        public static TempoHandler Instance { get; } = new TempoHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new TempoDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as TempoDirective) != null?(directive as TempoDirective).Text : null;
		}

        public override string LongName { get { return "meta: tempo"; } }
        public override string ShortName { get { return "tempo"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
