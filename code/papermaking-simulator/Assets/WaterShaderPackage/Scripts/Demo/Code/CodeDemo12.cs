using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo12 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_ShallowDepth"))
			{
				Material.SetFloat("_ShallowDepth", CodeDemoHelper.HelperTimeNormalized*5f);
			}
		}
	}
}