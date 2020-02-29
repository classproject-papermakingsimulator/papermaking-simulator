#ifndef WATER_SHADER_HELPER
#define WATER_SHADER_HELPER

#include "UnityCG.cginc"

	/* 
	 * Created by Martin Reintges
	 */
	 
////// Input structs
	struct VertexInputPro 
	{
		float4 vertex : POSITION;
		float2 uv_NormalTex : TEXCOORD0;
	};

	struct Vert2FragPro
	{
		float4 pos : SV_POSITION;
		float4 posWorld : TEXCOORD0;
		float4 posScreen : TEXCOORD1;
		float3 viewDir : TEXCOORD2;
		float4 uv : TEXCOORD3;
		float4 uv_Environment : TEXCOORD4;
		float2 uv_Tex : TEXCOORD5;
		UNITY_FOG_COORDS(6)
		UNITY_VERTEX_OUTPUT_STEREO
	};



		
////// Vertex function
	Vert2FragPro HandleVertexInput(VertexInputPro vertIn, float4 mainTexConfig, float4 normalTexConfig, float4 uvOffset, float foamScale)
	{
		Vert2FragPro output;
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
		output.pos = UnityObjectToClipPos(vertIn.vertex);
				
		output.posWorld = mul(unity_ObjectToWorld, vertIn.vertex);
		output.viewDir = WorldSpaceViewDir(vertIn.vertex);
		output.posScreen = ComputeScreenPos(output.pos);

		// Handing over the uv coordinates
		output.uv.xy = UnityStereoScreenSpaceUVAdjust(vertIn.uv_NormalTex, float4(normalTexConfig.xy, normalTexConfig.zw + uvOffset.xy));
		output.uv.zw = UnityStereoScreenSpaceUVAdjust(vertIn.uv_NormalTex, float4(normalTexConfig.xy, normalTexConfig.zw + uvOffset.zw));
		/*output.uv.xy = vertIn.uv_NormalTex * normalTexConfig.xy + normalTexConfig.zw + uvOffset.xy;
		output.uv.zw = vertIn.uv_NormalTex * normalTexConfig.xy + normalTexConfig.zw + uvOffset.zw;*/
		float2 foamUv = (vertIn.uv_NormalTex + uvOffset.xy) * foamScale;
		output.uv_Environment = float4(0,0,foamUv);
		output.uv_Tex = UnityStereoScreenSpaceUVAdjust(vertIn.uv_NormalTex, float4(mainTexConfig.xy, mainTexConfig.zw + uvOffset.xy));
		UNITY_TRANSFER_FOG(output,output.pos);
		
		return output;
	}

	Vert2FragPro HandleVertexInput(VertexInputPro vertIn, float4 mainTexConfig, float4 normalTexConfig, float4 heightMaptexConfig, float4 uvOffset, float foamScale)
	{
		Vert2FragPro output;
		output = HandleVertexInput(vertIn, mainTexConfig, normalTexConfig, uvOffset, foamScale);

		float2 heightMapUv = UnityStereoScreenSpaceUVAdjust(vertIn.uv_NormalTex, heightMaptexConfig);
		output.uv_Environment += float4(heightMapUv.xy,0,0);
		return output;
	}

////// Fragment function
	void init_frag(Vert2FragPro fragIn)
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(fragIn);
	}



// Main color
	float3 GetMainColor(sampler2D mainTex, float2 uv)
	{
		return tex2D(mainTex, uv.xy);
	}

// Normal mapping
	float3 GetNormal(sampler2D normalMap, float4 uv, float normalStrength)
	{
		float3 normal1 = saturate(UnpackNormal( tex2D(normalMap, uv.xy ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );
		float3 normal2 = saturate(UnpackNormal( tex2D(normalMap, uv.zw ) ).xzy*normalStrength + float3(0,1-normalStrength,0) );

		return normal1 - float3(normal2.x,0,normal2.z);
	}
	
	
// Light
	float3 DiffuseLightSimple(float3 normalDir, float3 lightDir, float lightOffset)
	{
		return max(dot(normalDir,lightDir)+float3(lightOffset,lightOffset,lightOffset), UNITY_LIGHTMODEL_AMBIENT);
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
		return float2(saturate(depthLevel), saturate(waterLevel));
	}

	float2 GetDepthValuesIso(float4 posScreen, sampler2D depthTexture, float shallowDeepFade, float shallowDepth, float edgeFade)
	{
		float depth = tex2D(depthTexture, posScreen);
		float depthLevel = (posScreen.z - depth);
		float waterLevel = (depth - posScreen.z)*shallowDeepFade - shallowDepth;
		depthLevel = edgeFade * depthLevel;
		return float2(saturate(depthLevel), saturate(waterLevel));
	}

	float GetDepthValue(float4 posScreen, sampler2D depthTexture, float edgeFade)
	{
		float depth = SAMPLE_DEPTH_TEXTURE_PROJ(depthTexture, UNITY_PROJ_COORD(posScreen));
		return edgeFade * (LinearEyeDepth(depth) - posScreen.w);
	}
	
	float GetWaterLevel(sampler2D heightMap, float4 uv, float shallowDeepFade, float shallowDepth)
	{
		float3 height = tex2D(heightMap, uv.xy);
		float waterLevel = height*shallowDeepFade - shallowDepth;
		return saturate(waterLevel);
	}

// DepthTint
	float3 GetDepthTint(float waterLevel, float3 deepSeaColor, float3 shallowColor)
	{
		return saturate(waterLevel*deepSeaColor) + saturate((1-waterLevel)*shallowColor);
	}
	
// Foam
	float GetFoamValue(float depthLevel, float offset, float fade)
	{
		float value = saturate((depthLevel+offset)*fade);
		return 1-value;
	}

	float3 GetFoam(sampler2D foamTex, float2 uv, float depthLevel, float offset, float fade)
	{
		float3 foam = tex2D(foamTex, uv);
		return foam * GetFoamValue(depthLevel, offset, fade);
	}

	float3 GetFoamHeightMap(sampler2D heightMap, sampler2D foamTex, float4 uv, float offset, float fade)
	{
		float3 height = tex2D(heightMap, uv.xy);
		float3 foam = tex2D(foamTex, uv.zw);
		return foam * GetFoamValue(height, offset, fade);
	}
	
// Reflection
	float3 GetReflection(float4 posScreen, sampler2D reflectionTex, float3 normalDir, float refraction)
	{
		return tex2Dproj(reflectionTex, UNITY_PROJ_COORD(posScreen)+normalDir.xzxz*refraction).xyz;
	}

	float GetReflectivity(float3 viewDir, float reflectivity)
	{
		return saturate((1-dot(float3(0,1,0), abs(viewDir))) + reflectivity);
	}

// Final composition
	float3 GetFinalColor(float3 mainColor, float3 diffuseLight, float3 waterTint, float3 specularLight, float3 reflection, float3 foam, float reflectivity)
	{
		return (diffuseLight * waterTint * mainColor + specularLight) * (1-reflectivity) + foam + reflection;
	}

	float GetFinalAlpha(float depthLevel, float specularLight, float edgeIndicator, float foam)
	{
		return max(max(max(depthLevel, specularLight), edgeIndicator), foam);
	}

#endif