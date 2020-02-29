// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Water/Debug/HeightMap" 
{
	/* 
	 * Created by Martin Reintges
	 */

	Properties 
	{
		_EnvironmentTex ("Height map",2D) = "black" {}
		_ShallowColor ("Shallow color tint", Color) = (0,0,1,1)
		_DeepSeaColor ("Deep sea color tint", Color) = (0,0,1,1)
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
			uniform sampler2D _EnvironmentTex;		// The texture containing environment information RGB as flowmap, A as heightmap
			uniform float4 _EnvironmentTex_ST;		// The env texture scale

			uniform float4 _ShallowColor;			// The color factor for shallow water
			uniform float4 _DeepSeaColor;			// The color factor for deep sea

	
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
				float2 uv_Environment : TEXCOORD4;
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
				output.uv_Environment = vertIn.uv_NormalTex * _EnvironmentTex_ST.xy + _EnvironmentTex_ST.zw;
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{
				// Height effects
				float groundLevel = tex2D(_EnvironmentTex, fragIn.uv_Environment).x;
				float3 waterDepthTint = (groundLevel*_ShallowColor) + (1-groundLevel)*_DeepSeaColor;
				
				return( float4(waterDepthTint,1) );  
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
