﻿using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class DefineHandler : DirectiveHandler<DefineDirective>
	{
		private DefineHandler() { }

		public static DefineHandler Instance { get; } = new DefineHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new DefineDirective(components.SubKey, components.Value); //TODO: allow various formats
			return true;
		}

		protected override string GetSubKey(Directive directive)
		{
            return (directive as DefineDirective) != null? (directive as DefineDirective).Chord : null;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as DefineDirective) != null? (directive as DefineDirective).Definition : null;
		}

		public override string LongName { get { return "define"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.Required; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
