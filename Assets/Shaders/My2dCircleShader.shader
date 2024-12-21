Shader "Custom/2DRadialFillSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} // Texture chính
        _FillAmount ("Fill Amount", Range(0,1)) = 1  // Tỷ lệ fill (0-1)
        _Color ("Tint Color", Color) = (1,1,1,1)     // Màu sắc
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            fixed4 _Color;
            float _FillAmount;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv-0.8; // Dịch UV về trung tâm (tâm hình tròn là (0.5, 0.5))
                float angle = atan2(uv.y, uv.x) / (2.0 * UNITY_PI) + 0.5; // Tính góc từ -PI đến PI về [0, 1]
                float radius = length(uv); // Khoảng cách từ tâm (bán kính)
                //
                // // Kiểm tra điều kiện góc và bán kính
                if (angle > _FillAmount || radius > 0.5)
                    discard; // Loại bỏ pixel ngoài vùng
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                return texColor;
            }
            ENDCG
        }
    }
}
