using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
	[Range(3, 100)]
	public int meridiansNb;
	public float radius;
	public float height;
	public Material mat;

	int lastMeridiansNb;

	List<Vector3> vertices;
	List<int> triangles;

	void Start()
	{
		lastMeridiansNb = meridiansNb;
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		createConeMesh();
	}

	void Update()
	{
		if (meridiansNb != lastMeridiansNb)
			createConeMesh();
		lastMeridiansNb = meridiansNb;
		gameObject.transform.localScale = new Vector3(radius, height, radius);
	}

	public void createConeMesh()
	{
		vertices = new List<Vector3>();
		triangles = new List<int>();

		for (int m = 0; m < meridiansNb; m++)
		{
			float theta = ((2 * Mathf.PI * m) / meridiansNb);
			vertices.Add(new Vector3(Mathf.Cos(theta), -0.5f, Mathf.Sin(theta)));
		}
		vertices.Add(new Vector3(0.0f, 0.5f, 0.0f));
		vertices.Add(new Vector3(0.0f, -0.5f, 0.0f));

		for (int i = 0; i < meridiansNb; i++)
		{
			int A = i;
			int B, Bp, C;

			if (i < meridiansNb - 1)
			{
				B = vertices.Count - 2;
				Bp = i + 1;
				C = vertices.Count - 1;
			}
			else
			{
				B = vertices.Count - 2;
				Bp = i - (meridiansNb - 1);
				C = vertices.Count - 1;
			}

			triangles.Add(A);
			triangles.Add(B);
			triangles.Add(Bp);
			
			triangles.Add(A);
			triangles.Add(Bp);
			triangles.Add(C);
		}

		Mesh msh = new Mesh();

		msh.vertices = vertices.ToArray();
		msh.triangles = triangles.ToArray();
		msh.RecalculateNormals();

		gameObject.transform.localScale = new Vector3(radius, height, radius);
		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer>().material = mat;
	}
}