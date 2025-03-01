using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class GenericMapObject : MapObject
{
    [Header("Discoverable Settings")]
    [SerializeField] private float discoverRadius = 50f;

    protected virtual void Start()
    {
        PositionReference ??= transform;
        MapDataHandler.AddMapObjectToList(this);
    }

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = discoverRadius;
    }
}
