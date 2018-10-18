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
		{
			Debug.Log("oui");
			createSphereMesh();
		}
		lastMeridiansNb = meridiansNb;
		gameObject.transform.localScale = new Vector3(radius, radius, radius);
	}

	public void createSphereMesh () {
		Vector3[] vertices = new Vector3[(meridiansNb * (meridiansNb - 2)) + 2];
		int t = 0;
		for(int i=0; i<meridiansNb-2; i++)
		{
			float phi = ((Mathf.PI * (i+1)) / meridiansNb+1);
			float nr = Mathf.Cos(phi);
			for (int m = 0; m < meridiansNb; m++)
			{
				float theta = ((2 * Mathf.PI * m) / meridiansNb);
				vertices[t++] = new Vector3(nr * Mathf.Cos(theta), Mathf.Sin(phi), nr * Mathf.Sin(theta));
			}
		}
		vertices[t++] = new Vector3(0, 1, 0);
		vertices[t++] = new Vector3(0, -1, 0);

		int[] triangles = new int[((meridiansNb-2)*(meridiansNb*2))*3];
		t = 0;
		for(int i = 0; i<((meridiansNb*(meridiansNb-3))-1); i++)
		{
			if ((i + 1) % meridiansNb != 0)
			{
				triangles[t++] = i;
				triangles[t++] = i + 1;
				triangles[t++] = i + meridiansNb;

				triangles[t++] = i + meridiansNb;
				triangles[t++] = i + 1;
				triangles[t++] = i + meridiansNb + 1;
			}
			else
			{
				triangles[t++] = i;
				triangles[t++] = i - (meridiansNb - 1);
				triangles[t++] = i + meridiansNb;

				triangles[t++] = i + meridiansNb;
				triangles[t++] = i - (meridiansNb - 1);
				triangles[t++] = i + 1;
			}
		}
		int A, B, C;
		for (int i = 0; i < meridiansNb; i++)
		{
			A = i;
			if ((i + 1) % meridiansNb != 0)
			{
				B = (meridiansNb * (meridiansNb - 2));
				C = i + 1;
			}
			else
			{
				B = (meridiansNb * (meridiansNb - 2));
				C = i - (meridiansNb - 1);
			}
			triangles[t++] = A;
			triangles[t++] = B;
			triangles[t++] = C;
		}
		for(int i=(meridiansNb*(meridiansNb-3)); i<(meridiansNb*(meridiansNb-2)); i++)
		{
			A = i;
			if ((i + 1) % meridiansNb != 0)
			{
				B = i + 1;
				C = (meridiansNb * (meridiansNb - 2)) + 1;
			}
			else
			{
				B = i - (meridiansNb - 1);
				C = (meridiansNb * (meridiansNb - 2)) + 1;
			}
			triangles[t++] = A;
			triangles[t++] = B;
			triangles[t++] = C;
		}

		Mesh msh = new Mesh();

		msh.vertices = vertices;
		msh.triangles = triangles;
		msh.RecalculateNormals();

		gameObject.transform.localScale = new Vector3(radius, radius, radius);
		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer>().material = mat;
	}
}
