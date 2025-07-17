using UnityEngine;

[RequireComponent(typeof(Grid), typeof(MeshRenderer))]
public class HoverGridCell : MonoBehaviour
{
  public static HoverGridCell Instance { get; private set; }

  [SerializeField] private Camera cam;
  [SerializeField] private LayerMask layerMask;
  private Grid grid;
  private MeshRenderer meshRenderer;
  [SerializeField] private Shader shader;
  private Material mat;

  public GridSettings gridSettings;
  private GridSettings oldGridSettings;

  [HideInInspector] public Vector2 mousePosition;
  [HideInInspector] public Vector2 cellSelected;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    grid = GetComponent<Grid>();

    UpdateShader();
  }

  void OnValidate()
  {
    oldGridSettings ??= new(gridSettings);

    if (!gridSettings.Compare(oldGridSettings))
    {
      oldGridSettings.CopySettings(gridSettings);
      UpdateShader();
    }
  }

  public void UpdateControllerPos(Vector2 delta)
  {
    cellSelected += delta;
  }

  void Update()
  {
    Ray rayFromMousePos = cam.ScreenPointToRay(mousePosition);

    if (Physics.Raycast(rayFromMousePos, out RaycastHit hit, 100, layerMask.value))
    {
      Vector3 cell = grid.LocalToCell(hit.point);
      Vector2 newCellSelected = new(cell.x, cell.z);

      if (!cellSelected.Equals(newCellSelected))
      {
        cellSelected = newCellSelected;
        UpdateShader();
      }
    }
  }

  void UpdateShader()
  {
    if (mat == null)
    {
      mat = new(shader);
    }

    mat.SetColor("_MainCol", gridSettings.mainColor);

    // Grid properties
    if (gridSettings.gridActive)
      mat.EnableKeyword("USE_GRID");
    else
      mat.DisableKeyword("USE_GRID");
    mat.SetFloat("_GridSize", gridSettings.gridSize);
    mat.SetFloat("_GridWidth", gridSettings.gridWidth);

    // Line properties
    mat.SetColor("_LineCol", gridSettings.lineColor);
    mat.SetFloat("_LineThreshold", gridSettings.lineThreshold);

    // Cell properties
    mat.SetColor("_CellCol", gridSettings.cellColor);
    mat.SetVector("_CellSelected", cellSelected);

    if (meshRenderer == null)
    {
      meshRenderer = GetComponent<MeshRenderer>();
    }

    meshRenderer.material = mat;
  }
}
