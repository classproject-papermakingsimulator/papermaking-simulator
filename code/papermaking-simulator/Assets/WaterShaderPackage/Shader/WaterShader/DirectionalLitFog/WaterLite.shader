Shader "Water/WaterLite" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_LightOffset ("Light strength offset", Range(-1,1)) = 1
		_NormalStrength ("Normal strength", Range(0,2)) = 1

		_Shininess ("Shininess", float) = 200
		
		_EdgeFade ("Edge fade", Range(0,3)) = 0.5
		_ShallowColor ("Shallow color tint", Color) = (1,1,1,1)
		_DeepSeaColor ("Deep sea color tint", Color) = (.7,.7,.7,1)
		
		_ShallowDepth ("ShallowDepth", float) = 0.5
		_ShallowDeepFade ("Shallow-Deep-Fade", float) = 0.5
		
		_Reflectivity ("Reflectivity", Range(-1,1)) = 0
	}
	SubShader 
	{
		// Subshader Tags
		Tags { "Queue"="Transparent-1" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		
		// Render Pass
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "WaterShaderHelper.cginc"
			#pragma target 2.0
			
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
	
	
		////// Uniform user variable definition
			uniform sampler2D _MainTex;					// color texture
			uniform float4 _MainTex_ST;					// Scale and offset of the normal map
			uniform float _LightOffset;					// light strength offset
			uniform float _NormalStrength;				// normal map multiplier
			uniform sampler2D _NormalTex;				// The water normal map
			uniform float4 _NormalTex_ST;				// Scale and offset of the normal map

			uniform float _Shininess;					// specular spread
			
			uniform float4 _Movement; 					// The uv-movement
			uniform float4 _UVOffset; 					// The uv-offset (pre calculated movement * time)
			uniform float _EdgeFade;					// Fading from shore to shallow water
			uniform float4 _ShallowColor;				// The color factor for shallow water
			uniform float4 _DeepSeaColor;				// The color factor for deep sea
			
			uniform float _Reflectivity;				// relection multiplier
			uniform float _ShallowDepth;				// 0-1 tuning (0-shallow-deep-1)
			uniform float _ShallowDeepFade;				// 0-1 fading from shallow to deep
			
			// Auto generated reflection texture
			uniform sampler2D _ReflectionTex;			// 
			
			// Unity variable definition
			uniform sampler2D _LastCameraDepthTexture;	// 
			
			
		////// Helper functions
			// can be found in "WaterShaderHelper.cginc"
			
	
		////// Shader functions
			// Vertex shader
			Vert2FragPro vert(VertexInputPro vertIn)
			{
				Vert2FragPro output;
				output = HandleVertexInput(vertIn, _MainTex_ST, _NormalTex_ST, _UVOffset, 1);
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2FragPro fragIn) : SV_Target
			{
				init_frag(fragIn);
				
				// Value definitions
				float3 viewDir = normalize(fragIn.viewDir);
				float3 lightDir = _WorldSpaceLightPos0;
				float3 mainColor = GetMainColor(_MainTex, fragIn.uv_Tex);

				// Normal mapping
				float3 normalDir = GetNormal(_NormalTex, fragIn.uv, _NormalStrength);
				 
				// Light
				float3 diffuseLight = DiffuseLightSimple(normalDir, lightDir, _LightOffset);
				float3 specularLight = SpecularLightSimple(normalDir, lightDir, viewDir, _Shininess);
				
				// Depth fog
				float2 depthValues = GetDepthValues(fragIn.posScreen, _LastCameraDepthTexture, _ShallowDeepFade, _ShallowDepth, _EdgeFade);
				float depthLevel = depthValues.x;
				float waterLevel = depthValues.y;

				float3 waterDepthTint = GetDepthTint(waterLevel, _DeepSeaColor, _ShallowColor);
				
				// Reflection
				float reflectivity = GetReflectivity(viewDir, _Reflectivity);
				float3 reflection = GetReflection(fragIn.posScreen, _ReflectionTex, normalDir, 0) * reflectivity;
				
				// Final composition
				float4 final = float4(GetFinalColor(diffuseLight, waterDepthTint, mainColor, specularLight, reflection, float3(0,0,0), reflectivity), 
										GetFinalAlpha(depthLevel, specularLight.x, 0, 0));
				
				UNITY_APPLY_FOG(fragIn.fogCoord, final);

				return(final);  
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
