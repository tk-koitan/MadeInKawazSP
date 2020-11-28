Shader "Custom/Test2"
{
    Properties
    {
        [Header(Main)]
        _MainColor("Main Color", Color) = (0, 0, 0, 1)
        _MainTexture("Main Texture", 2D) = "white" {} // 何も入っていない場合は白色のテクスチャ
        _RotationFactor("Rotation Factor", Range(0, 10)) = 0
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
                float _RotationFactor;

                #define PI 3.141592

                struct VertexInput
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct VertexOutput
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                VertexOutput vert(VertexInput v)
                {
                    VertexOutput o = (VertexOutput)0;
                    float power = sin(v.vertex.x) + sin(v.vertex.y);
                    float theta = power * _RotationFactor * PI;

                    v.vertex.x *= 1.2;
                    v.vertex.y *= 1.2;

                    // 回転行列の作成
                    half angleCos = cos(theta);
                    half angleSin = sin(theta);
                    half2x2 rotateMatrix = half2x2(angleCos, -angleSin, angleSin, angleCos);

                    v.vertex.xy = mul(v.vertex.xy, rotateMatrix);

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;

                    return o;
                }

                fixed4 frag(VertexOutput i) : SV_Target
                {

                    // _MainTextureを貼るためのuvを定義します．
                    // _MainTextureに設定されたタイリングとオフセットを反映させる式は下のようになります
                    // uv = uv * タイリング + オフセット
                    float2 mainUv = i.uv * _MainTexture_ST.xy + _MainTexture_ST.zw;

                    // _MainTextureをマッピングします．
                    // tex2D()を使うことで，テクスチャとuvから任意にマッピングさせることができます．
                    // 最後に_MainColorを乗算することでTint Colorとして動作させます．
                    return tex2D(_MainTexture, mainUv) * _MainColor;
                }
                ENDCG
            }
        }
}