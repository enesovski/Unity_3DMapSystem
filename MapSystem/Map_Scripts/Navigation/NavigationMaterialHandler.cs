using System.Linq;
using UnityEngine;

public static class NavigationMaterialHandler
{
    private static NavMaterial[] navMaterials;

    public static void Setup()
    {
        var materials = Resources.LoadAll<Material>("NavigationMaterials");
        navMaterials = materials.Select(mat => new NavMaterial(mat)).ToArray();
    }

    public static NavMaterial GetMaterial()
    {
        return navMaterials.FirstOrDefault(mat => !mat.isUsing);
    }
}

public class NavMaterial
{
    private readonly Material material;

    public bool isUsing { get; set; }

    public NavMaterial(Material material)
    {
        this.material = material;
    }

    public Material GetMaterial() => material;

    public Color GetColor() => material.color;
}
