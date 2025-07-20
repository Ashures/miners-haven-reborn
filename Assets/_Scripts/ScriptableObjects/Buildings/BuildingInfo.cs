using UnityEngine;

[CreateAssetMenu(fileName = "BuildingInfo", menuName = "ScriptableObjects/Buildings/BuildingInfo")]
public class BuildingInfo : ScriptableObject
{
  public GameObject prefab;

  public string id;
  public string displayName;
  public Sprite sprite;
  public string desc;
  public int price;
  public RPLevels tier;
}
