#ifndef DTT_URP_UNLIT_SURFACE
#define DTT_URP_UNLIT_SURFACE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceData.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/AmbientOcclusion.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Debug/Debugging3D.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

#include "Include/UVMapping.hlsl"
#include "Surfaces/Input/BaseInput.hlsl"

struct VertexInput
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;

    float2 uv : TEXCOORD0;
    float2 staticLightmapUV    : TEXCOORD1;
    float2 dynamicLightmapUV    : TEXCOORD2;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct PixelInput
{
    float2 uv : TEXCOORD0;
    float fogCoord : TEXCOORD1;
    float4 positionCS : SV_POSITION;
    float3 positionWS : TEXCOORD2;
    float3 normalWS : TEXCOORD3;
    float3 viewDirWS : TEXCOORD4;

    #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    float4 shadowCoord             : TEXCOORD5;
    #endif

    DECLARE_LIGHTMAP_OR_SH(staticLightmapUV, vertexSH, 6);

#ifdef DYNAMICLIGHTMAP_ON
    float2  dynamicLightmapUV : TEXCOORD7; // Dynamic lightmap UVs
#endif

    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

PixelInput UnlitVertexPass(VertexInput input)
{
    PixelInput output = (PixelInput)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    VertexPositionInputs vertexPositionInputs = GetVertexPositionInputs(input.positionOS.xyz);

    output.positionCS = vertexPositionInputs.positionCS;
    output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
    #if defined(_FOG_FRAGMENT)
    output.fogCoord = vertexPositionInputs.positionVS.z;
    #else
    output.fogCoord = ComputeFogFactor(vertexPositionInputs.positionCS.z);
    #endif
    
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS);
    half3 viewDirWS = GetWorldSpaceViewDir(vertexPositionInputs.positionWS);
    
    output.positionWS = vertexPositionInputs.positionWS;
    output.normalWS = normalInput.normalWS;
    output.viewDirWS = viewDirWS;

    OUTPUT_LIGHTMAP_UV(input.staticLightmapUV, unity_LightmapST, output.staticLightmapUV);
    #ifdef DYNAMICLIGHTMAP_ON
    output.dynamicLightmapUV = input.dynamicLightmapUV.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
    #endif
    OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

    return output;
}

void InitializeInputData(PixelInput input, out InputData inputData)
{
    inputData = (InputData)0;
 
    inputData.positionWS = input.positionWS;

    inputData.normalWS = input.normalWS;
    inputData.viewDirectionWS = input.viewDirWS;

    inputData.shadowCoord = 0;
    inputData.fogCoord = 0;
    inputData.vertexLighting = half3(0, 0, 0);
    

    #if defined(DYNAMICLIGHTMAP_ON)
    inputData.bakedGI = SAMPLE_GI(input.staticLightmapUV, input.dynamicLightmapUV, input.vertexSH, inputData.normalWS);
    #else
    inputData.bakedGI = SAMPLE_GI(input.staticLightmapUV, input.vertexSH, inputData.normalWS);
    #endif

    #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT)
    inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
    #else
    inputData.normalizedScreenSpaceUV = 0;
    #endif

    inputData.shadowMask = SAMPLE_SHADOWMASK(input.staticLightmapUV);
}

void InitializeInputData(PixelInput input, half3 normalTS, out InputData inputData)
{
    inputData = (InputData)0;

    inputData.positionWS = input.positionWS;

    #ifdef _NORMALMAP
        half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
        inputData.tangentToWorld = half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz);
        inputData.normalWS = TransformTangentToWorld(normalTS, inputData.tangentToWorld);
    #else
        half3 viewDirWS = GetWorldSpaceNormalizeViewDir(inputData.positionWS);
        inputData.normalWS = input.normalWS;
    #endif

    inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
    viewDirWS = SafeNormalize(viewDirWS);

    inputData.viewDirectionWS = viewDirWS;

    #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
        inputData.shadowCoord = input.shadowCoord;
    #elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
        inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
    #else
        inputData.shadowCoord = float4(0, 0, 0, 0);
    #endif

    //#ifdef _ADDITIONAL_LIGHTS_VERTEX
    //    inputData.fogCoord = InitializeInputDataFog(float4(inputData.positionWS, 1.0), input.fogFactorAndVertexLight.x);
    //    inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
    //#else
    //    inputData.fogCoord = InitializeInputDataFog(float4(inputData.positionWS, 1.0), input.fogFactor);
    //    inputData.vertexLighting = half3(0, 0, 0);
    //#endif

#if defined(DYNAMICLIGHTMAP_ON)
    inputData.bakedGI = SAMPLE_GI(input.staticLightmapUV, input.dynamicLightmapUV, input.vertexSH, inputData.normalWS);
#else
    inputData.bakedGI = SAMPLE_GI(input.staticLightmapUV, input.vertexSH, inputData.normalWS);
#endif

    inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
    inputData.shadowMask = SAMPLE_SHADOWMASK(input.staticLightmapUV);

    #if defined(DEBUG_DISPLAY)
    #if defined(DYNAMICLIGHTMAP_ON)
    inputData.dynamicLightmapUV = input.dynamicLightmapUV.xy;
    #endif
    #if defined(LIGHTMAP_ON)
    inputData.staticLightmapUV = input.staticLightmapUV;
    #else
    inputData.vertexSH = input.vertexSH;
    #endif
    #endif
}

