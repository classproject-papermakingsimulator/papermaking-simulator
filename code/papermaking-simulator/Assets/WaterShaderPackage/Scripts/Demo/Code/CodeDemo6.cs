using UnityEngine;

namespace nightowl.WaterShader
{
	public class CodeDemo6 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Material.HasProperty("_Shininess"))
			{
				Material.SetFloat("_Shininess", CodeDemoHelper.HelperTimeNormalized*200f);
			}
		}
	}
}