Shader "Custom/BuildingHologram"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Shadow ("Shadow", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Tags { "Queue"="Transparent" }

        ZWrite off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
          float2 uv_MainTex;
        };

        float4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
          o.Albedo = _Color.rgb;
          o.Alpha = _Color.a;
        }
        ENDCG

        ColorMask 0

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade alphatest:_Shadow addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
          float2 uv_MainTex;
        };

        float4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
          o.Albedo = _Color.rgb;
          o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
