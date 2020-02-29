// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/Specular" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_AmbientMultiplier ("Ambient strength", Range(0,10)) = 1
		_NormalStrength ("Normal strength", Range(0,1)) = 1
		_NormalTex ("Normalmap", 2D) = "blue" {}
		_Shininess ("Shininess", float) = 200
		
		_Movement ("Movement", Vector) = (0.01,0.01,-0.01,0)
		_UVOffset ("Auto.gen", Vector) = (0,0,0,0)
		_LightDir ("LightDirection", Vector) = (0,1,0)
	}
	SubShader 
	{
		// Subshader Tags
		Tags { "Queue"="Geometry" "RenderType"="Opaque" }
		
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
	
	
		////// Uniform user variable definition
			uniform float _AmbientMultiplier;		//
			uniform float _NormalStrength;			//
			uniform sampler2D _NormalTex;			// The main water texture (Normal+greyscale)
			uniform float4 _NormalTex_ST;			// Scale and offset of the main texture

			uniform float _Shininess;				// Control the specular spread
			uniform float4 _LightDir; 				// Light direction
			
			uniform float4 _Movement; 				// The uv-offset movement of the waves xy direction, z speed
			uniform float4 _UVOffset; 				// To fake directional light

	
		////// Input structs
			struct VertexInput 
			{
				float4 vertex : POSITION;
				float2 uv_NormalTex : TEXCOORD0;
			};
			struct Vert2Frag
			{
				float4 pos : SV_POSITION;
				float3 viewDir : TEXCOORD2;
				float4 uv : TEXCOORD3;
			};
			
			
		////// Helper functions
			// can be found in "WaterShaderHelper.cginc"
			
	
		////// Shader functions
			// Vertex shader
			Vert2Frag vert(VertexInput vertIn)
			{
				Vert2Frag output;
				
				output.pos = UnityObjectToClipPos(vertIn.vertex);
				output.viewDir = WorldSpaceViewDir(vertIn.vertex);
				output.uv.xy = vertIn.uv_NormalTex * _NormalTex_ST.xy + _NormalTex_ST.zw + _UVOffset.xy;
				output.uv.zw = vertIn.uv_NormalTex * _NormalTex_ST.xy + _NormalTex_ST.zw + _UVOffset.zw;
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{				
				// Value definitions
				float3 viewDir = normalize(fragIn.viewDir);
				float3 lightDir = normalize(_LightDir.xyz);
				
				// Normal mapping 
				float3 normalDir = GetNormal(_NormalTex, fragIn.uv, _NormalStrength);

				// Light
				float3 diffuseLight = DiffuseLightSimple(normalDir, lightDir, _AmbientMultiplier);
				float3 specularLight = SpecularLightSimple(normalDir, lightDir, viewDir, _Shininess);
				
				// Final composition
				return float4( saturate(diffuseLight + specularLight) ,1);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