half4 OverrideUniversalFragmentUnlit(InputData inputData, SurfaceData surfaceData)
{
    half3 albedo = surfaceData.albedo;

    #if defined(DEBUG_DISPLAY)
    half4 debugColor;

    if (CanDebugOverrideOutputColor(inputData, surfaceData, debugColor))
    {
        return debugColor;
    }
    #endif

    half4 finalColor = half4(albedo + surfaceData.emission, surfaceData.alpha);

    return finalColor;
}

half4 OverrideUniversalFragmentUnlit(InputData inputData, half3 color, half alpha)
{
    SurfaceData surfaceData;

    surfaceData.albedo = color;
    surfaceData.alpha = alpha;
    surfaceData.emission = 0;
    surfaceData.metallic = 0;
    surfaceData.occlusion = 1;
    surfaceData.smoothness = 1;
    surfaceData.specular = 0;
    surfaceData.clearCoatMask = 0;
    surfaceData.clearCoatSmoothness = 1;
    surfaceData.normalTS = half3(0, 0, 1);

    return OverrideUniversalFragmentUnlit(inputData, surfaceData);
}

half4 OverrideSampleAlbedoAlpha(PixelInput input, TEXTURE2D_PARAM(albedoAlphaMap, sampler_albedoAlphaMap))
{
    #ifdef _TRIPLANAR_UV
    return TriPlanarSampling(_BaseMap, sampler_BaseMap, input.positionWS, input.normalWS, 0.25, 2.0);
    #else
    return half4(SAMPLE_TEXTURE2D(albedoAlphaMap, sampler_albedoAlphaMap,  input.uv));
    #endif
    
}

half4 OverrideUniversalFragmentBlinnPhong(InputData inputData, SurfaceData surfaceData)
{
    #if defined(DEBUG_DISPLAY)
    half4 debugColor;

    if (CanDebugOverrideOutputColor(inputData, surfaceData, debugColor))
    {
        return debugColor;
    }
    #endif

    uint meshRenderingLayers = GetMeshRenderingLightLayer();
    half4 shadowMask = CalculateShadowMask(inputData);
    AmbientOcclusionFactor aoFactor = CreateAmbientOcclusionFactor(inputData, surfaceData);
    Light mainLight = GetMainLight(inputData, shadowMask, aoFactor);

    MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI, aoFactor);
    inputData.bakedGI *= surfaceData.albedo;

    LightingData lightingData = CreateLightingData(inputData, surfaceData);
    if (IsMatchingLightLayer(mainLight.layerMask, meshRenderingLayers))
    {
        lightingData.mainLightColor += CalculateBlinnPhong(mainLight, inputData, surfaceData);
    }

    half3 attenuatedLightColor = mainLight.color * (mainLight.distanceAttenuation * mainLight.shadowAttenuation);

    half NdotL = saturate(dot(inputData.normalWS, mainLight.direction));

    // TODO: Review extra lighting pass
    // ===================================================================================================================================
    //#if defined(_ADDITIONAL_LIGHTS)
    //uint pixelLightCount = GetAdditionalLightsCount();

    //#if USE_CLUSTERED_LIGHTING
    //for (uint lightIndex = 0; lightIndex < min(_AdditionalLightsDirectionalCount, MAX_VISIBLE_LIGHTS); lightIndex++)
    //{
    //    Light light = GetAdditionalLight(lightIndex, inputData, shadowMask, aoFactor);
    //    if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
    //    {
    //        lightingData.additionalLightsColor += CalculateBlinnPhong(light, inputData, surfaceData);
    //    }
    //}
    //#endif

    //LIGHT_LOOP_BEGIN(pixelLightCount)
    //    Light light = GetAdditionalLight(lightIndex, inputData, shadowMask, aoFactor);
    //    if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
    //    {
    //        lightingData.additionalLightsColor += CalculateBlinnPhong(light, inputData, surfaceData);
    //    }
    //LIGHT_LOOP_END
    //#endif

    //#if defined(_ADDITIONAL_LIGHTS_VERTEX)
    //lightingData.vertexLightingColor += inputData.vertexLighting * surfaceData.albedo;
    //#endif
    // ===================================================================================================================================

    return CalculateFinalColor(lightingData, surfaceData.alpha);
}

inline void OverrideInitializeSimpleLitSurfaceData(PixelInput input, out SurfaceData surfaceData)
{
    surfaceData = (SurfaceData)0;

    half4 albedoAlpha = OverrideSampleAlbedoAlpha(input, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));

    surfaceData.alpha = albedoAlpha.a * _BaseColor.a;
    AlphaDiscard(surfaceData.alpha, _Cutoff);

    surfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;
#ifdef _ALPHAPREMULTIPLY_ON
    surfaceData.albedo *= surfaceData.alpha;
#endif
   
    surfaceData.metallic = 0.0; // unused
    surfaceData.specular = 0.0;
    surfaceData.smoothness = 0.0;
    surfaceData.normalTS = 0;
    surfaceData.occlusion = 1.0;
    surfaceData.emission = 0.0;
}

half4 UnlitPixelPass(PixelInput input) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    SurfaceData surfaceData = (SurfaceData)0;
    OverrideInitializeSimpleLitSurfaceData(input, surfaceData);

    InputData inputData;
    InitializeInputData(input, surfaceData.normalTS, inputData);
    SETUP_DEBUG_TEXTURE_DATA(inputData, input.uv, _BaseMap);

#ifdef _DBUFFER
    ApplyDecalToSurfaceData(input.positionCS, surfaceData, inputData);
#endif

    half4 color = OverrideUniversalFragmentBlinnPhong(inputData, surfaceData);
    color.rgb = MixFog(color.rgb, inputData.fogCoord);
    color.a = OutputAlpha(color.a, _Surface);

    return color;
}

#endif // DTT_URP_UNLIT_SURFACE