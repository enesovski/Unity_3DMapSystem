using UnityEngine;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private AudioClip mapInputSound;
    private AudioSource soundPlayer;

    [Header("UI References")]
    [SerializeField] private GameObject mapDisplayParent;
    [SerializeField] private GameObject mapUIParent;

    [Header("Terrain References")]
    [SerializeField] private Transform mainTerrain;
    [SerializeField] private Transform mapTerrain;
    [SerializeField] private GameObject mapCameraParent;
    private Camera playerCamera;
    public Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }

    private bool isOpen = false;
    private MapUIDrawer mapUIDrawer;

    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        mapUIDrawer = GetComponentInChildren<MapUIDrawer>();
        MapDataHandler.onMapObjectsChanged += mapUIDrawer.UpdateUI;
        MapPositionCalculator.SetTerrainReferences(mainTerrain, mapTerrain);

    }

    private void Start()
    {
        EventManager.Instance.Register<OnPlayerSpawned>(OnPlayerSpawned);
    }
    public void OnMapInput()
    {
        if (!isOpen) EnableMap();
        else DisableMap();
    }
    public void EnableMap()
    {
        soundPlayer.PlayOneShot(mapInputSound);
        mapUIParent.SetActive(true);
        mapDisplayParent.SetActive(true);
        mapCameraParent.SetActive(true);
        //PlayerCamera.SetActive(false);
        playerCamera.enabled = false;

        mapUIDrawer.EnableUI();
        mapUIDrawer.UpdateUI();

        isOpen = true;
    }

    public void DisableMap()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mapUIParent.SetActive(false);
        mapDisplayParent.SetActive(false);
        mapCameraParent.SetActive(false);
        //PlayerCamera.SetActive(true);
        playerCamera.enabled = true;

        isOpen = false;
    }

    private void OnPlayerSpawned(OnPlayerSpawned e)
    {
        PlayerCamera = e.playerData.PlayerCam;
    }

    private void OnDestroy()
    {
        MapDataHandler.onMapObjectsChanged -= mapUIDrawer.UpdateUI;

        EventManager.Instance.Unregister<OnPlayerSpawned>(OnPlayerSpawned);
    }
}

