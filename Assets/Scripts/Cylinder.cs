using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

	public int meridiansNb;
	public float radius;
	public float height;
	public Material mat;

	int lastMeridiansNb;

	void Start () {
		lastMeridiansNb = meridiansNb;
		gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();

		createCylinderMesh ();
	}

	void Update() {
		if(meridiansNb != lastMeridiansNb)
			createCylinderMesh ();
		lastMeridiansNb = meridiansNb;
		gameObject.transform.localScale = new Vector3(radius, height, radius);
	}

	public void createCylinderMesh() {
		if (meridiansNb >= 3) {
			Vector3[] vertices = new Vector3[meridiansNb * 2 + 2];
			for (int m = 0; m < meridiansNb; m++) {
				float theta = ((2 * Mathf.PI * m) / meridiansNb);
				vertices [m] = new Vector3 (Mathf.Cos (theta), 0.5f, Mathf.Sin (theta));
				vertices [m + meridiansNb] = new Vector3 (Mathf.Cos (theta), -0.5f, Mathf.Sin (theta));
			}
			vertices [meridiansNb * 2] = new Vector3 (0.0f, 0.5f, 0.0f);
			vertices [meridiansNb * 2 + 1] = new Vector3 (0.0f, -0.5f, 0.0f);

			int[] triangles = new int[meridiansNb * 12];
			for (int i = 0; i < meridiansNb; i++) {
				int A = i;
				int B = i + meridiansNb;
				int C, D;
				if (i != meridiansNb - 1) {
					C = i + 1;
					D = i + meridiansNb + 1;
				} else {
					C = 0;
					D = meridiansNb;
				}
				triangles [i * 6] = B;
				triangles [i * 6 + 1] = A;
				triangles [i * 6 + 2] = C;

				triangles [i * 6 + 3] = B;
				triangles [i * 6 + 4] = C;
				triangles [i * 6 + 5] = D;
			}
			for (int i = 0; i < meridiansNb * 2; i++) {
				int A, B, C;
				if (i < meridiansNb) {
					A = i;
					C = meridiansNb*2;
					if (i != meridiansNb - 1)
						B = i + 1;
					else
						B = 0;
				} else {
					B = i;
					C = meridiansNb * 2 + 1;
					if (i != meridiansNb*2 - 1)
						A = i + 1;
					else
						A = meridiansNb;
				}
				triangles [meridiansNb * 6 + i * 3] = B;
				triangles [meridiansNb * 6 + i * 3 + 1] = A;
				triangles [meridiansNb * 6 + i * 3 + 2] = C;
			}

			Mesh msh = new Mesh ();

			msh.vertices = vertices;
			msh.triangles = triangles;
			msh.RecalculateNormals ();

			gameObject.transform.localScale = new Vector3(radius, height, radius);
			gameObject.GetComponent<MeshFilter> ().mesh = msh;
			gameObject.GetComponent<MeshRenderer> ().material = mat;
		} else {
			Debug.Log ("il faut au moins 3 méridiens");
		}
	}
}
