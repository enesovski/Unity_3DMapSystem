using System.Collections.Generic;
using System.Linq;
public class MapDataHandler
{
    private static List<MapObject> mapObjects = new List<MapObject>();

    public delegate void OnMapObjectsChanged();
    public static OnMapObjectsChanged onMapObjectsChanged;

    public static void AddMapObjectToList(MapObject newMapObj)
    {
        mapObjects.Add(newMapObj);

        //Sorta optimizasyon lazým
        mapObjects = mapObjects.OrderBy(o => -o.mapData.priority).ToList();
        onMapObjectsChanged?.Invoke();
    }

    public static void RemoveMapObjectFromList(MapObject oldMapObj)
    {
        mapObjects.Remove(oldMapObj);
        onMapObjectsChanged?.Invoke();
    }

    public static List<MapObject> GetList()
    {
        return mapObjects;
    }

    public static List<MapObject> GetMapObjectsForUI()
    {
        return mapObjects.Where(o => o.mapData.displayOnUI).ToList();
    }

    public static List<MapObject> GetMapObjectsForCompass()
    {
        return mapObjects.Where(o => o.mapData.displayOnCompass).ToList();
    }

}
