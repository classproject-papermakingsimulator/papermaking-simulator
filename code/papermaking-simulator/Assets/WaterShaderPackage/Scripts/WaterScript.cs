using UnityEngine;


namespace nightowl.WaterShader
{
	[ExecuteInEditMode]
	public class WaterScript : BaseWaterScript
	{
		// References
		public Camera MainCamera;

		// Fields
		private Camera reflectionCamera = null;
		private Vector3 lastPosition = Vector3.zero;

		// Mono
		void Update()
		{
		}

		void OnWillRenderObject()
		{
			UpdateRender();
		}

		// WaterShader
		protected override Camera GetCamera()
		{
			return MainCamera;
		}

		protected override Camera CreateReflectionCamera(Camera currentCamera)
		{
			reflectionCamera = base.CreateReflectionCamera(currentCamera);
			return reflectionCamera;
		}

		protected override bool HasMoved(Camera cam)
		{
			Vector3 position = cam.transform.position;
			bool hasMoved = !position.Equals(lastPosition);
			if (hasMoved)
			{
				lastPosition = position;
			}
			return hasMoved;
		}

		protected override Camera GetReflectionCamera(Camera _)
		{
			return reflectionCamera;
		}
	}
}