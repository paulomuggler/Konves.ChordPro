using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
    public sealed class ComposerHandler : DirectiveHandler<ComposerDirective>
	{
        private ComposerHandler() { }

        public static ComposerHandler Instance { get; } = new ComposerHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
            directive = new ComposerDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as ComposerDirective) != null?(directive as ComposerDirective).Text : null;
		}

        public override string LongName { get { return "meta: composer"; } }
        public override string ShortName { get { return "composer"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
