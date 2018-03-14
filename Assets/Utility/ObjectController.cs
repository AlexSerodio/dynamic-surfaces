using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(lifeTime(3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator lifeTime (float time) {
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
