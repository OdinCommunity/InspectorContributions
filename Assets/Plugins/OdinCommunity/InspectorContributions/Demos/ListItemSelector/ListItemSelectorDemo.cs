using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinCommunity.InspectorContributions.Demos
{
	public class ListItemSelectorDemo : MonoBehaviour
	{
		[InfoBox("Click on the list items below!")]
		[ShowInInspector, AssetsOnly, ListItemSelector("SetSelected"), ListDrawerSettings(Expanded = true)]
		private List<Sprite> ListOfSprites = new List<Sprite>();

		[BoxGroup("Selected Item")]
		[ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Hidden), HideLabel, HideReferenceObjectPicker]
		private Sprite Selected;

		public void SetSelected(int index)
		{
			Selected = index >= 0 ? ListOfSprites[index] : null;
		}
	}

	
}
