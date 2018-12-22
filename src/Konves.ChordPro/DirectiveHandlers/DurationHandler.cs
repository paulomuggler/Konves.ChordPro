using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class DurationHandler : DirectiveHandler<DurationDirective>
	{
        private DurationHandler() { }

        public static DurationHandler Instance { get; } = new DurationHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new DurationDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as DurationDirective) != null?(directive as DurationDirective).Text : null;
		}

        public override string LongName { get { return "meta: duration"; } }
        public override string ShortName { get { return "duration"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
