#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
StructuredBuffer<float3> _AllInstancesTransformBuffer;
StructuredBuffer<uint> _VisibleInstanceOnlyTransformIDBuffer;
#endif

float3 position;

void ConfigureProcedural () {
    #if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
    uint instanceID = _VisibleInstanceOnlyTransformIDBuffer[unity_InstanceID];
    position = _AllInstancesTransformBuffer[instanceID];
    #endif
}

void ShaderGraphFunction_float (out float3 PositionOut) {
    PositionOut = position;
}

void ShaderGraphFunction_half (out half3 PositionOut) {
    PositionOut = position;
}