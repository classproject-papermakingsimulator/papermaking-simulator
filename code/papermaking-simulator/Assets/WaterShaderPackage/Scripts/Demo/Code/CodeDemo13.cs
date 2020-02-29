using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo13 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_ShallowDeepFade"))
			{
				Material.SetFloat("_ShallowDeepFade", CodeDemoHelper.HelperTimeNormalized*4f + 0.01f);
			}
		}
	}
}