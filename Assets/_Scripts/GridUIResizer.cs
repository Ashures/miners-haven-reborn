using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class GridUIResizer : MonoBehaviour
{
  private GridLayoutGroup gridLayoutGroup;
  private RectTransform rectTransform;

  void OnTransformChildrenChanged()
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
    int cellSize = (int)(rectTransform.rect.width - totalSpacing) / gridLayoutGroup.constraintCount;

    gridLayoutGroup.cellSize = Vector2.one * cellSize;
  }
}
