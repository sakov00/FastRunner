Shader "ShellTexture/Grass1"
{
    Properties
    {
        [Header(ShellSettings)]
        _ShellColor ("Shell Color", Color) = (0,0,0)
        _CrossColor ("Cross Color", Color) = (0,0,0)
        _Density ("Density", Float) = 0
        _MinHeight ("Min Height", Float) = 0
        _MaxHeight ("Max Height", Float) = 0
        _Thickness ("Thickness", Float) = 0
        _FlowMap ("FlowMap", 2D) = "white"
        _CrossFlowMap ("CorssFlowMap", 2D) = "white"
        _WindFrequency ("Wind Frequency", Float) = 0
        _WindAmplitude ("Wind Amplitude", Float) = 0
        _HeightmapMultiplier ("Heightmap", Float) = 0
        _TopColorMultiplier ("Top Color Mul", Float) = 0

        [Header(Lighting)]
        _Attenuation ("Attenuation", Float) = 0
        _OcclusionBias ("Occlusion Bias", Float) = 0

        [HideInInspector]
        _ShellIndex ("Shell Index", Float) = 0

        [HideInInspector]
        _ShellCount ("Shell Count", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType"="Opaque"
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }

        ZWrite On
        Cull Back

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

        #pragma multi_compile_instancing

        TEXTURE2D(_FlowMap);
        SAMPLER(sampler_FlowMap);


        TEXTURE2D(_CrossFlowMap);
        SAMPLER(sampler_CrossFlowMap);

        CBUFFER_START(UnityPerMaterial)
        CBUFFER_END

        UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(int, _ShellIndex)
        UNITY_INSTANCING_BUFFER_END(Props)

        CBUFFER_START(UnityPerMaterial)
            float _TopColorMultiplier;
            float _WindFrequency;
            float _WindAmplitude;
            float _HeightmapMultiplier;
            float _MinHeight;
            float _MaxHeight;
            float3 _ShellColor;
            float3 _CrossColor;
            float _Density = 20;
            float _Thickness = 2;
            float _Attenuation;
            float _ShellDistanceAttenuation;
            float _OcclusionBias;
            int _ShellCount = 1;
        CBUFFER_END

        float hash(uint n)
        {
            n = (n << 13U) ^ n;
            n = n * (n * n * 15731U + 0x789221U) + 0x1376312589U;
            return float(n & uint(0x7fffffffU)) / float(0x7fffffff);
        }

        struct app_data
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
            float3 normal : NORMAL;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct interpolators
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 normal : TEXCOORD1;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        interpolators vert(app_data v)
        {
            interpolators o;
            UNITY_SETUP_INSTANCE_ID(v)
            const float height = (float)UNITY_ACCESS_INSTANCED_PROP(Props, _ShellIndex) / 100;
            float3 flowSample = SAMPLE_TEXTURE2D_LOD(_FlowMap, sampler_FlowMap, v.uv + float2(-cos(_Time.y/15), -sin(_Time.y/15)), 0);
            float3 crossFlowSample = SAMPLE_TEXTURE2D_LOD(_CrossFlowMap, sampler_CrossFlowMap, v.uv + float2(sin(_Time.y/10), cos(_Time.y/10)), 0);
            
            v.vertex.xz += sin(_Time.y * _WindFrequency) * flowSample.b * -crossFlowSample.b * _WindAmplitude * pow(height, 2);
            v.vertex.y += SAMPLE_TEXTURE2D_LOD(_FlowMap, sampler_FlowMap, v.uv, 1).b * _HeightmapMultiplier;

            o.vertex = TransformObjectToHClip(v.vertex.xyz);
            o.uv = v.uv;
            o.normal = normalize(TransformObjectToWorldNormal(v.normal));
            UNITY_TRANSFER_INSTANCE_ID(v, o);
            return o;
        }

        float4 frag(interpolators i) : SV_Target
        {
            UNITY_SETUP_INSTANCE_ID(i);
            const float2 densited_uv = i.uv * _Density;
            const float2 local_uv = frac(densited_uv) * 2 - 1;
            const float d = length(local_uv);

            const uint2 tid = densited_uv;
            const uint seed = tid.x + 100 * tid.y + 100 * 10;

            const float shell_index = (float)UNITY_ACCESS_INSTANCED_PROP(Props, _ShellIndex);
            const float shell_count = _ShellCount;

            const float flowmapB = SAMPLE_TEXTURE2D(_FlowMap, sampler_FlowMap, i.uv).b;
            
            const float random = lerp(_MinHeight/100, _MaxHeight/100, hash(seed));
            const float h = shell_index / shell_count;

            const int outside_thickness = d > _Thickness * (random - pow(h, 3) + 0.015) * flowmapB * 0.1;

            if (outside_thickness && shell_index > 0) discard;

            // lighting & output
            const float ambientOcclusion = saturate(pow(h, _Attenuation + _OcclusionBias));
            const float halfLambertSqr = pow(dot(i.normal, _MainLightPosition.xyz) * 0.5 + 0.5, 2);
            return float4(lerp(_ShellColor, _CrossColor, flowmapB * _TopColorMultiplier) * halfLambertSqr * ambientOcclusion, 1);
        }
        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDHLSL
        }
    }
}