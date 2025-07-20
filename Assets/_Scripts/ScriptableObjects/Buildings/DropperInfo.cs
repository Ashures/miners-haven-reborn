using UnityEngine;

[CreateAssetMenu(fileName = "DropperInfo", menuName = "ScriptableObjects/Buildings/DropperInfo")]
public class DropperInfo : BuildingInfo
{
  public float dropRate;
  public float oreValue;
  public Vector3 oreScale;
}
