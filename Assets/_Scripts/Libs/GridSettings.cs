using System;
using UnityEngine;

[Serializable]
public class GridSettings
{
  public Color mainColor;
  public bool gridActive;
  public float gridSize;
  public float gridWidth;
  public Color lineColor;
  [Range(0.01f, 0.25f)] public float lineThreshold;
  public Color cellColor;

  public GridSettings(GridSettings settings)
  {
    mainColor = settings.mainColor;
    gridActive = settings.gridActive;
    gridSize = settings.gridSize;
    gridWidth = settings.gridWidth;
    lineColor = settings.lineColor;
    lineThreshold = settings.lineThreshold;
    cellColor = settings.cellColor;
  }

  public void CopySettings(GridSettings settings)
  {
    mainColor = settings.mainColor;
    gridActive = settings.gridActive;
    gridSize = settings.gridSize;
    gridWidth = settings.gridWidth;
    lineColor = settings.lineColor;
    lineThreshold = settings.lineThreshold;
    cellColor = settings.cellColor;
  }

  public bool Compare(GridSettings settings)
  {
    return mainColor.Equals(settings.mainColor) &&
           gridActive == settings.gridActive &&
           gridSize == settings.gridSize &&
           gridWidth == settings.gridWidth &&
           lineColor.Equals(settings.lineColor) &&
           lineThreshold == settings.lineThreshold &&
           cellColor.Equals(settings.cellColor);
  }
}
