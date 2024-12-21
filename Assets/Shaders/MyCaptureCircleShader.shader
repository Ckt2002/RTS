Shader "Unlit/MyCaptureCircleShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1, 1, 1, 1)
        _Center ("Circle Center", Vector) = (0.5, 0.5, 0, 0)
        _FillAmount ("Fill Amount", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float2 _Center;
            float _FillAmount;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Tính toán UV
                float2 uv = i.uv - _Center;

                // Tính góc và bán kính
                float angle = atan2(uv.y, uv.x) / (2.0 * UNITY_PI) + 0.5;
                float radius = length(uv);

                // Kiểm tra điều kiện loại bỏ pixel
                if (angle > _FillAmount || radius > 0.5)
                    discard;

                // Lấy màu từ texture và nhân với tint
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                return texColor;
            }
            ENDCG
        }
    }
}
