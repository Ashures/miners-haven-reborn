using UnityEngine;
using UnityEngine.UI;

public class SidebarInteractions : MonoBehaviour
{
  [SerializeField] private Button inventoryButton;
  [SerializeField] private Button shopButton;
  [SerializeField] private Button settingsButton;

  void Awake()
  {
    inventoryButton.onClick.AddListener(() => ClickButton("inventory"));
    shopButton.onClick.AddListener(() => ClickButton("shop"));
    settingsButton.onClick.AddListener(() => ClickButton("settings"));
  }

  void ClickButton(string windowTarget)
  {
    UIHandler.Instance.OpenWindow(windowTarget);
  }
}
