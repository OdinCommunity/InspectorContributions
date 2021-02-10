using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace OdinCommunity.InspectorContributions
{
	[DrawerPriority(0.01)]
	public class ListItemSelectorAttributeDrawer : OdinAttributeDrawer<ListItemSelectorAttribute>
	{
		private static readonly Color selectedColor = new Color(0.301f, 0.563f, 1f, 0.497f);
		private bool isListElement;
		private InspectorProperty baseMemberProperty;
		private PropertyContext<InspectorProperty> globalSelectedProperty;
		private InspectorProperty selectedProperty;
		private Action<object, int> selectedIndexSetter;

		protected override void Initialize()
		{
			isListElement = Property.Parent != null && Property.Parent.ChildResolver is IOrderedCollectionResolver;
			bool isList = !isListElement;
			InspectorProperty listProperty = isList ? Property : Property.Parent;
			baseMemberProperty = listProperty.FindParent(x => x.Info.PropertyType == PropertyType.Value, true);
			globalSelectedProperty = baseMemberProperty.Context.GetGlobal
				("selectedIndex" + baseMemberProperty.GetHashCode(), (InspectorProperty) null);

			if (isList)
			{
				Type parentType = baseMemberProperty.ParentValues[0].GetType();
				selectedIndexSetter = EmitUtilities.CreateWeakInstanceMethodCaller<int>
					(parentType.GetMethod(Attribute.SetSelectedMethod, Flags.AllMembers));
			}
		}

		protected override void DrawPropertyLayout(GUIContent label)
		{
			EventType t = Event.current.type;

			if (isListElement)
			{
				if (t == EventType.Layout)
				{
					CallNextDrawer(label);
				}
				else
				{
					Rect rect = GUIHelper.GetCurrentLayoutRect();
					bool isSelected = globalSelectedProperty.Value == Property;

					if (t == EventType.Repaint && isSelected)
					{
						EditorGUI.DrawRect(rect, selectedColor);
					}
					else if (t == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
					{
						globalSelectedProperty.Value = Property;
					}

					CallNextDrawer(label);
				}
			}
			else
			{
				CallNextDrawer(label);

				if (Event.current.type != EventType.Layout)
				{
					InspectorProperty sel = globalSelectedProperty.Value;

					// Select
					if (sel != null && sel != selectedProperty)
					{
						selectedProperty = sel;
						Select(selectedProperty.Index);
					}
					// Deselect when destroyed
					else if (selectedProperty != null && selectedProperty.Index < Property.Children.Count &&
					         selectedProperty != Property.Children[selectedProperty.Index])
					{
						int index = -1;
						Select(index);
						selectedProperty = null;
						globalSelectedProperty.Value = null;
					}
				}
			}
		}

		private void Select(int index)
		{
			GUIHelper.RequestRepaint();
			Property.Tree.DelayAction
			(
				() =>
				{
					for (int i = 0; i < baseMemberProperty.ParentValues.Count; i++)
					{
						selectedIndexSetter(baseMemberProperty.ParentValues[i], index);
					}
				}
			);
		}
	}
}