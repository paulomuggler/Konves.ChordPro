using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class CopyrightHandler : DirectiveHandler<CopyrightDirective>
	{
        private CopyrightHandler() { }

        public static CopyrightHandler Instance { get; } = new CopyrightHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new CopyrightDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as CopyrightDirective) != null?(directive as CopyrightDirective).Text : null;
		}

        public override string LongName { get { return "meta: copyright"; } }
        public override string ShortName { get { return "copyright"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
