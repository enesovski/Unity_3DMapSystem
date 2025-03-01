using UnityEngine;
public class GenericMapMarker : MapMarker, ISelectableMarker
{
    public override void Setup(string iconName, MapObject mapObject)
    {
        base.Setup(iconName, mapObject);

        if (nameText != null)
        {
            nameText.text = iconName;
        }

        clickHandler += OnSelect;
    }

    public void OnSelect()
    {
        Debug.Log(transform.name + " selected.");
        var uiDrawer = FindObjectOfType<MapUIDrawer>();
        if (uiDrawer != null && linkedMapObject != null)
        {
            uiDrawer.SelectObject(linkedMapObject);
        }
    }
}
