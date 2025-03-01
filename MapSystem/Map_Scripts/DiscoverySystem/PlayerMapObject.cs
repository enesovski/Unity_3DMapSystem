using UnityEngine;
public class PlayerMapObject : MapObject
{
    private void Start()
    {
        InitializeMapIcon();
        MapDataHandler.AddMapObjectToList(this);
    }

    public override void UpdateIcon()
    {
        AdjustIconPosition();
    }

    public override void AdjustIconPosition()
    {
        base.AdjustIconPosition();
        //iconOnMap.transform.eulerAngles = MapPositionCalculator.CalculateEulerRotation(PositionReference);
    }
}
