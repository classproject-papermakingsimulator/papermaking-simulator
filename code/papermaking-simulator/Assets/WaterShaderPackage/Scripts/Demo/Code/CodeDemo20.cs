using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo20 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_FoamRefraction"))
			{
				Material.SetFloat("_FoamRefraction", CodeDemoHelper.HelperTimeNormalized*0.5f);
			}
		}
	}
}