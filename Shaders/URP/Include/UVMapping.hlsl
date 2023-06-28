#ifndef DTT_URP_SURFACE_BASE_INPUT
#define DTT_URP_SURFACE_BASE_INPUT

#include "Maths.hlsl"

inline float3 TriPlanarBlend(float3 normal, float exp)
{
	float3 n = pow(abs(normal), exp);
	float3 b = abs(float3(dot(float3(1, 0, 0), n), dot(float3(0, 1, 0), n), dot(float3(0, 0, 1), n)));
	float f = (b.x + b.y + b.z);
	b /= float3(f, f, f);
	return b;
}

inline float4 TriPlanarSampling(Texture2D Tex, SamplerState textureSampler, float3 vPos, float3 normal, float scale, float exp)
{
	float3 t = TriPlanarBlend(normal, exp);
	float3 s = vPos * scale;
	float4 x = SAMPLE_TEXTURE2D(Tex, textureSampler, s.yz);
	float4 y = SAMPLE_TEXTURE2D(Tex, textureSampler, s.xz);
	float4 z = SAMPLE_TEXTURE2D(Tex, textureSampler, s.xy);
	return (x * t.x) + (y * t.y) + (z * t.z);
}

inline float2 RotateTexCoords(float2 uv, float angle, float2 center)
{
	angle *= DEGTORAD;
	float cosAngle = cos(angle);
	float sinAngle = sin(angle);
	return mul(uv - center, float2x2(cosAngle, -sinAngle, sinAngle, cosAngle)) + center;
}

#endif // DTT_URP_SURFACE_BASE_INPUT