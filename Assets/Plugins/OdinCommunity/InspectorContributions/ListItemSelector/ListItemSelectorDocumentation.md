# List Item Selector
Author: Bjarke
Shared: 22-05-2019

Makes items in a list selectable.

# Usage
```cs
[ListItemSelector("SetSelected")]
public List<Material> SomeList = new List<Material>();

[BoxGroup("Preview")]
[ShowInInspector, InlineEditor(InlineEditorObjectFieldModes.Hidden)]
private Material selectedMaterial;

public void SetSelected(int index)
{
    this.selectedMaterial = index > 0 ? this.SomeList[index] : null;
}
```