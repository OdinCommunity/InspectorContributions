using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OdinCommunity.InspectorContributions
{
	[DrawerPriority(0, 200)]
	public class PageSliderAttributeDrawer : OdinAttributeDrawer<PageSliderAttribute>
	{
		private static GUIStyle titleStyle;
		private static SlidePageNavigationHelper<InspectorProperty> currentSlider;
		private static InspectorProperty currentDrawingPageProperty;
		private SlidePageNavigationHelper<InspectorProperty> slider;
		private SlidePageNavigationHelper<InspectorProperty>.Page page;
		private GUIContent pageLabel;

		protected override bool CanDrawAttributeProperty
			(InspectorProperty property) => !(property.ChildResolver is ICollectionResolver);

		protected override void Initialize()
		{
			titleStyle = titleStyle ?? new GUIStyle("ShurikenModuleTitle");
		}

		protected override void DrawPropertyLayout(GUIContent label)
		{
			if (Property.ValueEntry.WeakSmartValue == null)
			{
				CallNextDrawer(label);
				return;
			}

			UpdateBreadcrumbLabel(label);

			if (currentSlider == null)
			{
				DrawPageSlider(label);
			}
			else if (currentDrawingPageProperty == Property)
			{
				CallNextDrawer(null);
			}
			else
			{
				if (GUILayout.Button(new GUIContent(GetLabelText(label)), titleStyle))
				{
					currentSlider.PushPage(Property, Guid.NewGuid().ToString());
					currentSlider.EnumeratePages.Last();
					page = currentSlider.EnumeratePages.Last();
					page.Name = GetLabelText(label);
					pageLabel = label;
				}
			}
		}

		private void UpdateBreadcrumbLabel(GUIContent label)
		{
			if (Event.current.type != EventType.Layout)
			{
				return;
			}

			if (page == null)
			{
				return;
			}

			if (pageLabel != null && pageLabel != Property.Label)
			{
				return;
			}

			string newLabel = GetLabelText(label ?? pageLabel);

			if (newLabel != page.Name)
			{
				page.Name = newLabel;
				page.GetType().GetField("TitleWidth", Flags.AllMembers)?.SetValue(page, null);
			}
		}

		private void DrawPageSlider(GUIContent label)
		{
			try
			{
				if (slider == null)
				{
					slider = new SlidePageNavigationHelper<InspectorProperty>();
					slider.PushPage(Property, Guid.NewGuid().ToString());
					page = slider.EnumeratePages.Last();
					page.Name = GetLabelText(label);
				}

				currentSlider = slider;

				SirenixEditorGUI.BeginBox();
				SirenixEditorGUI.BeginToolbarBoxHeader();
				{
					Rect rect = GUILayoutUtility.GetRect(0, 20);
					rect.x -= 5;
					slider.DrawPageNavigation(rect);
				}
				SirenixEditorGUI.EndToolbarBoxHeader();
				{
					slider.BeginGroup();
					foreach (SlidePageNavigationHelper<InspectorProperty>.Page p in slider.EnumeratePages)
					{
						if (p.BeginPage())
						{
							if (p.Value == Property)
							{
								CallNextDrawer(null);
							}
							else
							{
								currentDrawingPageProperty = p.Value;
								if (p.Value.Tree != Property.Tree)
								{
									p.Value.Tree.BeginDraw(true);
								}

								p.Value.Draw(null);

								if (p.Value.Tree != Property.Tree)
								{
									p.Value.Tree.EndDraw();
								}

								currentDrawingPageProperty = null;
							}
						}

						p.EndPage();
					}

					slider.EndGroup();
				}
				SirenixEditorGUI.EndBox();
			}
			finally
			{
				currentSlider = null;
			}
		}

		private string GetLabelText(GUIContent label)
		{
			if (label != null)
			{
				return label.text;
			}

			object val = Property.ValueEntry.WeakSmartValue;
			if (val == null)
			{
				return "Null";
			}

			Object unityObject = val as Object;
			if (unityObject)
			{
				if (string.IsNullOrEmpty(unityObject.name))
				{
					return unityObject.ToString();
				}

				return unityObject.name;
			}

			return val.ToString();
		}
	}
}