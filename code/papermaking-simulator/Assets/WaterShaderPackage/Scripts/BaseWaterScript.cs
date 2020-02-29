using UnityEngine;

namespace nightowl.WaterShader
{
	[ExecuteInEditMode]
	public abstract class BaseWaterScript : MonoBehaviour
	{
		// Enum
		public enum UpdateMode
		{
			OnStart = 0,
			Continous
		}

		// References
		[Tooltip("")]
		public Texture2D NormalMap1;
		[Tooltip("")]
		public Texture2D NormalMap2;

		// Fields
		[Tooltip("")]
		public Vector2 NormalMapTiling = Vector2.one;
		[Tooltip("")]
		public Vector2 NormalMapOffset = Vector2.zero;
		[Tooltip("")]
		public Vector2 NormalMap1Movement = Vector2.zero;
		[Tooltip("")]
		public Vector2 NormalMap2Movement = Vector2.zero;

		[Tooltip("")]
		public UpdateMode Setup = UpdateMode.OnStart;
		[Tooltip("")]
		public int ReflectionTextureSize = 1024;
		[Tooltip("")]
		public float ClipPlaneOffset = 0.07f;
		[Tooltip("")]
		public LayerMask ReflectLayers = -1;
		[Tooltip("")]
		public bool ReflectSky = true;
		[Tooltip("")]
		public bool TrackCamMovement = true;

		protected bool insideRendering = false;
		protected bool firstRender = true;
		protected Texture2D combinedNormalMap = null;

		// Mono
		void Start()
		{
			firstRender = true;
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				UpdateNormalMapData();
			}
#endif
		}
		 
