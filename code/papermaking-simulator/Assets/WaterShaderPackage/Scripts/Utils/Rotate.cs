using UnityEngine;

namespace nightowl.WaterShaderPack
{
	public class Rotate : MonoBehaviour
	{
		// Props
		public float Speed = 0.1f;
		public Vector3 Angle = Vector3.up;

		// Mono
		void Update()
		{
			transform.Rotate(Angle, Speed);
		}
	}
}