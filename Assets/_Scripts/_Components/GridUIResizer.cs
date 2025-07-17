using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class GridUIResizer : MonoBehaviour
{
  private GridLayoutGroup gridLayoutGroup;
  private RectTransform rectTransform;

  [SerializeField] private bool horizontalConstraint = true;

  void OnTransformChildrenChanged()
  {
    UpdateCellSize();
  }

  void OnValidate()
  {
    UpdateCellSize();
  }

  void AssignComponentsIfNull()
  {
    if (gridLayoutGroup == null)
      gridLayoutGroup = GetComponent<GridLayoutGroup>();
    if (rectTransform == null)
      rectTransform = GetComponent<RectTransform>();
  }

  void UpdateCellSize()
  {
    AssignComponentsIfNull();

    int totalSpacing = (gridLayoutGroup.constraintCount - 1) * (int)gridLayoutGroup.spacing.x;
    int cellSize = (int)((horizontalConstraint ? rectTransform.rect.width : rectTransform.rect.height) - totalSpacing) / gridLayoutGroup.constraintCount;

    gridLayoutGroup.cellSize = Vector2.one * cellSize;
  }
}
