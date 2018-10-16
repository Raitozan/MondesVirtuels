using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
	
	[Range(3, 100)]
	public int meridiansNb;
	public float radius;
	public Material mat;

	int lastMeridiansNb;

	// Use this for initialization
	void Start () {
		lastMeridiansNb = meridiansNb;
		gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();

		createSphereMesh ();
	}
	
	// Update is called once per frame
	void Update () {
		if(meridiansNb != lastMeridiansNb)
			createSphereMesh ();
		lastMeridiansNb = meridiansNb;
		gameObject.transform.localScale = new Vector3(radius, radius, radius);
	}

	public void createSphereMesh () {
		
	}
}
