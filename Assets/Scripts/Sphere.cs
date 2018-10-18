using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
	
	[Range(3, 100)]
	public int meridiansNb;
	[Range(1, 100)]
	public int parallelsNb;
	public float radius;
	public Material mat;

	int lastMeridiansNb;
	int lastParallelsNb;

	List<Vector3> vertices;
	List<int> triangles;

	// Use this for initialization
	void Start () {
		lastMeridiansNb = meridiansNb;
		lastParallelsNb = parallelsNb;
		gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();

		createSphereMesh ();
	}

	// Update is called once per frame
	void Update () {
		if(meridiansNb != lastMeridiansNb || parallelsNb != lastParallelsNb)
			createSphereMesh();
		lastMeridiansNb = meridiansNb;
		lastParallelsNb = parallelsNb;
		gameObject.transform.localScale = new Vector3(radius, radius, radius);
	}

	public void createSphereMesh () {
		vertices = new List<Vector3>();
		triangles = new List<int>();

		for (int p=0; p<parallelsNb; p++)
		{
			float phi = ((Mathf.PI * (p+1))/(parallelsNb+1));
			for (int m = 0; m < meridiansNb; m++)
			{
				float theta = ((2 * Mathf.PI * m) / meridiansNb);
				vertices.Add(new Vector3(Mathf.Sin(phi)*Mathf.Cos(theta), Mathf.Cos(phi), Mathf.Sin(phi)*Mathf.Sin(theta)));
			}
		}
		vertices.Add(new Vector3(0, 1, 0));
		vertices.Add(new Vector3(0, -1, 0));

		for(int i = 0; i<(meridiansNb*(parallelsNb-1)); i++)
		{
			if ((i + 1) % meridiansNb != 0)
			{
				triangles.Add(i);
				triangles.Add(i + 1);
				triangles.Add(i + meridiansNb);

				triangles.Add(i + meridiansNb);
				triangles.Add(i + 1);
				triangles.Add(i + meridiansNb + 1);
			}
			else
			{
				triangles.Add(i);
				triangles.Add(i - (meridiansNb - 1));
				triangles.Add(i + meridiansNb);

				triangles.Add(i + meridiansNb);
				triangles.Add(i - (meridiansNb - 1));
				triangles.Add(i + 1);
			}
		}
		int A, B, C;
		for (int i = 0; i < meridiansNb; i++)
		{
			A = i;
			if ((i + 1) % meridiansNb != 0)
			{
				B = (meridiansNb * parallelsNb);
				C = i + 1;
			}
			else
			{
				B = (meridiansNb * parallelsNb);
				C = i - (meridiansNb - 1);
			}
			triangles.Add(A);
			triangles.Add(B);
			triangles.Add(C);
		}
		for(int i=(meridiansNb*(parallelsNb-1)); i<(meridiansNb*parallelsNb); i++)
		{
			A = i;
			if ((i + 1) % meridiansNb != 0)
			{
				B = i + 1;
				C = (meridiansNb * parallelsNb) + 1;
			}
			else
			{
				B = i - (meridiansNb - 1);
				C = (meridiansNb * parallelsNb) + 1;
			}
			triangles.Add(A);
			triangles.Add(B);
			triangles.Add(C);
		}

		Mesh msh = new Mesh();

		msh.vertices = vertices.ToArray();
		msh.triangles = triangles.ToArray();
		msh.RecalculateNormals();

		gameObject.transform.localScale = new Vector3(radius, radius, radius);
		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer>().material = mat;
	}
}
