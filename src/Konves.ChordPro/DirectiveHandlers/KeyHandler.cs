using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class KeyHandler : DirectiveHandler<KeyDirective>
	{
        private KeyHandler() { }

        public static KeyHandler Instance { get; } = new KeyHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new KeyDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as KeyDirective) != null?(directive as KeyDirective).Text : null;
		}

        public override string LongName { get { return "meta: key"; } }
        public override string ShortName { get { return "key"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
