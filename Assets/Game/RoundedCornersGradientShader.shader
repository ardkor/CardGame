Shader "Custom/RoundedCornersGradientShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorTop ("Top Color", Color) = (1,1,1,1)
        _ColorBottom ("Bottom Color", Color) = (1,1,1,1)
        _CornerRadius ("Corner Radius", Range(0, 0.5)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off

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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _ColorTop;
            fixed4 _ColorBottom;
            float _CornerRadius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Determine the color based on vertical gradient
                fixed4 color = lerp(_ColorBottom, _ColorTop, uv.y);

                // Apply rounded corners
                float2 center = float2(0.5, 0.5);
                float2 dist = abs(uv - center);
                float2 size = float2(0.5 - _CornerRadius, 0.5 - _CornerRadius);

                if (dist.x > size.x && dist.y > size.y)
                {
                    float2 cornerDist = dist - size;
                    float distToCorner = length(cornerDist);
                    float radius = _CornerRadius;
                    color.a *= smoothstep(radius, radius - 0.001, distToCorner);
                }
                
                return color;
            }
            ENDCG
        }
    }
    FallBack "UI/Default"
}
