Shader "Custom/BuildingHologram"
{
    Properties
    {
      [Toggle(PLACEMENT_ERROR)] _PlacementError ("Placement Error", Float) = 1

      _MainColor ("Main Color", Color) = (1,1,1,1)
      _ErrorColor ("Error Color", Color) = (1,1,1,1)
      _Shadow ("Shadow", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Tags { "Queue"="Transparent" }

        ZWrite off

        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0
        #pragma multi_compile _ PLACEMENT_ERROR


        sampler2D _MainTex;

        struct Input
        {
          float2 uv_MainTex;
        };

        float4 _MainColor;
        float4 _ErrorColor;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
          #ifdef PLACEMENT_ERROR
            o.Albedo = _ErrorColor.rgb;
            o.Alpha = _ErrorColor.a;
          #else
            o.Albedo = _MainColor.rgb;
            o.Alpha = _MainColor.a;
          #endif
        }
        ENDCG

        ColorMask 0

        CGPROGRAM
        #pragma surface surf Standard alpha:fade alphatest:_Shadow addshadow
        #pragma target 3.0
        #pragma multi_compile _ PLACEMENT_ERROR

        sampler2D _MainTex;

        struct Input
        {
          float2 uv_MainTex;
        };

        float4 _MainColor;
        float4 _ErrorColor;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
          #ifdef PLACEMENT_ERROR
            o.Albedo = _ErrorColor.rgb;
            o.Alpha = _ErrorColor.a;
          #else
            o.Albedo = _MainColor.rgb;
            o.Alpha = _MainColor.a;
          #endif
        }
        ENDCG
    }
    FallBack "Diffuse"
}
