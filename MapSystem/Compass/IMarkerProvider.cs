using UnityEngine;

public interface IMarkerProvider
{
    void InitializeMarkers();
    void UpdateMarkers(Transform playerTransform, float compassUnit);
}
