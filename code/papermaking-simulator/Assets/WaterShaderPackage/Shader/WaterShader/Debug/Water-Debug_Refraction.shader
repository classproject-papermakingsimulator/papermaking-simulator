// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/Refraction" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_NormalStrength ("Normal strength", Range(0,1)) = 1
		_NormalTex ("Normalmap", 2D) = "blue" {}

		_Movement ("Movement", Vector) = (0.01,0.01,-0.01,0)
		_UVOffset ("Auto.gen", Vector) = (0,0,0,0)

		_ReflectionTex ("Reflection", 2D) = "black" {}
		_Reflectivity ("Reflectivity", float) = 0
		_Refraction ("Refraction", float) = 2
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
			uniform float _NormalStrength;			//
			uniform sampler2D _NormalTex;			// The main water texture (Normal+greyscale)
			uniform float4 _NormalTex_ST;			// Scale and offset of the main texture

			uniform float4 _Movement; 				// The uv-offset movement of the waves xy direction, z speed
			uniform float4 _UVOffset; 				

			uniform float _Reflectivity;
			uniform float _Refraction;
			
			// Auto generated reflection texture
			uniform sampler2D _ReflectionTex;

	
		////// Input structs
			struct VertexInput 
			{
				float4 vertex : POSITION;
				float2 uv_NormalTex : TEXCOORD0;
			};
			struct Vert2Frag
			{
				float4 pos : SV_POSITION;
				float4 posScreen : TEXCOORD1;
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
				output.posScreen = ComputeScreenPos(output.pos);
				output.viewDir = (WorldSpaceViewDir(vertIn.vertex));
				output.uv.xy = vertIn.uv_NormalTex * _NormalTex_ST.xy + _NormalTex_ST.zw + _UVOffset.xy;
				output.uv.zw = vertIn.uv_NormalTex * _NormalTex_ST.xy + _NormalTex_ST.zw + _UVOffset.zw;
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{				
				// Value definitions
				float3 viewDir = normalize(fragIn.viewDir);
				
				// Normal mapping
				float3 normalDir = GetNormal(_NormalTex, fragIn.uv, _NormalStrength);

				// Reflection
				float reflectivity = GetReflectivity(viewDir, _Reflectivity);
				float4 reflection = GetReflection(fragIn.posScreen, _ReflectionTex, normalDir, _Refraction) * reflectivity;
				
				// Final composition
				float4 final = reflection;

				return(final);  
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
