using System.Collections.Generic;
using UnityEngine;

public class OrePool : MonoBehaviour
{
  [SerializeField] private GameObject poolParent;
  [SerializeField] private GameObject pooledObject;

  private readonly Stack<GameObject> inactiveObjects = new();

  public void RequestFromPool(DropperInfo dropper, Vector3 pos, Vector3 scale, Quaternion rot)
  {
    GameObject requestedObject;
    if (inactiveObjects.Count > 0)
    {
      requestedObject = inactiveObjects.Pop();
    }
    else
    {
      requestedObject = Instantiate(pooledObject, poolParent.transform);
    }

    Transform _requestedObjectTransform = requestedObject.transform;

    _requestedObjectTransform.SetPositionAndRotation(pos, rot);
    _requestedObjectTransform.localScale = scale;

    Rigidbody _requestedObjectRigidbody = requestedObject.GetComponent<Rigidbody>();
    _requestedObjectRigidbody.ResetInertiaTensor();
    _requestedObjectRigidbody.linearVelocity = Vector3.zero;
    _requestedObjectRigidbody.angularVelocity = Vector3.zero;

    requestedObject.GetComponent<OreMechanics>().dropperInfo = dropper;

    requestedObject.SetActive(true);
  }

  public void ReturnToPool(GameObject objectToReturn)
  {
    inactiveObjects.Push(objectToReturn);

    objectToReturn.SetActive(false);
  }
}
