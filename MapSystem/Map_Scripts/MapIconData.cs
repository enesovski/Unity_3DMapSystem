using UnityEngine;

[CreateAssetMenu(fileName = "NewMapIconData", menuName = "Map/MapIconData", order = 1)]
public class MapIconData : ScriptableObject
{
    [Header("Icon Settings")]
    [Tooltip("The name of the icon (used as a reference).")]
    public string iconName;

    [Tooltip("The sprite used for the map icon.")]
    public Sprite iconSprite;

    public string explanation;

    [Tooltip("The prefab used for the map icon.")]
    public GameObject mapPrefab;

    [Header("Display Settings")]
    [Tooltip("Should this icon be displayed on the UI?")]
    public bool displayOnUI = true;

    [Tooltip("Should this icon appear on the compass?")]
    public bool displayOnCompass = true;

    [Tooltip("The priority of this icon (used for sorting). Lower values = higher priority.")]
    public int priority;
}
