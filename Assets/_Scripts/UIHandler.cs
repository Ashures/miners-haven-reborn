using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
  public static UIHandler Instance { get; private set; }

  [Header("UI Elements")]
  [SerializeField] private GameObject window;
  [SerializeField] private Button closeWindowButton;
  [SerializeField] private GameObject inventory;
  [SerializeField] private GameObject shop;
  [SerializeField] private GameObject settings;
  [SerializeField] private GameObject errorMessage;

  private GameObject activeWindowContext;
  private ErrorHandler errorHandler;
  private Coroutine currentError;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    closeWindowButton.onClick.AddListener(CloseWindow);
  }

  public void CloseWindow()
  {
    window.SetActive(false);
    activeWindowContext = null;
  }

  public void OpenWindow(string windowTarget)
  {
    window.SetActive(true);

    switch (windowTarget)
    {
      case "inventory":
        OpenContext(inventory);
        break;
      case "shop":
        OpenContext(shop);
        break;
      case "settings":
        OpenContext(settings);
        break;
      default:
        print($"Invalid window context \"{windowTarget}\".");
        return;
    }
  }

  private void OpenContext(GameObject ctx)
  {
    if (activeWindowContext != null)
      activeWindowContext.SetActive(false);

    if (activeWindowContext == ctx)
    {
      CloseWindow();
      return;
    }

    DisplayError($"Opened {ctx.name}");

    activeWindowContext = ctx;
    activeWindowContext.SetActive(true);
  }

  public void DisplayError(string messageToDisplay)
  {
    if (errorHandler == null)
      errorHandler = errorMessage.GetComponent<ErrorHandler>();
    
    if (currentError != null)
      StopCoroutine(currentError);

    errorMessage.SetActive(true);

    currentError = StartCoroutine(errorHandler.DisplayError(messageToDisplay));
  }
}
