using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo16 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_Refraction"))
			{
				Material.SetFloat("_Refraction", CodeDemoHelper.HelperTimeNormalized*10f);
			}
		}
	}
}