Shader "Custom/Test"
{
    Properties
    {
        [Header(Main)]
        _MainColor ("Main Color", Color) = (0, 0, 0, 1)
        _MainTexture("Main Texture", 2D) = "white" {} // 何も入っていない場合は白色のテクスチャ
        _DiffuseShade ("Diffuse Shade", Float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _MainColor;
            sampler2D _MainTexture;
            float4 _MainTexture_ST; // _MainTextureのタイリングとオフセット値が格納されている
            float _DiffuseShade;

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                // ポリゴンから法線を取得する
                float3 normal : NORMAL;
            };

            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;

                // フラグメントシェーダーに渡す法線はワールド座標へ変換される
                float3 normalWS : TEXCOORD1;
            };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // UnityObjectToWorldNormalを使うと、オブジェクト座標系の法線をワールド座標系に変換できる
                o.normalWS = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag(VertexOutput i) : SV_Target
            {

                // World Space Directions
                // L 光源の向き、N 法線の向き　どちらもワールド座標
                float3 L = normalize(-_WorldSpaceLightPos0.xyz);
                float3 N = normalize(i.normalWS);

                // Final color of this shader
                fixed4 finalColor = fixed4(0, 0, 0, 1);

                // Texture mapping
                float2 mainUv = i.uv * _MainTexture_ST.xy + _MainTexture_ST.zw;
                finalColor = tex2D(_MainTexture, mainUv) * _MainColor;

                // Diffuse Shade
                fixed3 diffColor = max(0, dot(N, -L) * _DiffuseShade + (1 - _DiffuseShade)) * finalColor.rgb;
                finalColor.rgb = diffColor;

                return finalColor;
            }
            ENDCG
        }
    }
}