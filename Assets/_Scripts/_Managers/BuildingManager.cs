using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
  public static BuildingManager Instance { get; private set; }

  [HideInInspector] public OrePool orePool;

  private GameObject[,] cellsOccupied;
  private readonly Dictionary<float, List<DropOre>> droppers = new();
  private readonly Dictionary<float, float> nextDrops = new();
  public bool droppersActive = true;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    orePool = GetComponent<OrePool>();
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
      if (nextDrops[dropRate] <= Time.time)
      {
        nextDrops[dropRate] = Time.time + dropRate;
        SpawnOres(dropRate);
      }
    }
  }

  private void SpawnOres(float dropRate)
  {
    foreach (DropOre dropper in droppers[dropRate])
    {
      dropper.Drop();
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
      {
        droppers[dropOre.dropperInfo.dropRate] = new() { dropOre };
        nextDrops[dropOre.dropperInfo.dropRate] = Time.time + dropOre.dropperInfo.dropRate;
      }
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
