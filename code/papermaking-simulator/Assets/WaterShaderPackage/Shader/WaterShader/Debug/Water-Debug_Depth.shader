// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/Depth" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_EdgeFade ("Edge fade", float) = 0.5
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
				float4 posWorld : TEXCOORD0;
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
				
				output.posWorld = mul(unity_ObjectToWorld, vertIn.vertex);
				output.posScreen = ComputeScreenPos(output.pos);
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{
				// Depth fog
				float depth = SAMPLE_DEPTH_TEXTURE_PROJ(_LastCameraDepthTexture, UNITY_PROJ_COORD(fragIn.posScreen));
				depth = LinearEyeDepth(depth);
				depth = _EdgeFade * (depth - fragIn.posScreen.w);

				// Final composition
				float4 final = float4( depth, depth, depth, 1);

				return(final);  
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
