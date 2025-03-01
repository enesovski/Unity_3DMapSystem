using System.Collections;
using UnityEngine;

public class PlayerDiscoverer : MonoBehaviour, IDiscoverer
{
    private const int RENDERTEXTURE_SIZE = 1024;

    [Header("Settings")]
    [SerializeField] private float updateTimeInterval = 2f;

    [Header("Splatmap Properties")]
    [Space]
    public float fogRevealRadius = 10f;

    public float fogFallOff = 2f;
    [Range(0f, 1f)]
    public float fogRevealStrength = 0.8f;

    [SerializeField] private Shader splatmapShader;
    [SerializeField] private Material fogMaterial;

    private RenderTexture splatmapTexture;
    private Material splatmapMaterial;

    private void Start()
    {
        splatmapMaterial = new Material(splatmapShader);
        splatmapMaterial.SetVector("_BrushColor", Color.white);
        splatmapMaterial.SetFloat("_BrushSize", fogRevealRadius);
        splatmapMaterial.SetFloat("_BrushStrength", fogRevealStrength);
        splatmapMaterial.SetFloat("_BrushFalloff", fogFallOff);

        splatmapTexture = new RenderTexture(RENDERTEXTURE_SIZE, RENDERTEXTURE_SIZE, 0, RenderTextureFormat.ARGBFloat);
        splatmapTexture.enableRandomWrite = true;

        splatmapTexture.Create();

        fogMaterial = GameObject.Find("MapFog").GetComponent<MeshRenderer>().sharedMaterial;

        if (fogMaterial != null)
        {
            fogMaterial.SetTexture("_Splatmap", splatmapTexture);
        }

        StartCoroutine(UpdateFog());
    }

    IEnumerator UpdateFog()
    {
        while (true)
        {
            Vector4 uvPos = MapPositionCalculator.CalculateUVPosition(transform, RENDERTEXTURE_SIZE);
            drawOnSplatmap(uvPos);
            yield return new WaitForSeconds(updateTimeInterval);
        }
    }

    private void drawOnSplatmap(Vector4 drawPos)
    {
        //Debug.Log(drawPos);

        splatmapMaterial.SetVector("_Coordinates", drawPos);

        RenderTexture tempTexture = RenderTexture.GetTemporary(RENDERTEXTURE_SIZE, RENDERTEXTURE_SIZE, 0, RenderTextureFormat.ARGBFloat);

        Graphics.Blit(splatmapTexture, tempTexture);

        Graphics.Blit(tempTexture, splatmapTexture, splatmapMaterial);

        RenderTexture.ReleaseTemporary(tempTexture);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDiscoverable discovered))
        {
            Discover(other.GetComponent<MapObject>());
        }
    }

    public void Discover(MapObject discoveredObj)
    {
        if(!discoveredObj.isDiscovered)
        {
            discoveredObj.DiscoverObject();
        }
    }

    private void OnDestroy()
    {
        if (splatmapTexture != null)
        {
            splatmapTexture.Release();
        }
    }

}
