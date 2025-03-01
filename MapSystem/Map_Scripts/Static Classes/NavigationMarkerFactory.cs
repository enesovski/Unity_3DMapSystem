using UnityEngine;

public static class NavigationMarkerFactory
{
    private static GameObject navigationIconPrefab;
    private static GameObject navigationObjectPrefab;

    public static void Initialize(GameObject iconPrefab, GameObject objectPrefab)
    {
        navigationIconPrefab = iconPrefab;
        navigationObjectPrefab = objectPrefab;
    }

    public static void CreateMarker(Vector3 position)
    {
        if (navigationIconPrefab == null || navigationObjectPrefab == null)
        {
            return;
        }

        var iconInstance = Object.Instantiate(navigationIconPrefab, position, Quaternion.identity);

        var navigationMarker = iconInstance.GetComponent<NavigationMarker>();
        if (navigationMarker == null)
        {
            return;
        }

        var material = NavigationMaterialHandler.GetMaterial();
        if (material == null)
        {
            return;
        }

        var navigationObj = iconInstance.AddComponent<NavigationMapObject>();
        navigationMarker.Initialize(navigationObjectPrefab, position, material);
        navigationMarker.Setup(material.GetMaterial().name, navigationObj);
    }
}
