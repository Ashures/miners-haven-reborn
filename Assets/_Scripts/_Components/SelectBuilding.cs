using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SetupBuildingSlot))]
public class SelectBuilding : MonoBehaviour
{
  [SerializeField] private List<PlatformBinds> binds;
  private SetupBuildingSlot setupBuildingSlot;
  private Button button;

  void OnEnable()
  {
    button = GetComponent<Button>();
    button.onClick.AddListener(Select);
  }

  private void AssignComponentIfNull()
  {
    if (setupBuildingSlot == null)
      setupBuildingSlot = GetComponent<SetupBuildingSlot>();
  }

  public void Select()
  {
    AssignComponentIfNull();

    GameManager.Instance.StartPlacingBuilding(setupBuildingSlot);
    UIHandler.Instance.CloseWindow();
    UIHandler.Instance.OpenInteractionGuide(binds);
  }
}
