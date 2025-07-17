using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SetupBuildingSlot))]
public class SelectBuilding : MonoBehaviour
{
  private SetupBuildingSlot setupBuildingSlot;
  private Button button;

  void OnEnable()
  {
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

    GameManager.Instance.ChangeSelectedBuilding(setupBuildingSlot.building);
    UIHandler.Instance.CloseWindow();
  }
}
