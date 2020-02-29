using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo9 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_EdgeFade"))
			{
				Material.SetFloat("_EdgeFade", CodeDemoHelper.HelperTimeNormalized*10f + 0.001f);
			}
		}
	}
}