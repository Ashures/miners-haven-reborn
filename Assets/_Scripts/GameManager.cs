using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  [HideInInspector] public Dictionary<string, BuildingStack> inventory = new();

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  public void AddBuildingToInventory(BuildingInfo buildingInfo, int stack)
  {
    if (inventory.TryGetValue(buildingInfo.id, out BuildingStack buildingStack))
    {
      buildingStack.currentStack += stack;
      return;
    }
    else
    {
      buildingStack = new()
      {
        buildingData = buildingInfo,
        currentStack = stack
      };
    }

    inventory[buildingInfo.id] = buildingStack;
  }

  public void RemoveBuildingFromInventory(string buildingId, int amountToRemove)
  {
    if (!inventory.TryGetValue(buildingId, out BuildingStack building)) return;

    building.currentStack -= amountToRemove;

    if (building.currentStack <= 0)
      inventory.Remove(buildingId);
    else
      inventory[buildingId] = building;
  }
}
