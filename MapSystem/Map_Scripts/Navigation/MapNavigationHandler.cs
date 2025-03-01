using UnityEngine;

public class MapNavigationHandler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask mapTerrainMask;
    [SerializeField] private GameObject navigationIconPrefab;
    [SerializeField] private GameObject navigationObjectPrefab;

    private void Start()
    {
        if (navigationIconPrefab == null || navigationObjectPrefab == null)
        {
            Debug.LogError("Navigation prefabs are not assigned in MapNavigationHandler.");
            return;
        }

        NavigationMaterialHandler.Setup();
        NavigationMarkerFactory.Initialize(navigationIconPrefab, navigationObjectPrefab);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (TryGetClickPoint(out Vector3 point))
            {
                NavigationMarkerFactory.CreateMarker(point);
            }
        }
    }

    private bool TryGetClickPoint(out Vector3 point)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mapTerrainMask))
        {
            point = hit.point;
            return true;
        }

        point = Vector3.zero;
        return false;
    }
}
