using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class YearHandler : DirectiveHandler<YearDirective>
	{
        private YearHandler() { }

        public static YearHandler Instance { get; } = new YearHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new YearDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as YearDirective) != null?(directive as YearDirective).Text : null;
		}

        public override string LongName { get { return "meta: album"; } }
        public override string ShortName { get { return "album"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
