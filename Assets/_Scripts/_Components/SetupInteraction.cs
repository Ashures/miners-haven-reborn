using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SetupInteraction : MonoBehaviour
{
  public PlatformBinds bind;

  [SerializeField] private Image image;
  [SerializeField] private TMPro.TMP_Text textChild;

  public void Setup()
  {
    AssignComponentIfNull();

    image.sprite = bind.sprite;
    image.preserveAspect = true;

    textChild.text = bind.hint;
  }

  void AssignComponentIfNull()
  {
    if (image == null)
      image = GetComponent<Image>();
  }
}
