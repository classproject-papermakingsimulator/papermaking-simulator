#ifndef WATER_SHADER_HELPER_DEBUG
#define WATER_SHADER_HELPER_DEBUG

#include "UnityCG.cginc"

	/* Created: Martin Reintges 01.12.2016
	 * Last Update: Martin Reintges 26.02.2017
	 */

// Normal mapping
	float3 GetNormal(sampler2D normalMap, float4 uv, float normalStrength)
	{
		float3 normal1 = saturate(UnpackNormal( tex2D(normalMap, uv.xy ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );
		float3 normal2 = saturate(UnpackNormal( tex2D(normalMap, uv.zw ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );

		return normal1 - float3(normal2.x,0,normal2.z);
	}
	
	
// Light
	float3 DiffuseLightSimple(float3 normalDir, float3 lightDir, float ambientMultiplier)
	{
		return max(dot(normalDir,lightDir),UNITY_LIGHTMODEL_AMBIENT * ambientMultiplier);
	}

	float3 SpecularLightSimple(float3 normalDir, float3 lightDir, float3 viewDir, float shininess)
	{
		return ( pow( saturate(dot(reflect(-lightDir, normalize(normalDir)), viewDir)), shininess) );
	}
	
	
// Depth
	float2 GetDepthValues(float4 posScreen, sampler2D depthTexture, float shallowDeepFade, float shallowDepth, float edgeFade)
	{
		float depth = SAMPLE_DEPTH_TEXTURE_PROJ(depthTexture, UNITY_PROJ_COORD(posScreen));
		depth = LinearEyeDepth(depth);
		float depthLevel = (depth - posScreen.w);
		float waterLevel = (depth - posScreen.w)*shallowDeepFade - shallowDepth;
		depthLevel = edgeFade * depthLevel;
		return float2(depthLevel, saturate(waterLevel));
	}
	

// Waves
	float2 GetMovement(float4 movement)
	{
		return( movement.xy*_Time.x*movement.z );
	}
	
// Foam
	float3 GetFoam(sampler2D environmentMap, float2 uv, float groundLevel, float distance)
	{
		float3 foam = tex2D(environmentMap, uv);
		//float3 foam = float3(value, value, value);

		float height = max(1-distance, groundLevel);
		height = min(1.0, height);
		
		height = (height-(1-distance)) / distance;

		return foam * height;
	}
	
// Reflection
	float4 GetReflection(float4 posScreen, sampler2D reflectionTex, float3 normalDir, float refraction)
	{
		return tex2Dproj( reflectionTex, UNITY_PROJ_COORD(posScreen)+normalDir.xzxz*refraction );
	}

	float4 GetReflectivity(float3 viewDir, float reflectivity)
	{
		return saturate((1-dot(float3(0,1,0), abs(viewDir))) + reflectivity);
	}
#endif