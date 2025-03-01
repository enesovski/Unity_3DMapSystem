using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCameraController : MonoBehaviour
{
    [Header("Pan")]
    [SerializeField]
    private float panSpeed = 2f;

    [SerializeField]
    [Range(0f,1f)]
    private float panScreenLimit = .8f;

    [Header("Zoom")]
    [SerializeField] private float startZoom;

    [Space]
    [SerializeField]
    private float zoomSpeed = 3f;

    [SerializeField]
    private float zoomInMax = 40f;
    [SerializeField]
    private float zoomOutMax = 90f;

    [Header("Settings")]
    [SerializeField]
    private Transform cameraFollowTransform;

    [SerializeField]
    private float borderLimitX;

    [SerializeField]
    private float borderLimitZ;

    private Vector3 originalPosition;

    private CinemachineInputProvider inputProvider;
    private CinemachineFramingTransposer framingTransposer;

    private void Awake()
    {
        inputProvider = GetComponentInChildren<CinemachineInputProvider>();
        CinemachineVirtualCamera virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        framingTransposer.m_CameraDistance = startZoom;

        originalPosition = cameraFollowTransform.position;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        ResetCamera();
    }

    private void ResetCamera()
    {
        cameraFollowTransform.position = originalPosition;
    }

    private void LateUpdate()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);

        if(x != 0 || y != 0)
        {
            PanOnScreen(x, y);
        }

        if(z != 0)
        {
            ZoomOnScreen(z);
        }
    }

    private void ZoomOnScreen(float zoomAmount)
    {
        float currentZoom = framingTransposer.m_CameraDistance;
        float targetZoom= Mathf.Clamp(currentZoom + zoomAmount, zoomInMax, zoomOutMax);

        framingTransposer.m_CameraDistance = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed*Time.deltaTime);  
    }

    private void PanOnScreen(float x, float y)
    {
        Vector2 dir = GetPanDirection(x, y);

        Vector3 newPos = cameraFollowTransform.position +
            new Vector3(dir.x, 0, dir.y) * panSpeed;

        newPos.x = Mathf.Clamp(newPos.x, originalPosition.x - borderLimitX, originalPosition.x + borderLimitX);
        newPos.z = Mathf.Clamp(newPos.z, originalPosition.z - borderLimitZ, originalPosition.z + borderLimitZ);

        cameraFollowTransform.position = Vector3.Lerp(cameraFollowTransform.position, newPos,  Time.deltaTime);
    }


    //Bu fonksiyona optimizasyon gerekebilir;
    private Vector2 GetPanDirection(float x, float y)
    {
        Vector2 dir = Vector2.zero;

        if(Input.GetKey(KeyCode.W) || y >= Screen.height * panScreenLimit)
        {
            dir.y += 1;
        }
        else if(Input.GetKey(KeyCode.S) || y <= Screen.height * (1 - panScreenLimit))
        {
            dir.y -= 1;
        }

        if (Input.GetKey(KeyCode.D) || x >= Screen.width * panScreenLimit)
        {
            dir.x += 1;
        }
        else if (Input.GetKey(KeyCode.A) || x <= Screen.width * (1 - panScreenLimit))
        {
            dir.x -= 1;
        }

        return dir;
    }



}
