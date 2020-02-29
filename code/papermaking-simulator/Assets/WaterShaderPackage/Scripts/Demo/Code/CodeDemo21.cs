using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo21 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_FoamDistance"))
			{
				Material.SetFloat("_FoamDistance", CodeDemoHelper.HelperTimeNormalized + 0.001f);
			}
		}
	}
}