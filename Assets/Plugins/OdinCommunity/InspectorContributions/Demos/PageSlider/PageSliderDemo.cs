using Sirenix.OdinInspector;
using UnityEngine;

namespace OdinCommunity.InspectorContributions
{
	public class PageSliderDemo : MonoBehaviour
	{
		[ShowInInspector]
		[LabelText("$testName")]
		public static SomeData test = new SomeData();

		public string testName = "Title";
	}

	[PageSlider, HideReferenceObjectPicker]
	public class SomeData
	{
		[OnInspectorInit]
		private void InspectorInit()
		{
			a[0] = new SubData();
			a[1] = new SubData();
			a[2] = new SubData();
		}
		
		[ListDrawerSettings(Expanded = true)]
		public SubData[] a = new SubData[3];
		public int b, c, d;
		public SubData e = new SubData();
		public SubData f = new SubData();
		public SubData g = new SubData();

		[ListDrawerSettings(Expanded = true), InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Foldout, Expanded = true)]
		[PageSlider]
		public Material[] h = new Material[0];
		public int i, j, k;
	}

	[PageSlider, InlineProperty, HideReferenceObjectPicker]
	public class SubData
	{
		public int a;
		public float b;
		public SomeData c;
	}
}
