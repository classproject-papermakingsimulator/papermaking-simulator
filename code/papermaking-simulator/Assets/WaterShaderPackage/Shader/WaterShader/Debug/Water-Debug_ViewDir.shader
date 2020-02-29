// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/ViewDir" 
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
			};
			struct Vert2Frag
			{
				float4 pos : SV_POSITION;
				float3 viewDir : TEXCOORD2;
			};
			
	
		////// Shader functions
			// Vertex shader
			Vert2Frag vert(VertexInput vertIn)
			{
				Vert2Frag output;
				
				output.pos = UnityObjectToClipPos(vertIn.vertex);
				output.viewDir = (WorldSpaceViewDir(vertIn.vertex));
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{				
				// Value definitions
				float3 viewDir = normalize(fragIn.viewDir);
				return float4(viewDir,1);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
