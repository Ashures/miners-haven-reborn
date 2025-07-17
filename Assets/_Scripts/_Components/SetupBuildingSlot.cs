using UnityEngine;
using UnityEngine.UI;

public class SetupBuildingSlot : MonoBehaviour
{
  [SerializeField] private TMPro.TMP_Text stackText;
  public BuildingStack building;

  private Image image;

  void Awake()
  {
    image = GetComponent<Image>();

    if (building.buildingData != null)
      DisplayBuildingInfo();
  }

  public void DisplayBuildingInfo()
  {
    image.sprite = building.buildingData.sprite;
    stackText.text = $"{building.currentStack}";
  }
}
