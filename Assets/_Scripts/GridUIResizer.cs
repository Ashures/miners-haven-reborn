using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class GridUIResizer : MonoBehaviour
{
  private GridLayoutGroup gridLayoutGroup;
  private RectTransform rectTransform;

  void OnTransformChildrenChanged()
  {
    print(gridLayoutGroup.spacing);
    print(gridLayoutGroup.constraintCount);
    print(rectTransform.rect.width);

    int totalSpacing = (gridLayoutGroup.constraintCount - 1) * (int)gridLayoutGroup.spacing.x;
    int cellSize = (int)(rectTransform.rect.width - totalSpacing) / gridLayoutGroup.constraintCount;

    gridLayoutGroup.cellSize = Vector2.one * cellSize;
  }
}
