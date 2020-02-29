// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/ScreenPos" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
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
			#pragma target 2.0
			
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#pragma vertex vert
			#pragma fragment frag
	
	
		////// Uniform user variable definition

	
		////// Input structs
			struct VertexInput 
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			struct Vert2Frag
			{
				float4 pos : SV_POSITION;
				float4 posScreen : TEXCOORD0;
			};
			
	
		////// Shader functions
			// Vertex shader
			Vert2Frag vert(VertexInput vertIn)
			{
				Vert2Frag output;

			    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				output.pos = UnityObjectToClipPos(vertIn.vertex);
				output.posScreen = ComputeScreenPos(output.pos);
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{				
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(fragIn);

				// Value definitions
				float3 posScreen = fragIn.posScreen;
				return float4(posScreen,1);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
