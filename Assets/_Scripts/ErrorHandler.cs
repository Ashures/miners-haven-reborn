using System.Collections;
using TMPro;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
  [SerializeField] private TMP_Text message;
  [SerializeField] private float errorDisplayTime;

  public IEnumerator DisplayError(string messageToDisplay)
  {
    message.text = messageToDisplay;

    yield return new WaitForSeconds(errorDisplayTime);

    gameObject.SetActive(false);
  }
}
