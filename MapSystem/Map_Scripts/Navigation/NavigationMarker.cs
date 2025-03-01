using TMPro;
using UnityEngine;

public class NavigationMarker : MapMarker
{
    private const float Y_OFFSET = 4;

    private NavigationMapObject navigationObj;
    private SpriteRenderer currentSprite;
    private GameObject currentNavigationObject;
    private NavMaterial currentNavMaterial;

    public override void Setup(string iconName, MapObject mapObject)
    {
        base.Setup(iconName, mapObject);
        clickHandler += DeleteNavigationMarker;

    }
    public void Initialize(GameObject navigationPrefab, Vector3 position, NavMaterial material)
    {
        currentNavMaterial = material;

        if (currentNavMaterial == null)
        {
            Debug.LogError("No material available for navigation marker.");
            return;
        }

        currentSprite = GetComponentInChildren<SpriteRenderer>();
        if (currentSprite != null)
        {
            currentSprite.color = currentNavMaterial.GetColor();
        }

        CreateNavigationObject(navigationPrefab, position);
        Setup("Marker", navigationObj);
    }

    public override void MouseDown()
    {
        base.MouseDown();
        DeleteNavigationMarker();
    }

    private void CreateNavigationObject(GameObject navigationPrefab, Vector3 position)
    {
        Vector3 posOnRealMap = MapPositionCalculator.CalculatePositionForTerrain(transform);
        posOnRealMap.y += Y_OFFSET;

        currentNavigationObject = Instantiate(navigationPrefab, posOnRealMap, Quaternion.identity);
        if (currentNavigationObject.TryGetComponent(out MeshRenderer renderer))
        {
            renderer.material = currentNavMaterial.GetMaterial();
        }

        navigationObj = currentNavigationObject.GetComponentInChildren<NavigationMapObject>();
        currentNavMaterial.isUsing = true;

        if (navigationObj != null)
        {
            navigationObj.SetPositionReference(currentNavigationObject.transform);
        }
    }

    private void DeleteNavigationMarker()
    {
        currentNavMaterial.isUsing = false;

        MapDataHandler.RemoveMapObjectFromList(navigationObj);

        if (currentNavigationObject != null)
        {
            Destroy(currentNavigationObject);
        }

        Destroy(gameObject);
    }
}
