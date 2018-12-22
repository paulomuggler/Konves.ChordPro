
//using System.Threading.Tasks;

namespace Konves.ChordPro.Directives
{
	public sealed class PageTypeDirective : Directive
	{
		public PageTypeDirective(PageType pageType)
		{
			PageType = pageType;
		}

		public PageType PageType { get; set; }
	}
}
