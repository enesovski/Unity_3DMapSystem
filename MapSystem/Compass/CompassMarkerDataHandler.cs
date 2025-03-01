using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassMarkerProvider : IMarkerProvider
{
    private readonly RawImage compassImage;
    private readonly GameObject markerPrefab;
    private List<MapObject> markers;

    public CompassMarkerProvider(RawImage compassImage, GameObject markerPrefab)
    {
        this.compassImage = compassImage;
        this.markerPrefab = markerPrefab;
    }

    public void InitializeMarkers()
    {
        markers = MapDataHandler.GetMapObjectsForCompass();
    }

    public void UpdateMarkers(Transform playerTransform, float compassUnit)
    {
        if (markers == null) return;

        foreach (var marker in markers)
        {
            var position = CalculateMarkerPosition(marker, playerTransform, compassUnit);
            marker.GetImageForCompass().rectTransform.anchoredPosition = position;
        }
    }

    private Vector2 CalculateMarkerPosition(MapObject marker, Transform playerTransform, float compassUnit)
    {
        Vector2 playerForward = new Vector2(playerTransform.forward.x, playerTransform.forward.z);
        Vector2 playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.z);
        Vector2 markerPosition = new Vector2(marker.PositionReference.position.x, marker.PositionReference.position.z);

        float angle = Vector2.SignedAngle(markerPosition - playerPosition, playerForward);
        return new Vector2(angle * compassUnit, 0f);
    }
}
