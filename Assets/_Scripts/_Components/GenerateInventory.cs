using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateInventory : MonoBehaviour
{

  [SerializeField] private GameObject slotParent;
  [SerializeField] private GameObject slotPrefab;

  private List<string> currentInventoryKeys = new();
  private readonly List<GameObject> buildingSlots = new();

  void OnEnable()
  {
    List<string> newInventoryKeys = new(GameManager.Instance.inventory.Keys);

    if (currentInventoryKeys == null)
    {
      Generate(newInventoryKeys);
      return;
    }

    if (!currentInventoryKeys.Equals(newInventoryKeys))
    {
      if (currentInventoryKeys.Count < newInventoryKeys.Count)
        Generate(new(newInventoryKeys.Except(currentInventoryKeys)));
      else
        RemoveBuildings(new(currentInventoryKeys.Except(newInventoryKeys)));
      
      currentInventoryKeys = newInventoryKeys;
    }
  }

  public void Generate(List<string> newBuildings)
  {
    newBuildings.ForEach(buildingId =>
    {
      GameObject newBuildingSlot = GenerateBuildingSlot(GameManager.Instance.inventory[buildingId]);
      newBuildingSlot.transform.SetParent(slotParent.transform, false);
      buildingSlots.Add(newBuildingSlot);
    });
  }

  public void RemoveBuildings(List<string> buildingsToRemove)
  {
    buildingsToRemove.ForEach(buildingId =>
    {
      int buildingIndex = buildingSlots.FindIndex(slot => slot.GetComponent<SetupBuildingSlot>().building.buildingData.id == buildingId);
      Destroy(buildingSlots[buildingIndex]);
      buildingSlots.RemoveAt(buildingIndex);
    });
  }

  public GameObject GenerateBuildingSlot(BuildingStack building)
  {
    GameObject newBuildingSlot = Instantiate(slotPrefab);

    SetupBuildingSlot setupBuildingSlot = newBuildingSlot.GetComponent<SetupBuildingSlot>();
    setupBuildingSlot.building = building;
    setupBuildingSlot.DisplayBuildingInfo();

    return newBuildingSlot;
  }
}
