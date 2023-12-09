Shader "Custom/VisibilityShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("View Radius", Float) = 10.0
        _DarknessIntensity ("Darkness Intensity", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Radius;
            float _DarknessIntensity;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 centerUV = float2(0.5, 0.5); // Центр текстури
                float distance = length(i.uv - centerUV); // Відстань від центру текстури
                float alpha = distance > _Radius ? _DarknessIntensity : 1.0;
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a *= alpha;
                return col;
            }
            ENDCG
        }
    }
}