using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
  [SerializeField] private Shader shader;
  private Material mat;
  private BuildingStack selectedBuilding;
  private GameObject buildingToPlace;

  private bool isEnabled;

  void Awake()
  {
    if (mat == null)
    {
      mat = new(shader);
    }
  }

  void Update()
  {
    if (isEnabled)
      MoveHologram();
  }

  private void MoveHologram()
  {
    buildingToPlace.transform.position = HoverGridCell.Instance.cellSelectedPosition;
  }

  public void Enable(bool enabled)
  {
    isEnabled = enabled;
    buildingToPlace.SetActive(enabled);
  }

  public void ChangeSelectedBuilding(BuildingStack building)
  {
    selectedBuilding = building;

    Destroy(buildingToPlace);
    buildingToPlace = Instantiate(selectedBuilding.buildingData.prefab, transform);
    buildingToPlace.name = selectedBuilding.buildingData.displayName;
    buildingToPlace.GetComponent<MeshRenderer>().material = mat;
  }
}
