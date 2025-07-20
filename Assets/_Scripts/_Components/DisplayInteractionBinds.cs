using System.Collections.Generic;
using UnityEngine;

public class DisplayInteractionBinds : MonoBehaviour
{
  [SerializeField] private GameObject bindsParent;
  [SerializeField] private GameObject interactionPrefab;
  public List<PlatformBinds> binds;

  private readonly List<GameObject> interactions = new();

  void OnEnable()
  {
    Display();
  }

  public void OpenGuide(List<PlatformBinds> newBinds)
  {
    binds = newBinds;
    gameObject.SetActive(true);
  }

  public void CloseGuide()
  {
    gameObject.SetActive(false);
  }

  void Display()
  {
    if (interactions.Count < binds.Count)
    {
      for (int i = interactions.Count; i < binds.Count; i++)
      {
        interactions.Add(Instantiate(interactionPrefab, bindsParent.transform));
      }
    }

    if (interactions.Count > binds.Count)
    {
      for (int i = interactions.Count - 1; i >= binds.Count; i--)
      {
        GameObject interaction = interactions[i];

        interactions.RemoveAt(i);
        Destroy(interaction);
      }
    }

    for (int i = 0; i < binds.Count; i++)
    {
      SetupInteraction setupInteraction = interactions[i].GetComponent<SetupInteraction>();
      setupInteraction.bind = binds[i];
      setupInteraction.Setup();
    }
  }
}
