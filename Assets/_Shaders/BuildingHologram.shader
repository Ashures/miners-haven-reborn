Shader "Custom/Object/BuildingHologram"
{
  Properties
  {
    [Toggle(PLACEMENT_ERROR)] _PlacementError ("Placement Error", Float) = 1
    
    _BaseMap ("Main Texture", 2D) = "white" {}
    _MainColor ("Main Color", Color) = (1,1,1,1)
    _ErrorColor ("Error Color", Color) = (1,1,1,1)
  }
  SubShader
  {
    Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

    Pass 
    {
      ZWrite Off
      HLSLPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile _ PLACEMENT_ERROR

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
      float4 _MainColor;
      float4 _ErrorColor;
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

        #ifdef PLACEMENT_ERROR
          col = lerp(col, _ErrorColor, 0.5);
        #else
          col = lerp(col, _MainColor, 0.5);
        #endif

        return col;
      }
      ENDHLSL
    }
  }
}
