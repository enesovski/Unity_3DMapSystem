using Michsky.UI.Reach;
using UnityEngine;

public class MapUIElement : MonoBehaviour
{
    [SerializeField]
    ButtonManager buttonManager;
    private MapObject mapObj;
    private MapUIDrawer mapUIDrawer;

    public void Setup(MapObject obj, MapUIDrawer drawer)
    {
        mapObj = obj;
        mapUIDrawer = drawer;

        buttonManager.SetText(mapObj.mapData.iconName);
        buttonManager.onClick.AddListener(delegate { mapUIDrawer.SelectObject(mapObj); });
    }

    public ButtonManager GetButton()
    {
        return buttonManager;
    }
}
