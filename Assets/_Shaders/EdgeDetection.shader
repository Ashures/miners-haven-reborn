Shader "Custom/PostProcessing/EdgeDetection"
{
  Properties
  {
    _BaseMap ("Texture", 2D) = "white" {}
    _BorderColor ("Border Color", Color) = (0, 0, 0, 0)
    _EdgeSize ("Edge Size", Float) = 0
  }
  SubShader
  {
    Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }

    Pass
    {
      HLSLPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

      struct Attributes
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct Varyings
      {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
      };

      TEXTURE2D(_BaseMap);
      SAMPLER(sampler_BaseMap);

      TEXTURE2D(_CameraDepthTexture);
      SAMPLER(sampler_CameraDepthTexture);

      CBUFFER_START(UnityPerMaterial)
      float4 _BorderColor;
      float4 _CameraDepthTexture_TexelSize;
      float _EdgeSize;
      CBUFFER_END

      Varyings vert (Attributes v)
      {
        Varyings o;
        o.vertex = TransformObjectToHClip(v.vertex.xyz);
        o.uv = v.uv;
        return o;
      }

      float getDepthByDirection(Varyings i, float2 dir) 
      {
        return Linear01Depth(
          SAMPLE_TEXTURE2D(
            _CameraDepthTexture, 
            sampler_CameraDepthTexture, 
            i.uv + dir * _CameraDepthTexture_TexelSize.xy * _EdgeSize
          ).r,
          _ZBufferParams
        );
      }

      float4 frag (Varyings i) : SV_Target
      {
        float4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);

        float n = getDepthByDirection(i, float2(0, 1));
        float e = getDepthByDirection(i, float2(1, 0));
        float s = getDepthByDirection(i, float2(0, -1));
        float w = getDepthByDirection(i, float2(-1, 0));

        float edgeThreshold = 0.1;
        if (n - s > edgeThreshold || w - e > edgeThreshold || s - n > edgeThreshold || e - w > edgeThreshold)
          col = _BorderColor;

        return col;
      }
      ENDHLSL
    }
  }
}
