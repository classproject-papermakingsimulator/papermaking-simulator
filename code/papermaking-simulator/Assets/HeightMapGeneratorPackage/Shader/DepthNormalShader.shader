// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
 *	Created by Martin Reintges
 */ 

Shader "HeightMapPack/DepthNormalShader" 
{
	Properties 
	{
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		Pass
		{
			CGPROGRAM
			#pragma target 2.0

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			uniform sampler2D _CameraDepthNormalsTexture;

			
		////// Input structs
			struct VertexInput 
			{
				half4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
			};
			struct Vert2Frag
			{
				half4 pos : SV_POSITION;
				half4 posScreen : TEXCOORD1;
				half2 uv : TEXCOORD0;
			};


			Vert2Frag vert( VertexInput vertIn )
			{
				Vert2Frag output;
				output.pos = UnityObjectToClipPos (vertIn.vertex);
				output.posScreen = ComputeScreenPos(output.pos);
				output.uv =  vertIn.texcoord.xy;
				return output;
			}    
 
			half4 frag (Vert2Frag fragIn) : COLOR
			{
				float3 normalValues;
				float depthValue;

				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, fragIn.posScreen.xy), depthValue, normalValues);

				return float4(normalValues.xyz*0.5+float3(0.5,0.5,0.5),depthValue);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
