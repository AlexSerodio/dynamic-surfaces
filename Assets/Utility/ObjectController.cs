using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	// private float positionX;
	// private float positionZ;
	// private Quaternion rotation;

	// Use this for initialization
	void Start () {
		// positionX = transform.position.x;
		// positionZ = transform.position.z;
		// rotation = transform.rotation;
		// StartCoroutine(lifeTime(5));
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		// transform.position = new Vector3(positionX, transform.position.y, positionZ);
		// transform.rotation = rotation;
	}

	private IEnumerator lifeTime (float time) {
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
