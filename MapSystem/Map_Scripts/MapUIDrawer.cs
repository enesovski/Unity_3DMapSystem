using Michsky.UI.Reach;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapUIDrawer : MonoBehaviour
{
    private List<MapUIElement> mapElements = new List<MapUIElement>();

    [Header("UI References")]
    [SerializeField] private Transform uiParent;
    [SerializeField] private GameObject mapUIElementPrefab;
    [SerializeField] private GameObject explanationParent;
    [SerializeField] private ButtonManager navigationMarkerButton;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text explanation;
    [SerializeField] private TMP_Text mapRegionHeader;

    MapNotificationHandler notificationHandler;
    private MapObject currentData;

    private void Start()
    {
        notificationHandler = GetComponent<MapNotificationHandler>();
    }

    public void UpdateRegion(MapRegion region)
    {
        mapRegionHeader.SetText(region.regionName);
    }

    public void EnableUI()
    {
        DisableExplanation();
    }

    public void UpdateUI()
    {
        foreach (MapObject obj in MapDataHandler.GetMapObjectsForUI())
        {
            if (!obj.isInitializedForUI && obj.isDiscovered && obj.mapData.displayOnUI)
            {
                CreateIconForUI(obj);
            }
            if (obj.isInitializedForUI)
            {
                obj.UpdateIcon();
            }
        }
    }

    private void CreateIconForUI(MapObject obj)
    {
        MapUIElement element = Instantiate(mapUIElementPrefab, uiParent).GetComponent<MapUIElement>();
        element.Setup(obj, this);
        mapElements.Add(element);
        obj.isInitializedForUI = true;
    }

    public void SelectObject(MapObject obj)
    {
        EnableExplanation();

        currentData = obj;
        MapIconData mapIconData = currentData.mapData;

        header.SetText(mapIconData.iconName);
        explanation.SetText(mapIconData.explanation);
    }

    private void EnableExplanation()
    {
        explanationParent.SetActive(true);
    }

    private void DisableExplanation()
    {
        explanationParent.SetActive(false);
    }

    public void ShowNotification(string message)
    {
        notificationHandler.DisplayDiscoveryDescription(message);
    }
}
