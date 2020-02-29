using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo4 : MonoBehaviour
	{
		// Refs
		public Material Material;
		public Texture Texture;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_FoamTex"))
			{
				Material.SetTexture("_FoamTex", CodeDemoHelper.HelperTimeSin > 0 ? Texture : null);
			}
		}
	}
}