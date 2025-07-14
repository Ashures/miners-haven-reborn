using UnityEngine;
using UnityEngine.UI;

public class SidebarInteractions : MonoBehaviour
{
  [SerializeField] private Button inventoryButton;

  void Awake()
  {
    inventoryButton.onClick.AddListener(ClickInventoryButton);
  }

  void ClickInventoryButton()
  {
    print("clicked!");
  }
}
