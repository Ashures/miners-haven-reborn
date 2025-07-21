using System.Collections;
using UnityEngine;

public class OreMechanics : MonoBehaviour
{
  [HideInInspector] public DropperInfo dropperInfo;

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.layer == 6)
    {
      StartCoroutine(nameof(ReturnOre));
    }
  }

  private IEnumerator ReturnOre()
  {
    yield return new WaitForSeconds(0.5f);

    BuildingManager.Instance.orePool.ReturnToPool(gameObject);
  }
}
