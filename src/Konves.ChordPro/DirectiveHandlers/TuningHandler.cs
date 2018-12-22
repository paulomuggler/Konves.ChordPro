using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class TuningHandler : DirectiveHandler<TuningDirective>
	{
        private TuningHandler() { }

        public static TuningHandler Instance { get; } = new TuningHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new TuningDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as TuningDirective) != null?(directive as TuningDirective).Text : null;
		}

        public override string LongName { get { return "meta: tuning"; } }
        public override string ShortName { get { return "tuning"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
