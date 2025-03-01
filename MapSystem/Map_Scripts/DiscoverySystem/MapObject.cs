using UnityEngine;
using UnityEngine.UI;

public abstract class MapObject : MonoBehaviour, ICompassTrackable, IMapTrackable, IDiscoverable
{
    [Header("Map Object Settings")]
    [SerializeField] public MapIconData mapData;
    [SerializeField] private Transform positionReference;

    protected GameObject iconOnMap; 
    protected MapMarker iconReference; 
    private bool isInitialized; 
    private Image compassIconMarker;

    public bool isInitializedForUI = false;
    public bool isDiscovered = false;

    public Image CompassIconMarker => compassIconMarker;

    public Transform PositionReference { get => positionReference; protected set => positionReference = value; }

    public Image GetImageForCompass()
    {
        return CompassIconMarker;
    }
    public bool IsInitialized()
    {
        return isInitialized;
    }

    public void InitializeMapIcon()
    {
        if (isInitialized || mapData == null) return;

        Vector3 posOnMap = MapPositionCalculator.CalculatePosition(PositionReference);
        iconOnMap = Instantiate(mapData.mapPrefab, posOnMap, Quaternion.identity);

        iconReference = iconOnMap.GetComponent<MapMarker>();
        if (iconReference != null)
        {
            iconReference.Setup(mapData.iconName, this);
        }

        isInitialized = true;
        AdjustIconPosition();
        SetupCompassMarker();
    }

    public virtual void UpdateIcon()
    {
        if (!isInitialized)
        {
            InitializeMapIcon();
        }
    }

    public virtual void AdjustIconPosition()
    {
        if (iconOnMap == null) return;

        Vector3 posOnMap = MapPositionCalculator.CalculatePosition(PositionReference);
        iconOnMap.transform.position = posOnMap;
    }

    public void DiscoverObject()
    {
        isDiscovered = true;
        FindObjectOfType<MapUIDrawer>().ShowNotification(mapData.iconName + " discovered.");
    }

    public void EnableIcon()
    {
        if (iconOnMap != null)
        {
            iconOnMap.SetActive(true);
        }
    }

    public void DisableIcon()
    {
        if (iconOnMap != null)
        {
            iconOnMap.SetActive(false);
        }
    }

    protected void SetupCompassMarker()
    {
        if (mapData == null)     
            return;

        if(!mapData.displayOnCompass || mapData.iconSprite == null)
            return;

        //compassIconMarker = Compass.InstantiateImageOnCompass();

        //if (compassIconMarker != null)
        //{
        //    compassIconMarker.sprite = mapData.iconSprite;
        //}
    }

}
