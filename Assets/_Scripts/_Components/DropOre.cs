using UnityEngine;

public class DropOre : MonoBehaviour
{
  public DropperInfo dropperInfo;
  [SerializeField] private GameObject dropperSpout;

  public void Drop()
  {
    BuildingManager.Instance.orePool.RequestFromPool(
      dropper: dropperInfo,
      pos: dropperSpout.transform.position,
      scale: dropperInfo.oreScale,
      rot: Quaternion.Euler(Vector3.zero)
    );
  }
}