		void OnEnable()
		{
			firstRender = true;
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				UpdateNormalMapData();
			}
#endif
		}

		void OnDisable()
		{
			Clear();
		}

		// WaterShader
		protected virtual void UpdateRender()
		{
			Material[] materials = GetComponent<Renderer>().sharedMaterials;
			UpdateNormalMapData();
			UpdateUVs(materials);

			Camera cam = GetCamera();
			if (!cam)
				return;

			//cam.depth = 24.0f;
			cam.depthTextureMode = DepthTextureMode.Depth;

			if (!enabled || !GetComponent<Renderer>() || !GetComponent<Renderer>().sharedMaterial ||
				!GetComponent<Renderer>().enabled)
				return;

			if (insideRendering)
				return;
			insideRendering = true;

			Camera reflectionCamera = CreateSurfaceObjects(cam);

			int oldPixelLightCount = QualitySettings.pixelLightCount;
			QualitySettings.pixelLightCount = 0;

			UpdateCameraSettings(cam, reflectionCamera);
			if (!TrackCamMovement || HasMoved(cam))
			{
				UpdatePositions(cam, reflectionCamera);
			}

			GL.invertCulling = true;
			reflectionCamera.Render();
			GL.invertCulling = false;
			
			foreach (Material mat in materials)
			{
				mat.SetTexture("_ReflectionTex", reflectionCamera.targetTexture);
			}

			Matrix4x4 scaleOffset = Matrix4x4.TRS(
				new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
			Vector3 scale = transform.lossyScale;
			Matrix4x4 mtx = transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1.0f / scale.x, 1.0f / scale.y, 1.0f / scale.z));
			mtx = scaleOffset * cam.projectionMatrix * cam.worldToCameraMatrix * mtx;
			foreach (Material mat in materials)
			{
				mat.SetMatrix("_ProjMatrix", mtx);
			}

			QualitySettings.pixelLightCount = oldPixelLightCount;

			insideRendering = false;
			if (firstRender)
			{
				firstRender = false;
			}
		}

		public void ForceUpdateNormalMapData()
		{
			//Debug.Log("ForceUpdateNormalMapData");
			firstRender = true;
#if UNITY_EDITOR
			DestroyImmediate(combinedNormalMap);
#else
				Destroy(combinedNormalMap);
#endif
			combinedNormalMap = null;
			UpdateNormalMapData();
		}

		protected void UpdateNormalMapData()
		{
			Material[] materials = GetComponent<Renderer>().sharedMaterials;
			foreach (Material mat in materials)
			{
				mat.SetVector("_NormalTex_ST", new Vector4(NormalMapTiling.x, NormalMapTiling.y, NormalMapOffset.x, NormalMapOffset.y));
			}

			if (!firstRender)
				return;

			SetTexture(materials);
		}

		protected virtual void SetTexture(Material[] materials)
		{
			combinedNormalMap = GetCombinedNormalTexture();
			foreach (Material mat in materials)
			{
				mat.SetTexture("_NormalTex", combinedNormalMap);
			}
		}

		protected virtual Texture2D GetCombinedNormalTexture()
		{
			//Debug.Log("GetCombinedNormalTexture: ");
			if (combinedNormalMap != null)
			{
				return combinedNormalMap;
			}

			if (NormalMap1 == null && NormalMap2 == null)
			{
				//Debug.Log("GetCombinedNormalTexture default1: ");
				return CreateDefaultNormalMap();
			}

			if (NormalMap1 != null && NormalMap2 != null)
			{
				if (NormalMap1.width != NormalMap2.width ||
				    NormalMap1.height != NormalMap2.height)
				{
					//Debug.Log("GetCombinedNormalTexture single1: ");
					return CreateSingleNormalMap(NormalMap1);
				}
				//Debug.Log("GetCombinedNormalTexture combined1: ");
				return CreateCombinedNormalMap(NormalMap1, NormalMap2);
			}
			//Debug.Log("GetCombinedNormalTexture single2: ");

			return CreateSingleNormalMap(NormalMap1 ?? NormalMap2);
		}

		protected Texture2D CreateDefaultNormalMap()
		{
			//Debug.Log("CreateDefaultNormalMap()");
			var texture = new Texture2D(1, 1);
			texture.SetPixels(new []{Color.black});
			texture.Apply();
			return texture;
		}

		private Texture2D CreateCombinedNormalMap(Texture2D NormalMap1, Texture2D NormalMap2)
		{
			//Debug.Log("CreateCombinedNormalMap()");
			var baseTexture = NormalMap1;
			var texture = new Texture2D(baseTexture.width, baseTexture.height);
			Color32[] map1 = NormalMap1 != null ? NormalMap1.GetPixels32(0) : baseTexture.GetPixels32(0);
			Color32[] map2 = NormalMap2 != null ? NormalMap2.GetPixels32(0) : baseTexture.GetPixels32(0);

			for (int a = 0; a < map1.Length; ++a)
			{
				map1[a].b = map2[a].r;
				map1[a].a = map2[a].g;
			}

			texture.SetPixels32(map1);
			texture.Apply();

			return texture;
		}

		private Texture2D CreateSingleNormalMap(Texture2D template)
		{
			//Debug.Log("CreateSingleNormalMap()");
			var texture = new Texture2D(template.width, template.height);
			Color32[] map = template.GetPixels32(0);

			for (int a = 0; a < map.Length; ++a)
			{
				map[a].b = map[a].r;
				map[a].a = map[a].g;
			}

			texture.SetPixels32(map);
			texture.Apply();

			//Debug.Log("CreateSingleNormalMap(): "+texture);
			return texture;
		}

		protected virtual bool HasMoved(Camera cam)
		{
			return true;
		}

		protected virtual void UpdatePositions(Camera cam, Camera reflectCam)
		{
			Vector3 pos = transform.position;
			Vector3 normal = transform.up;

			float d = -Vector3.Dot(normal, pos) - ClipPlaneOffset;
			Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

			Matrix4x4 reflection = Matrix4x4.zero;
			CalculateReflectionMatrix(ref reflection, reflectionPlane);
			Vector3 oldpos = cam.transform.position;
			Vector3 newpos = reflection.MultiplyPoint(oldpos);
			reflectCam.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

			Vector4 clipPlane = CameraSpacePlane(reflectCam, pos, normal, 1.0f);
			Matrix4x4 projection = cam.projectionMatrix;
			CalculateObliqueMatrix(ref projection, clipPlane);
			reflectCam.projectionMatrix = projection;

			reflectCam.transform.position = newpos;
			Vector3 euler = cam.transform.eulerAngles;
			reflectCam.transform.eulerAngles = new Vector3(0, euler.y, euler.z);
		}

		protected virtual void UpdateUVs(Material[] materials)
		{
			float time = Time.time;
			time = (Mathf.Cos(time) * 0.2f + time + 120);
			Vector4 offset = new Vector4(NormalMap1Movement.x, NormalMap1Movement.y, NormalMap2Movement.x, NormalMap2Movement.y) * time;
			foreach (Material mat in materials)
			{
				mat.SetVector("_UVOffset", offset);
			}
		}

		protected virtual Camera GetCamera()
		{
			return Camera.main;
		}

		protected virtual void UpdateCameraSettings(Camera srcCamera, Camera destinationCamera)
		{
			if (destinationCamera == null ||
				srcCamera == null ||
				!firstRender && Setup == UpdateMode.OnStart)
				return;

			destinationCamera.clearFlags = ReflectSky ? srcCamera.clearFlags : CameraClearFlags.Color;
			destinationCamera.backgroundColor = srcCamera.backgroundColor;

			if (srcCamera.clearFlags == CameraClearFlags.Skybox && ReflectSky)
			{
				Skybox sourceSky = srcCamera.GetComponent(typeof(Skybox)) as Skybox;
				Skybox destinationSky = destinationCamera.GetComponent(typeof(Skybox)) as Skybox;
				if (!sourceSky || !sourceSky.material)
				{
					destinationSky.enabled = false;
				}
				else
				{
					destinationSky.enabled = true;
					destinationSky.material = sourceSky.material;
				}
			}

			destinationCamera.farClipPlane = srcCamera.farClipPlane;
			destinationCamera.nearClipPlane = srcCamera.nearClipPlane;
			destinationCamera.orthographic = srcCamera.orthographic;
			destinationCamera.fieldOfView = srcCamera.fieldOfView;
			destinationCamera.aspect = srcCamera.aspect;
			destinationCamera.orthographicSize = srcCamera.orthographicSize;

			// Never render water layer
			destinationCamera.cullingMask = ~(1 << 4) & ReflectLayers.value;
		}

		private Camera CreateSurfaceObjects(Camera currentCamera)
		{
			var reflectionCamera = CreateReflectionCamera(currentCamera);
			reflectionCamera.targetTexture = CreateReflectionTexture(reflectionCamera);
			return reflectionCamera;
		}

		protected virtual RenderTexture CreateReflectionTexture(Camera reflectCam)
		{
			RenderTexture reflectionTexture = reflectCam.targetTexture;
			if (reflectionTexture == null || reflectionTexture.width != ReflectionTextureSize)
			{
				if (reflectionTexture)
				{
					reflectCam.targetTexture = null;
					DestroyImmediate(reflectionTexture);
				}

				// Debug.Log("CreateReflectionTexture()");
				reflectionTexture = new RenderTexture(ReflectionTextureSize, ReflectionTextureSize, 24);
				reflectionTexture.name = "__ReflectionTex" + reflectCam.GetInstanceID();
				reflectionTexture.isPowerOfTwo = true;
				reflectionTexture.hideFlags = HideFlags.HideAndDontSave;
				reflectCam.targetTexture = reflectionTexture;
			}
			return reflectionTexture;
		}

		protected virtual Camera CreateReflectionCamera(Camera currentCamera)
		{
			Camera reflectCam = GetReflectionCamera(currentCamera);
			if (!reflectCam)
			{
				// Debug.Log("CreateReflectionCamera()");
				string name = "ReflectionCam " + GetInstanceID() + " for " + currentCamera.GetInstanceID();
				GameObject go = new GameObject(name, typeof(Camera), typeof(Skybox));
				reflectCam = go.GetComponent<Camera>();
				reflectCam.enabled = false;
				reflectCam.transform.position = transform.position;
				reflectCam.transform.rotation = transform.rotation;
				go.hideFlags = HideFlags.HideAndDontSave;
			}
			return reflectCam;
		}

		protected virtual float sgn(float a)
		{
			if (a > 0.0f) return 1.0f;
			if (a < 0.0f) return -1.0f;
			return 0.0f;
		}

		protected virtual Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
		{
			Vector3 offsetPos = pos + normal * ClipPlaneOffset;
			Matrix4x4 m = cam.worldToCameraMatrix;
			Vector3 cpos = m.MultiplyPoint(offsetPos);
			Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
			return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
		}

		protected virtual void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
		{
			Vector4 q = projection.inverse * new Vector4(
				sgn(clipPlane.x),
				sgn(clipPlane.y),
				1.0f,
				1.0f
				);
			Vector4 c = clipPlane * (2.0F / (Vector4.Dot(clipPlane, q)));
			projection[2] = c.x - projection[3];
			projection[6] = c.y - projection[7];
			projection[10] = c.z - projection[11];
			projection[14] = c.w - projection[15];
		}

		protected virtual void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
		{
			reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
			reflectionMat.m01 = (-2F * plane[0] * plane[1]);
			reflectionMat.m02 = (-2F * plane[0] * plane[2]);
			reflectionMat.m03 = (-2F * plane[3] * plane[0]);

			reflectionMat.m10 = (-2F * plane[1] * plane[0]);
			reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
			reflectionMat.m12 = (-2F * plane[1] * plane[2]);
			reflectionMat.m13 = (-2F * plane[3] * plane[1]);

			reflectionMat.m20 = (-2F * plane[2] * plane[0]);
			reflectionMat.m21 = (-2F * plane[2] * plane[1]);
			reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
			reflectionMat.m23 = (-2F * plane[3] * plane[2]);

			reflectionMat.m30 = 0F;
			reflectionMat.m31 = 0F;
			reflectionMat.m32 = 0F;
			reflectionMat.m33 = 1F;
		}

		protected virtual Camera GetReflectionCamera(Camera _)
		{
			return null;
		}

		protected virtual void Clear()
		{
#if UNITY_EDITOR
			DestroyImmediate(combinedNormalMap);
#else
			Destroy(combinedNormalMap);
#endif
			ClearCamera(GetCamera());
		}

		protected virtual void ClearCamera(Camera cam)
		{
			Camera reflectCam = GetReflectionCamera(cam);
			if (reflectCam != null)
			{
				var targetTexture = reflectCam.targetTexture;
				if (targetTexture != null)
				{
					reflectCam.targetTexture = null;
#if UNITY_EDITOR
					DestroyImmediate(targetTexture);
#else
					Destroy(targetTexture);
#endif
				}
				DestroyImmediate(reflectCam.gameObject);
			}
		}
	}
}