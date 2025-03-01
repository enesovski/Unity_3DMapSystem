using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    [Header("Compass UI Settings")]

    [SerializeField]
    private RawImage compassImage;

    private static Transform compassUIParent;

    [SerializeField]
    private Transform playerObject;

    [SerializeField]
    private MapUIDrawer mapUIDrawer;

    [Header("Icon Settings")]
    [SerializeField] 
    private GameObject markerPrefab;

    private List<MapObject> markers;

    private float COMPASS_UNIT;

    [SerializeField]
    private static GameObject compassIconPrefab;

    private void Awake()
    {
        EventManager.Instance.Register<OnPlayerSpawned>(SetPlayerObjectOnSpawn);
    }

    private void Start()
    {
        COMPASS_UNIT = compassImage.rectTransform.rect.width / 360f;

        MapDataHandler.onMapObjectsChanged += UpdateList;
        compassIconPrefab = markerPrefab;
        compassUIParent = compassImage.transform;
    }

    private void UpdateList()
    {
        markers = MapDataHandler.GetMapObjectsForCompass();
    }

    private void Update()
    {
        UpdateCompass();
    }

    private void UpdateCompass()
    {
        if (playerObject == null) return;

        compassImage.uvRect = new Rect(playerObject.localEulerAngles.y / 360f, 0f, 1f, 1f);

        if (markers != null)
        {
            foreach (MapObject marker in markers)
            {
                marker.GetImageForCompass().rectTransform.anchoredPosition = GetPosOnCompass(marker);
            }
        }
    }

    public static Image InstantiateImageOnCompass()
    {
        Image iconImage = Instantiate(compassIconPrefab, compassUIParent.transform).GetComponent<Image>();
        return iconImage;
    }


    private void SetPlayerObjectOnSpawn(OnPlayerSpawned onPlayerSpawned)
    {
        playerObject = onPlayerSpawned.playerData.gameObject.transform;

        EventManager.Instance.Unregister<OnPlayerSpawned>(SetPlayerObjectOnSpawn);
    }

    private Vector2 GetPosOnCompass(MapObject mapObject)
    {
        Vector2 playerPlanarForward = new Vector2(playerObject.transform.forward.x, playerObject.transform.forward.z);

        float angleBetween = Vector2.SignedAngle(GetPlanarVector(mapObject.PositionReference) - GetPlanarVector(playerObject), playerPlanarForward);

        return new Vector2(angleBetween * COMPASS_UNIT, 0f);
    }
    private Vector2 GetPlanarVector(Transform obj)
    {
        return new Vector2(obj.position.x, obj.position.z);
    }

}
