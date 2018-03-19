using UnityEngine;

public class InstantiationController : MonoBehaviour {

	public GameObject[] prefabs;
	
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100.0f)) {
				if (hit.collider.tag=="Surface") {
					int rand = Random.Range(0, prefabs.Length);
					Vector3 position = new Vector3(hit.point.x, 
						hit.point.y + prefabs[rand].transform.localScale.y, hit.point.z);
					Instantiate(prefabs[rand], position, Quaternion.identity);
				}
			}
		}
	}
}
