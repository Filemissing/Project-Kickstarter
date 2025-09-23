Shader "UI/ImageBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlurRadius("Blur Radius", Float) = 8
        _Blackness("Blackness", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "CanvasRenderer"="True" }
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurRadius;
            float _Blackness;

            struct appdata { float4 vertex:POSITION; float2 uv:TEXCOORD0; };
            struct v2f { float2 uv:TEXCOORD0; float4 vertex:SV_POSITION; };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (_BlurRadius <= 0.001)
                {
                    fixed4 c = tex2D(_MainTex, i.uv);
                    return max(c - fixed4(_Blackness,_Blackness,_Blackness,0), 0);
                }

                // Limit samples for performance
                int radius = min(int(_BlurRadius), 16);
                float sigma = _BlurRadius * 0.5;

                // Horizontal blur
                fixed4 hCol = 0;
                float hWeight = 0;
                for (int x = -radius; x <= radius; x++)
                {
                    float w = exp(-x*x / (2*sigma*sigma));
                    hCol += tex2D(_MainTex, i.uv + float2(x * _MainTex_TexelSize.x, 0)) * w;
                    hWeight += w;
                }
                hCol /= hWeight;

                // Vertical blur
                fixed4 vCol = 0;
                float vWeight = 0;
                for (int y = -radius; y <= radius; y++)
                {
                    float w = exp(-y*y / (2*sigma*sigma));
                    vCol += tex2D(_MainTex, i.uv + float2(0, y * _MainTex_TexelSize.y)) * w;
                    vWeight += w;
                }
                vCol /= vWeight;

                // Combine and apply blackness
                fixed4 col = (hCol + vCol) * 0.5;
                col = max(col - fixed4(_Blackness,_Blackness,_Blackness,0), 0);

                return col;
            }

            ENDCG
        }
    }
}
