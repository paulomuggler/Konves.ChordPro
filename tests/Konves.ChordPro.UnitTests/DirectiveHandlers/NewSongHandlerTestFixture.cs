﻿using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class NewSongHandlerTestFixture : KeyOnlyBaseTestFixture<NewSongDirective>
	{
		public NewSongHandlerTestFixture() : base("{new_song}", "{ns}", NewSongHandler.Instance) { }

		[TestMethod]
		public override void TryParseTest_LongForm() { base.TryParseTest_LongForm(); }

		[TestMethod]
		public override void TryParseTest_ShortForm() { base.TryParseTest_ShortForm(); }

		[TestMethod]
		public override void GetStringTest_LongForm() { base.GetStringTest_LongForm(); }

		[TestMethod]
		public override void GetStringTest_ShortForm() { base.GetStringTest_ShortForm(); }
	}
}