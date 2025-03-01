using UnityEngine;

public class MapPositionCalculator
{
    private static Transform mainTerrain;
    private static Transform mapTerrain;
    private static float terrainScaleMultiplier;

    public static void SetTerrainReferences(Transform _mainTerrain, Transform _mapTerrain)
    {
        mainTerrain = _mainTerrain; ;
        mapTerrain = _mapTerrain;

        float terrainScale = mainTerrain.GetComponent<Terrain>().terrainData.size.x;
        float projectTerrainScale = mapTerrain.GetComponent<Terrain>().terrainData.size.x;

        terrainScaleMultiplier = projectTerrainScale / terrainScale;

    }

    public static Vector4 CalculateUVPosition(Transform obj, int textureSize)
    {

        Vector3 relativePos = (obj.position - mainTerrain.position) / (float)textureSize;


        Vector4 uvPos = new Vector4(Mathf.Abs(relativePos.x), Mathf.Abs(relativePos.z), 0, 0);
        return uvPos;
        
    }

    public static Vector3 CalculatePosition(Transform obj)
    {
        Vector3 relativePos = obj.position - mainTerrain.position;


        Vector3 finalPos = mapTerrain.position + relativePos * terrainScaleMultiplier;

        return finalPos;
    }

    public static Vector3 CalculatePositionForTerrain(Transform obj)
    {
        Vector3 relativePos = new Vector3(obj.position.x - mapTerrain.position.x,
            obj.position.y - mapTerrain.position.y,
            obj.position.z - mapTerrain.position.z);


        Vector3 finalPos = mainTerrain.position + relativePos * 1 / terrainScaleMultiplier;

        return finalPos;
    }


    public static Vector3 CalculateEulerRotation(Transform obj)
    {
        Vector3 eulerRot = obj.eulerAngles;

        return eulerRot;
    }
    public static Vector2 GetPlanarVector(Vector3 v3)
    {
        Vector2 planar = new Vector2(v3.x, v3.z);

        return planar;
    }

}
