using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AppControls : MonoBehaviour 
{
	// Fields
	public string sceneName = "";
	public bool cameraControl = false;
	public Transform camera;

	private Vector3 lastPosition = Vector3.zero;

	// Unity
	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	void Update()
	{
		if (!cameraControl)
			return;

		if (Input.GetMouseButtonDown(0) &&
 			EventSystem.current &&
			EventSystem.current.currentSelectedGameObject == null)
		{
			lastPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			lastPosition = Vector3.zero;
		}

		if (lastPosition != Vector3.zero)
		{
			RotateCamera();
		}
	}

	// Ui
	public void LoadNextScene()
	{
		SceneManager.LoadScene(sceneName);
	}

	// AppControls
	private void RotateCamera()
	{
		var diff = Input.mousePosition - lastPosition;
		float angle = diff.x/Screen.width;
		camera.RotateAround(transform.up, -angle);
		angle = diff.y/Screen.height;
		camera.RotateAround(camera.right, angle);

		lastPosition = Input.mousePosition;
	}

}
