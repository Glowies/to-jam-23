Shader "Unlit/HeartGauge" {
    Properties {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        _CorruptionTex ("Texture", 2D) = "white" {}

        _HeartFill ("HeartFill", Range(0, 100)) = 100
        _CorruptionFill ("CorruptionFill", Range(0, 100)) = 100
        
        _BackgroundColor ("BackgroundColor", Color) = (1, 1, 1, 1)
        _CorruptionColor ("CorruptedColor", Color) = (1, 1, 1, 1)
        _GradientColorA ("GradientColorA", Color) = (1, 1, 1, 1)
        _GradientColorB ("GradientColorB", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        // Pass to render the background color
        Pass {
            Tags { "Queue"="Transparent" }
            ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _BackgroundColor;

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Interpolators vert (MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target {
                return _BackgroundColor;
            }
            ENDCG
        }
        
        // Pass to render the heart rate and corruption of the heart fill
        Pass {
            Tags { "Queue"="Transparent" }
            ZWrite Off
            Blend DstColor SrcColor

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _BackgroundColor, _CorruptionColor, _GradientColorA, _GradientColorB;;
            float _HeartFill, _CorruptionFill;

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _CorruptionTex;
            float4 _CorruptionTex_ST;

            sampler2D _Texture;

            Interpolators vert (MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _CorruptionTex);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target {
                float heartfillMask = (_HeartFill / 100) > i.uv.y;
                float corruptionfillMask = (_CorruptionFill / 100) > i.uv.y;

                float4 fillColor = lerp(_GradientColorA, _GradientColorB, i.uv.y);
                float3 backgroundColor = _BackgroundColor;

                float3 gradientColor = lerp(backgroundColor, fillColor, heartfillMask);
                float3 outputColor = lerp(_BackgroundColor, gradientColor, heartfillMask);
                
                float3 finalColor = lerp(outputColor, tex2D(_CorruptionTex, i.uv) * _CorruptionColor, corruptionfillMask);
                return float4(finalColor, 1);
            }
            ENDCG
        }
    }
}