using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
  public static BuildingManager Instance { get; private set; }

  private GameObject[,] cellsOccupied;
  private readonly Dictionary<float, List<DropOre>> droppers = new();
  public bool droppersActive = true;
  [SerializeField, Range(0f, 0.5f)] private float dropThreshold;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  void Start()
  {
    int cellsArraySize = (int)HoverGridCell.Instance.gridSettings.gridWidth;
    cellsOccupied = new GameObject[cellsArraySize, cellsArraySize];
  }

  private void Update()
  {
    RunDropOre();
  }

  private void RunDropOre()
  {
    if (!droppersActive || droppers.Count == 0) return;

    foreach (float dropRate in droppers.Keys)
    {
      
    }
  }

  public bool AddBuilding(GameObject building, Vector2 cell)
  {
    Vector2Int cellIndices = new((int)cell.x, (int)cell.y);
    if (cellsOccupied[cellIndices.x, cellIndices.y] != null) return false;

    cellsOccupied[cellIndices.x, cellIndices.y] = building;

    if (building.TryGetComponent(out DropOre dropOre))
    {
      bool dropRateInDroppers = droppers.ContainsKey(dropOre.dropperInfo.dropRate);

      if (dropRateInDroppers)
        droppers[dropOre.dropperInfo.dropRate].Add(dropOre);
      else
        droppers[dropOre.dropperInfo.dropRate] = new() { dropOre };
    }

    return true;
  }

  public bool RemoveBuilding(Vector2 cell)
  {
    Vector2Int cellIndices = new((int)cell.x, (int)cell.y);
    if (cellsOccupied[cellIndices.x, cellIndices.y] == null) return false;

    GameObject buildingToRemove = cellsOccupied[cellIndices.x, cellIndices.y];
    cellsOccupied[cellIndices.x, cellIndices.y] = null;
    Destroy(buildingToRemove);

    return true;
  }

  public bool CellOccupied(Vector2 cell)
  {
    Vector2Int cellIndices = new((int)cell.x, (int)cell.y);

    return cellsOccupied[cellIndices.x, cellIndices.y] != null;
  }

  public GameObject BuildingInCell(Vector2 cell)
  {
    Vector2Int cellIndices = new((int)cell.x, (int)cell.y);
    if (cellsOccupied[cellIndices.x, cellIndices.y] == null) return null;

    GameObject building = cellsOccupied[cellIndices.x, cellIndices.y];

    return building;
  }
}
