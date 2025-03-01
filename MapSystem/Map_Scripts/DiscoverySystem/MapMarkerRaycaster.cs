using UnityEngine;

public class MapMarkerRaycaster : MonoBehaviour
{
    [SerializeField] private Camera targetCamera; // The camera used for raycasting
    [SerializeField] private LayerMask raycastLayerMask; // LayerMask to filter objects (optional)

    private MapMarker lastHoveredMarker; // To keep track of the last hovered marker

    private void Update()
    {
        // Perform a screen-to-world raycast
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayerMask))
        {
            // Check if the hit object is a MapMarker
            MapMarker currentMarker = hit.collider.GetComponent<MapMarker>();
            if (currentMarker != null)
            {
                // If this is a new marker, call OnMouseEnter
                if (currentMarker != lastHoveredMarker)
                {
                    HandleMouseExit();
                    lastHoveredMarker = currentMarker;
                    //currentMarker.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
                    currentMarker.MouseEnter();
                }
            }
            else
            {
                // If the ray hits something that isn't a MapMarker, handle exit
                HandleMouseExit();
            }
        }
        else
        {
            // If the ray doesn't hit anything, handle exit
            HandleMouseExit();
        }

        // Handle mouse down events (optional)
        if (Input.GetMouseButtonDown(0) && lastHoveredMarker != null)
        {
            //lastHoveredMarker.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
            lastHoveredMarker.MouseDown();
        }
    }

    private void HandleMouseExit()
    {
        if (lastHoveredMarker != null)
        {
            //lastHoveredMarker.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
            lastHoveredMarker.MouseExit();
            lastHoveredMarker = null;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a debug ray for visualization
        if (targetCamera != null)
        {
            Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * 100f);
        }
    }
}
