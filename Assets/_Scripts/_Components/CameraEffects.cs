using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEffects : MonoBehaviour
{
  [Header("Edge settings")]
  [SerializeField] private Shader edgeShader;
  [SerializeField] private Color borderColor;
  [SerializeField, Range(0f, 3f)] private float edgeSize;

  [Header("Toon settings")]
  [SerializeField] private Shader toonShader;
  [SerializeField, Range(2, 12)] private int colorsPerChannel;

  private Material edgeMat;
  private Material toonMat;

  private RenderTexture[] buffer = new RenderTexture[2];

  void Start()
  {
    edgeMat = edgeMat != null ? edgeMat : new Material(edgeShader);
    toonMat = toonMat != null ? toonMat : new Material(toonShader);


    Camera cam = GetComponent<Camera>();
    cam.depthTextureMode = DepthTextureMode.Depth;
  }

  void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    edgeMat.SetColor("_BorderColor", borderColor);
    edgeMat.SetFloat("_EdgeSize", edgeSize);

    toonMat.SetInt("_ColorsPerChannel", colorsPerChannel);

    if (!buffer[0])
      buffer[0] = new(source);
    if (!buffer[1])
    buffer[1] = new(source);

    Graphics.Blit(source, buffer[0], edgeMat);
    Graphics.Blit(buffer[0], buffer[1], toonMat);
    Graphics.Blit(buffer[1], destination);
  }
}
