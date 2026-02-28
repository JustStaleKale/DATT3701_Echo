Shader "Custom/PalettePostProcess"
{
    Properties
    {
        _MainTex ("Source", 2D) = "white" {}
        _PaletteTex ("Palette", 2D) = "white" {}
        _PaletteSize ("Palette Size", Float) = 36
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Name "PalettePass"
            ZWrite Off
            Cull Off
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D(_PaletteTex);
            SAMPLER(sampler_PaletteTex);

            float _PaletteSize;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // finding positions 
            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            // going through and mapping to closest colour on palette
            half4 frag (Varyings input) : SV_Target
            {
                float3 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv).rgb;

                float3 closestColor = 0;
                float minDistance = 9999;

                for (int i = 0; i < _PaletteSize; i++)
                {
                    float u = (i + 0.5) / _PaletteSize;
                    float3 paletteColor = SAMPLE_TEXTURE2D(_PaletteTex, sampler_PaletteTex, float2(u, 0.5)).rgb;

                    float dist = distance(col, paletteColor);

                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closestColor = paletteColor;
                    }
                }

                return float4(closestColor, 1.0);
            }

            ENDHLSL
        }
    }
}