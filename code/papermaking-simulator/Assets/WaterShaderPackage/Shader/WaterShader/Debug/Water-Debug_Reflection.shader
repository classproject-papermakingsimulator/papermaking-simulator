// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/Reflection" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_ReflectionTex ("Reflection", 2D) = "black" {}
		_Reflectivity ("Reflectivity", float) = 0
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
			uniform float _Reflectivity;
			
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
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{				
				// Value definitions
				float3 viewDir = normalize(fragIn.viewDir);
				
				// Normal mapping fragIn.uv
				float3 normalDir = float3(0,1,0);

				// Reflection
				float reflectivity = GetReflectivity(viewDir, _Reflectivity);
				float4 reflection = GetReflection(fragIn.posScreen, _ReflectionTex, normalDir, 0) * reflectivity;
				
				// Final composition
				float4 final = reflection;

				return(final);  
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
