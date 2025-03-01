using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapRegionController : MonoBehaviour
{
    [SerializeField]
    private List<MapRegionData> regions;

    private delegate void OnRegionChanged(MapRegion region);
    private OnRegionChanged onRegionChanged;

    private MapUIDrawer uiDrawer;

    private int currentDisplayedRegion = 0;

    private void Start()
    {
        onRegionChanged += ViewRegion;
        uiDrawer = GetComponent<MapUIDrawer>();
        onRegionChanged += uiDrawer.UpdateRegion;

        SwitchInOrder(1);
    }

    public void OnRegionDisplayIncrease(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SwitchInOrder(1);
        }
    }

    public void OnRegionDisplayDecrease(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchInOrder(-1);
        }

    }


    public void SwitchInOrder(int increase)
    {
        currentDisplayedRegion += increase;
        currentDisplayedRegion = currentDisplayedRegion % (regions.Count);
        onRegionChanged?.Invoke(regions[currentDisplayedRegion].region);
    }

    private void ViewRegion(MapRegion region)
    {

    }

    public void SelectRegion(MapRegion region)
    {

    }

}

[System.Serializable]
public struct MapRegionData
{
    public MapRegion region;
    public Material mapFog;
}