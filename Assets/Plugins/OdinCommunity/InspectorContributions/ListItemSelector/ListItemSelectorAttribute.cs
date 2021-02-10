using System;

namespace OdinCommunity.InspectorContributions
{
	public class ListItemSelectorAttribute : Attribute
	{
		public string SetSelectedMethod;

		public ListItemSelectorAttribute(string setSelectedMethod)
		{
			this.SetSelectedMethod = setSelectedMethod;
		}
	}
}
