Shader "Custom/FogOfWar"
{
    Properties
    {
        _MainTex ("Fog Texture", 2D) = "white" {}
        _FogColor ("Fog Color", Color) = (0,0,0,1)
        _ExploredColor ("Explored Color", Color) = (0,0,0,0.5)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent+1" "RenderType"="Transparent"
        }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _FogColor;
            float4 _ExploredColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 fogData = tex2D(_MainTex, i.uv);

                // Alpha value determines visibility
                // 0 = fully visible, 0.5 = explored, 1 = unexplored
                return fixed4(_FogColor.rgb, fogData.a);
            }
            ENDCG
        }
    }
}