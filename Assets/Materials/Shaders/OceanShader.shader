Shader "Unlit/Ocean"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Speed ("Speed", Float) = 1.0
        _WavesNum ("Number of Waves", float) = 4.0
        _GlobalLight ("Global Light", Color) = (1, 1, 1, .2)
        _Smoothness ("Smoothness", Range(0, 1)) = 1
        _Metalic ("Metalic", Range(0, 1)) = 1
        _NoiseMap ("Noise Map", 2D) = "white"{}
        _NoiseIntensity ("Noise Intensity", float) = 1
        _FoamColor ("Sea Foam Color", Color) = (1, 1, 1, 1)
        _FoamStrength ("Foam Strength", float) = 1
        _FoamHeight ("Foam Height", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            // Properties
            float4 _Color;
            float _Speed;
            float _Gravity = 9.8;
            float _WavesNum;
            float4 _GlobalLight;
            float _Smoothness;
            float _Metalic;
            sampler2D _NoiseMap;
            float _NoiseIntensity;
            float4 _FoamColor;
            float _FoamStrength;
            float _FoamHeight;

            // Wave settings (adjust NUM_WAVES for more detail change max here)
            float4 _WaveData[32]; // (dirX, dirZ, wavelength, steepness)
            float _WaveSpeeds[32]; // speed multiplier for each wave

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 clipPos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 worldPos : TEXCOORD1;
                float4 vertex : TEXCOORD2;
            };

            // Gerstner Wave Function
            float3 GerstnerWave(float3 worldPos, float2 direction, float wavelength, float steepness, float speed, float time)
            {
                float k = 2.0 * UNITY_PI / wavelength;  // Wave number
                float c = sqrt(_Gravity / k);           // Wave speed
                float a = steepness / k;                // Amplitude

                // Compute phase, moving in wave direction over time
                float phase = k * (dot(worldPos.xz, direction) - time * speed);

                // Apply displacement
                float3 displacedPos = worldPos;
                displacedPos.x += direction.x * a * cos(phase);
                displacedPos.z += direction.y * a * cos(phase);
                displacedPos.y += a * sin(phase);

                return displacedPos;
            }

            float3 calculateTotalDisplacement(float3 worldPos)
            {
                for (int i = 0; i < _WavesNum; i++)
                {
                    float2 dir = normalize(_WaveData[i].xy);
                    float wavelength = _WaveData[i].z;
                    float steepness = _WaveData[i].w;

                    worldPos = GerstnerWave(worldPos, dir, wavelength, steepness, _WaveSpeeds[i], _Time.y);
                }

                return worldPos;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                float3 displacedPos = calculateTotalDisplacement(worldPos);

                o.worldPos = displacedPos;

                // Transform back to object space
                o.vertex = mul(unity_WorldToObject, float4(displacedPos, 1.0));
                o.clipPos = UnityObjectToClipPos(o.vertex);

                // recalculate normals
                float3 tangentX = calculateTotalDisplacement(worldPos + float3(1, 0, 0)) - displacedPos;
                float3 tangentZ = calculateTotalDisplacement(worldPos + float3(0, 0, 1)) - displacedPos;

                o.worldNormal = normalize(cross(tangentZ, tangentX));

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float baseVal = max(0, i.worldPos.y - _FoamHeight + 1);
                float foamStrength = saturate(pow(baseVal, _FoamStrength));
                
                float4 albedo = lerp(_Color, _FoamColor, foamStrength);

                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

                // diffuse light
                float diffuseAmount = saturate(dot(i.worldNormal, lightDirection));
                float4 diffuseLighting = _LightColor0 * diffuseAmount;

                // global light
                float4 globalIllumination = float4(_GlobalLight.rgb * _GlobalLight.a, 1);

                // specular light
                float3 toCamera = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 reflection = reflect(-lightDirection, i.worldNormal);

                float specularPower = lerp(0.1, 32, _Smoothness);

                float dt = saturate(dot(toCamera, reflection));
                float specularEffect = pow(dt, specularPower);

                float4 specularColor = lerp(albedo, _LightColor0, _Metalic);

                return (albedo * diffuseLighting) + (albedo * globalIllumination) + (specularColor * specularEffect);
            }
            ENDCG
        }
    }
}
