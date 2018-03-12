using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationController : MonoBehaviour {

	public GameObject prefab;
	public MeshController meshController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100.0f))
			{
				if (hit.collider.tag=="Surface") {
					Vector3 position = new Vector3(hit.point.x, 
						hit.point.y + prefab.transform.localScale.y/2, hit.point.z);
					Instantiate(prefab, position, Quaternion.identity);
				}
			}
		}
	}
}
