Shader "Hidden/EdgeDetection"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "white" {}
  }
  SubShader
  {
    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f
      {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
      };

      sampler2D _MainTex, _CameraDepthTexture;
      float4 _BorderColor, _CameraDepthTexture_TexelSize;
      float _EdgeSize;

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

      float getDepthByDirection(v2f i, float2 dir) 
      {
        return Linear01Depth(
          tex2D(
            _CameraDepthTexture, 
            i.uv + dir * _CameraDepthTexture_TexelSize * _EdgeSize
          ).r
        );
      }

      float4 frag (v2f i) : SV_Target
      {
        float4 col = tex2D(_MainTex, i.uv);

        float n = getDepthByDirection(i, float2(0, 1));
        float e = getDepthByDirection(i, float2(1, 0));
        float s = getDepthByDirection(i, float2(0, -1));
        float w = getDepthByDirection(i, float2(-1, 0));

        float edgeThreshold = 0.1;
        if (n - s > edgeThreshold || w - e > edgeThreshold || s - n > edgeThreshold || e - w > edgeThreshold)
          col = _BorderColor;

        return col;
      }
      ENDCG
    }
  }
}
