using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class TimeHandler : DirectiveHandler<TimeDirective>
	{
        private TimeHandler() { }

        public static TimeHandler Instance { get; } = new TimeHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new TimeDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as TimeDirective) != null?(directive as TimeDirective).Text : null;
		}

        public override string LongName { get { return "meta: time"; } }
        public override string ShortName { get { return "time"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
