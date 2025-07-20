using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandler : MonoBehaviour
{
  [SerializeField] private TMP_Text message;
  [SerializeField, Range(1f, 5f)] private float errorDisplayTime;
  [SerializeField, Range(0.1f, 3f)] private float errorFadeOutTime;

  private Image errorWindowImage;
  [SerializeField] private Color originalErrorWindowColor;
  [SerializeField] private Color originalErrorMessageColor;

  private void AssignValuesIfNull()
  {
    if (errorWindowImage == null)
      errorWindowImage = GetComponent<Image>();
  }

  private void AdjustAlphaOfError(float alpha)
  {
    message.color = new Color(originalErrorMessageColor.r, originalErrorMessageColor.g, originalErrorMessageColor.b, alpha);
    errorWindowImage.color = new Color(originalErrorWindowColor.r, originalErrorWindowColor.g, originalErrorWindowColor.b, alpha);
  }

  public IEnumerator DisplayError(string messageToDisplay)
  {
    AssignValuesIfNull();

    AdjustAlphaOfError(alpha: 1.0f);
    message.text = messageToDisplay;

    yield return new WaitForSeconds(errorDisplayTime);

    float fadeTimeElapsed = 0f;

    while (fadeTimeElapsed < errorFadeOutTime)
    {
      float alpha = Mathf.Lerp(1f, 0f, fadeTimeElapsed / errorFadeOutTime);
      AdjustAlphaOfError(alpha);

      fadeTimeElapsed += Time.deltaTime;
      yield return null;
    }

    AdjustAlphaOfError(alpha: 0f);
    gameObject.SetActive(false);
  }
}
