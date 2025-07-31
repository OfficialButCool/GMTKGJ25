Shader "Custom/SmoothFogWithTwoPlayers"
{
    Properties
    {
        _FogColor("Fog Color", Color) = (0.2, 0.2, 0.2, 0.8)
        _Player1Pos("Player 1 Position", Vector) = (0, 0, 0, 0)
        _Player2Pos("Player 2 Position", Vector) = (0, 0, 0, 0)
        _Radius1("Player 1 Radius", Float) = 3
        _Radius2("Player 2 Radius", Float) = 3
        _Feather("Feather Amount", Float) = 2
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 worldPos : TEXCOORD0;
            };

            float4 _Player1Pos;
            float4 _Player2Pos;
            float _Radius1;
            float _Radius2;
            float _Feather;
            fixed4 _FogColor;

            v2f vert(appdata v)
            {
                v2f o;
                float4 world = mul(unity_ObjectToWorld, v.vertex);
                o.worldPos = world.xy;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float d1 = distance(i.worldPos, _Player1Pos.xy);
                float d2 = distance(i.worldPos, _Player2Pos.xy);

                float alpha1 = smoothstep(_Radius1 - _Feather, _Radius1, d1);
                float alpha2 = smoothstep(_Radius2 - _Feather, _Radius2, d2);

                float finalAlpha = alpha1 * alpha2;
                return fixed4(_FogColor.rgb, _FogColor.a * finalAlpha);
            }
            ENDCG
        }
    }
}
