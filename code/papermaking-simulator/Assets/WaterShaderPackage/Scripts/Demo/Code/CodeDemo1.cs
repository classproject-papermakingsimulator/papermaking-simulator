using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo1 : MonoBehaviour
	{
		// Refs
		public Material Material;
		public Texture Texture;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_MainTex"))
			{
				Material.SetTexture("_MainTex", CodeDemoHelper.HelperTimeSin > 0 ? Texture : null);
			}
		}
	}
}