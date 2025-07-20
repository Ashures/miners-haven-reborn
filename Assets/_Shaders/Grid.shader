Shader "Custom/Grid"
{
  Properties
  {
    [Toggle(USE_GRID)] _GridActive ("Grid Active", Float) = 1
    _MainCol ("Main Color", Color) = (0, 0, 0, 0)
    _GridSize ("Grid Size", Float) = 0
    _GridWidth ("Grid Width", Float) = 0
    _LineCol ("Line Color", Color) = (0, 0, 0, 0)
    _LineThreshold ("Line Threshold", Range (0.01, 0.1)) = 0
    _CellCol ("Cell Color", Color) = (0, 0, 0, 0)
    _CellSelected ("Cell Selected", Vector) = (0, 0, 0, 0)
  }
  SubShader
  {
    Tags { "RenderType"="Opaque" }

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile _ USE_GRID

      #include "UnityCG.cginc"
      
      bool _GridActive;
      float4 _MainCol;
      float4 _LineCol;
      float _GridSize;
      float _GridWidth;
      float _LineThreshold;
      float4 _CellCol;
      float2 _CellSelected;

      struct VertData
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };
      
      struct v2f
      {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
      };
      
      v2f vert (VertData v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

      bool withinLineThreshold (float num) 
      {
        float mod = num % _GridSize;
        return mod < _LineThreshold / 2 || mod > _GridSize - _LineThreshold / 2;
      }

      bool withinCellThreshold (float2 uv)
      {
        return (uv.x >= _CellSelected.x && uv.x <= _CellSelected.x + _GridSize && 
                uv.y >= _CellSelected.y && uv.y <= _CellSelected.y + _GridSize) &&
              !(uv.x > _CellSelected.x + _LineThreshold && uv.x < _CellSelected.x + _GridSize - _LineThreshold &&
                uv.y > _CellSelected.y + _LineThreshold && uv.y < _CellSelected.y + _GridSize - _LineThreshold);
      }

      float4 frag (v2f i) : SV_Target
      {
        float4 col;

        #ifdef USE_GRID
          float2 uvMod = i.uv * _GridWidth;
          col = withinLineThreshold(uvMod.x) || withinLineThreshold(uvMod.y) ? _LineCol : _MainCol;
          col = withinCellThreshold(float2(_GridWidth, _GridWidth) - uvMod) ? _CellCol : col;
        #else
          col = _MainCol;
        #endif

        return col;
      }
      ENDCG
    }
  }
}
