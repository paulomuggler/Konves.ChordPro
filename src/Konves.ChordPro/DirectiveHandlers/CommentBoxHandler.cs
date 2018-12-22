﻿using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class CommentBoxHandler : DirectiveHandler<CommentBoxDirective>
	{
		private CommentBoxHandler() { }

		public static CommentBoxHandler Instance { get; } = new CommentBoxHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentBoxDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
            return (directive as CommentBoxDirective) != null? (directive as CommentBoxDirective).Text : null;
		}

		public override string LongName { get { return "comment_box"; } }
		public override string ShortName { get { return "cb"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
