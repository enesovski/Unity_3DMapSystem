using UnityEngine;

public class NavigationMapObject : MapObject
{
    private void Start()
    {
        PositionReference ??= transform;
        SetupCompassMarker();
    }

    public override void AdjustIconPosition()
    {
        base.AdjustIconPosition();
    }

    public void SetPositionReference(Transform posReference)
    {
        PositionReference = posReference;
    }
}
