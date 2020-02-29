using UnityEngine;

public class LookAt : MonoBehaviour 
{
	// Refs
	public Transform target;

	// Mono
	void Update ()
	{
		if (target == null)
			return;

		transform.LookAt(target);
	}
}
