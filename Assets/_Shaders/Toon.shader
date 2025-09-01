Shader "Custom/PostProcessing/Toon"
{
  Properties
  {
    _BaseMap ("Texture", 2D) = "white" {}
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

      CBUFFER_START(UnityPerMaterial)
      int _ColorsPerChannel;
      CBUFFER_END

      Varyings vert (Attributes v)
      {
          Varyings o;
          o.vertex = TransformObjectToHClip(v.vertex.xyz);
          o.uv = v.uv;
          return o;
      }

      float4 frag (Varyings i) : SV_Target
      {
        float4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);
        return floor(col * _ColorsPerChannel + 0.5) / _ColorsPerChannel;
      }
      ENDHLSL
    }
  }
}
