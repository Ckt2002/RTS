Shader "Custom/ImprovedOutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+1" "RenderType" = "Transparent" "ForceNoShadowCasting" = "True"
        }

        // Outline pass
        Pass
        {
            Name "OUTLINE"

            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;

                // Convert to view space
                float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                float3 viewPos = UnityObjectToViewPos(v.vertex);

                // Extrude in view space
                viewPos += viewNormal * _OutlineWidth;

                // Convert back to clip space
                o.pos = mul(UNITY_MATRIX_P, float4(viewPos, 1));

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}