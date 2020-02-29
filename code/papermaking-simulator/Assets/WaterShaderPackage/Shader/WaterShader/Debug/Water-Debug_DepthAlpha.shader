// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/DepthAlpha" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_EdgeFade ("Edge fade", float) = 0.5
		_ShallowDepth ("ShallowDepth", float) = 0.5
		_ShallowDeepFade ("Shallow-Deep-Fade", float) = 0.5
		_ShoreEdgeIndicator("Water shore edge indicator", float) = 0.2
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
	
	
		////// Uniform user variable definition
			uniform float _EdgeFade;				// Fading from shore to shallow water
			
			uniform float _ShallowDepth;
			uniform float _ShallowDeepFade;
			uniform float _ShoreEdgeIndicator;
			
			// Unity variable definition
			uniform sampler2D _LastCameraDepthTexture;

	
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
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{
				// Depth fog
				float2 depthValues = GetDepthValues(fragIn.posScreen, _LastCameraDepthTexture, _ShallowDeepFade, _ShallowDepth, _EdgeFade);
				float depthLevel = depthValues.x;
				float waterLevel = depthValues.y;
				
				// Final composition
				float4 final = float4( waterLevel, waterLevel, waterLevel, depthLevel);
				return(final);  
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
