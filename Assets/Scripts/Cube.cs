﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	[Range(1, 100)]
	public float size;
	public Material mat;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();

		createCubeMesh ();
	}

	void Update() {
		gameObject.transform.localScale = new Vector3(size, size, size);
	}

	public void createCubeMesh() {
		Vector3[] vertices = new Vector3[8];

		vertices [0] = new Vector3 (-0.5f, +0.5f, -0.5f);
		vertices [1] = new Vector3 (+0.5f, +0.5f, -0.5f);
		vertices [2] = new Vector3 (+0.5f, -0.5f, -0.5f);
		vertices [3] = new Vector3 (-0.5f, -0.5f, -0.5f);
		vertices [4] = new Vector3 (+0.5f, +0.5f, +0.5f);
		vertices [5] = new Vector3 (-0.5f, +0.5f, +0.5f);
		vertices [6] = new Vector3 (-0.5f, -0.5f, +0.5f);
		vertices [7] = new Vector3 (+0.5f, -0.5f, +0.5f);

		int[] triangles = { 0, 1, 2,
			0, 2, 3,
			1, 4, 7,
			1, 7, 2,
			4, 5, 6,
			4, 6, 7,
			5, 0, 3,
			5, 3, 6,
			5, 4, 1,
			5, 1, 0,
			3, 2, 7,
			3, 7, 6 };

		Mesh msh = new Mesh ();

		msh.vertices = vertices;
		msh.triangles = triangles;
		msh.RecalculateNormals ();

		gameObject.transform.localScale = new Vector3(size, size, size);
		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer> ().material = mat;
	}
}
