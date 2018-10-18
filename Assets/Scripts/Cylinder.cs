using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

	[Range(3, 100)]
	public int meridiansNb;
	public float radius;
	public float height;
	public Material mat;

	int lastMeridiansNb;

	void Start ()
	{
		lastMeridiansNb = meridiansNb;
		gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();

		createCylinderMesh ();
	}

	void Update()
	{
		if(meridiansNb != lastMeridiansNb)
			createCylinderMesh ();
		lastMeridiansNb = meridiansNb;
		gameObject.transform.localScale = new Vector3(radius, height, radius);
	}

	public void createCylinderMesh()
	{
		Vector3[] vertices = new Vector3[meridiansNb * 2 + 2];
		for (int m = 0; m < meridiansNb; m++) {
			float theta = ((2 * Mathf.PI * m) / meridiansNb);
			vertices [m] = new Vector3 (Mathf.Cos (theta), 0.5f, Mathf.Sin (theta));
			vertices [m + meridiansNb] = new Vector3 (Mathf.Cos (theta), -0.5f, Mathf.Sin (theta));
		}
		vertices [meridiansNb * 2] = new Vector3 (0.0f, 0.5f, 0.0f);
		vertices [meridiansNb * 2 + 1] = new Vector3 (0.0f, -0.5f, 0.0f);

		int[] triangles = new int[meridiansNb * 12];
		int t=0;
		for (int i = 0; i < meridiansNb; i++)
		{
			//faces
			if (i < meridiansNb - 1)
			{
				triangles [t++] = i;
				triangles [t++] = i + 1;
				triangles [t++] = i + meridiansNb;

				triangles [t++] = i + meridiansNb;
				triangles [t++] = i + 1;
				triangles [t++] = i + meridiansNb + 1;
			}
			else
			{
				triangles [t++] = i;
				triangles [t++] = i - (meridiansNb - 1);
				triangles [t++] = i + meridiansNb;

				triangles [t++] = i + meridiansNb;
				triangles [t++] = i - (meridiansNb - 1);
				triangles [t++] = i + 1;
			}

		}
		for (int i = 0; i < meridiansNb * 2; i++)
		{
			//couvercles
			int A=i;
			int B, C;

			if ((i+1) % meridiansNb != 0)
			{
				if (i < meridiansNb)
				{
					B = meridiansNb * 2;
					C = i + 1;
				}
				else
				{
					B = i + 1;
					C = meridiansNb * 2 + 1;
				}
			}
			else
			{
				if (i < meridiansNb)
				{
					B = meridiansNb * 2;
					C = i - (meridiansNb - 1);
				}
				else
				{
					B = i - (meridiansNb - 1);
					C = meridiansNb * 2 + 1;
				}
			}
			triangles[t++] = A;
			triangles[t++] = B;
			triangles[t++] = C;
		}

		Mesh msh = new Mesh ();

		msh.vertices = vertices;
		msh.triangles = triangles;
		msh.RecalculateNormals ();

		gameObject.transform.localScale = new Vector3(radius, height, radius);
		gameObject.GetComponent<MeshFilter> ().mesh = msh;
		gameObject.GetComponent<MeshRenderer> ().material = mat;
	}
}
