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

  // private RenderTexture[] buffer = new RenderTexture[2];

  void Start()
  {
    edgeMat = edgeMat != null ? edgeMat : new Material(edgeShader);
    toonMat = toonMat != null ? toonMat : new Material(toonShader);
  }
}
