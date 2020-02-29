using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo19 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_FoamScale"))
			{
				Material.SetFloat("_FoamScale", CodeDemoHelper.HelperTimeNormalized*10f + 1f);
			}
		}
	}
}