using UnityEngine;
using System.Collections;

namespace nightowl.WaterShader
{
	[ExecuteInEditMode]
	public class MultiCameraWaterScript : BaseWaterScript
	{
		// Fields
		private static Hashtable renderedCameras = new Hashtable();
		private static Hashtable reflectionCameras = new Hashtable();
		private static Hashtable cameraPositions = new Hashtable();

		[Tooltip("Only render one reflection per camera to enable better performance. Can lead to wrong reflection in case water planes are on different levels or angles.")]
		public bool SingleReflectionPerCam = true;

		// Unity
		void OnWillRenderObject()
		{
			UpdateRender();
		}

		// WaterScript
		protected override Camera GetCamera()
		{
			var cam = Camera.current;
			if (renderedCameras.Contains(cam.GetHashCode()))
			{
				if (Time.time == (float) renderedCameras[cam.GetHashCode()])
				{
					if (SingleReflectionPerCam)
					{
						return null;
					}
				}
				else
				{
					renderedCameras[cam.GetHashCode()] = Time.time;
				}
			}
			else
			{
				renderedCameras.Add(cam.GetHashCode(), Time.time);
			}
			return cam;
		}

		protected override Camera GetReflectionCamera(Camera cam)
		{
			return reflectionCameras[cam] as Camera;
		}

		protected override Camera CreateReflectionCamera(Camera currentCamera)
		{
			Camera reflectCam = base.CreateReflectionCamera(currentCamera);
			reflectionCameras[currentCamera] = reflectCam;
			return reflectCam;
		}

		protected override bool HasMoved(Camera cam)
		{
			return true;
			Vector3 position = cam.transform.position + cam.transform.eulerAngles;
			bool hasMoved = !position.Equals(cameraPositions[cam]);
			if (hasMoved)
			{
				cameraPositions[cam] = position;
			}
			return hasMoved;
		}

		protected override void Clear()
		{
#if UNITY_EDITOR
			DestroyImmediate(combinedNormalMap);
#else
			Destroy(combinedNormalMap);
#endif

			foreach (DictionaryEntry pair in reflectionCameras)
			{
				ClearCamera(pair.Value as Camera);
			}
			reflectionCameras.Clear();
		}
	}
}