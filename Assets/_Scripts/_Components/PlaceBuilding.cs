using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
  [SerializeField] private GameObject buildingParent;
  [SerializeField] private Material mat;
  private BuildingStack selectedBuilding;
  private GameObject buildingToPlace;

  [HideInInspector] public bool isEnabled;

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

    MeshRenderer buildingMeshRenderer = buildingToPlace.GetComponent<MeshRenderer>();
    Material[] newMaterials = new Material[buildingMeshRenderer.materials.Length];

    for (int i = 0; i < newMaterials.Length; i++)
    {
      newMaterials[i] = mat;
    }

    buildingMeshRenderer.materials = newMaterials;
  }

  public void Place()
  {
    if (BuildingManager.Instance.CellOccupied(HoverGridCell.Instance.cellSelected)) return;

    GameManager.Instance.RemoveBuildingFromInventory(selectedBuilding.buildingData.id, amountToRemove: 1);
    
    GameManager.Instance.selectedBuildingSlot.DisplayBuildingInfo();
    GameManager.Instance.selectedBuildingSlot = null;
    
    GameObject building = Instantiate(selectedBuilding.buildingData.prefab, buildingParent.transform);
    building.transform.position = HoverGridCell.Instance.cellSelectedPosition;
    BuildingManager.Instance.AddBuilding(building, HoverGridCell.Instance.cellSelected);

    Enable(false);
  }
}
