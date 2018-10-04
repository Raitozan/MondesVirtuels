using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	
	public int cote;
	public Material mat;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();

		Vector3[] vertices = new Vector3[8];
		int[] triangles = new int[36];

		vertices [0] = new Vector3 (transform.position.x - cote / 2, transform.position.y + cote / 2, transform.position.z - cote / 2);
		vertices [1] = new Vector3 (transform.position.x + cote / 2, transform.position.y + cote / 2, transform.position.z - cote / 2);
		vertices [2] = new Vector3 (transform.position.x + cote / 2, transform.position.y - cote / 2, transform.position.z - cote / 2);
		vertices [3] = new Vector3 (transform.position.x - cote / 2, transform.position.y - cote / 2, transform.position.z - cote / 2);
		vertices [4] = new Vector3 (transform.position.x + cote / 2, transform.position.y + cote / 2, transform.position.z + cote / 2);
		vertices [5] = new Vector3 (transform.position.x - cote / 2, transform.position.y + cote / 2, transform.position.z + cote / 2);
		vertices [6] = new Vector3 (transform.position.x - cote / 2, transform.position.y - cote / 2, transform.position.z + cote / 2);
		vertices [7] = new Vector3 (transform.position.x + cote / 2, transform.position.y - cote / 2, transform.position.z + cote / 2);

		triangles [0] = 0;
		triangles [1] = 1;
		triangles [2] = 2;

		triangles [3] = 0;
		triangles [4] = 2;
		triangles [5] = 3;

		triangles [6] = 1;
		triangles [7] = 4;
		triangles [8] = 7;

		triangles [9] = 1;
		triangles [10] = 7;
		triangles [11] = 2;

		triangles [12] = 4;
		triangles [13] = 5;
		triangles [14] = 6;

		triangles [15] = 4;
		triangles [16] = 6;
		triangles [17] = 7;

		triangles [18] = 5;
		triangles [19] = 0;
		triangles [20] = 3;

		triangles [21] = 5;
		triangles [22] = 3;
		triangles [23] = 6;

		triangles [24] = 5;
		triangles [25] = 4;
		triangles [26] = 1;

		triangles [27] = 5;
		triangles [28] = 1;
		triangles [29] = 0;

		triangles [30] = 3;
		triangles [31] = 2;
		triangles [32] = 7;

		triangles [33] = 3;
		triangles [34] = 7;
		triangles [35] = 6;

		Mesh msh = new Mesh ();

		msh.vertices = vertices;
		msh.triangles = triangles;

		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer> ().material = mat;
	}
}
