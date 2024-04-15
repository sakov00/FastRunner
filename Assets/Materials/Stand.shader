Shader "MyShader/Mask"
{
    Properties
    {
        [IntRange] _Stencilref("StencilRef", Range(0,255)) = 0
    }
        SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry-1"
        }

        Blend Zero One
        Zwrite Off

        Stencil
        {
            Ref[_Stencilref]
            Comp Always
            Pass Replace
        }

        Pass
        {
        }
    }
}