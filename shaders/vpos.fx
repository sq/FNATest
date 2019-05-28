#include "..\..\Fracture\Squared\RenderLib\Shaders\ViewTransformCommon.fxh"
#include "..\..\Fracture\Squared\RenderLib\Shaders\TargetInfo.fxh"

float4 TransformPosition (float4 position) {
    float4 modelViewPos = mul(position, GetViewportModelViewMatrix());
    float4 result = mul(modelViewPos, GetViewportProjectionMatrix());
    return result;
}

void vertexShader (
    in float4 position : POSITION0,
    out float4 result : POSITION0
) {
    float4 transformedPosition = TransformPosition(position);
    result = float4(transformedPosition.xy, 0, transformedPosition.w);
}

void pixelShader (
    ACCEPTS_VPOS,
    out float4 result : COLOR0
) {
    float2 vpos = GET_VPOS;
    result = float4(vpos.x / 1920, vpos.y / 1280, 0, 1);
}

technique vposShader {
    pass P0
    {
        vertexShader = compile vs_3_0 vertexShader();
        pixelShader = compile ps_3_0 pixelShader();
    }
}