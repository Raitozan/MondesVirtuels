using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OffToMesh : MonoBehaviour {

	public Object file;
	public Material mat;

	// Use this for initialization
	void Start() {
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		string[] eleTemp = System.IO.File.ReadAllText(@"C:\Users\ndesages\Desktop\Mondes Virtuels\MondesVirtuels\Assets\" + file.name + ".off").Split(new char[] { ' ', '\n' });
		List<string> elements = new List<string>(eleTemp);
		for(int i=0; i< elements.Count; i++)
			if (elements[i] == "")
				elements.Remove(elements[i]);

		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		int e = 1;
		int verticesNb = int.Parse(elements[e++]);
		int trianglesNb = int.Parse(elements[e++]);
		e++;

		int start = e;
		while(e < start + (verticesNb * 3))
		{
			vertices.Add(new Vector3(float.Parse(elements[e++]), float.Parse(elements[e++]), float.Parse(elements[e++])));
		}

		start = e;
		while (e < start + (trianglesNb * 4))
		{
			e++;
			triangles.Add(int.Parse(elements[e++]));
			triangles.Add(int.Parse(elements[e++]));
			triangles.Add(int.Parse(elements[e++]));
		}

		Mesh msh = new Mesh();

		msh.vertices = vertices.ToArray();
		msh.triangles = triangles.ToArray();
		msh.RecalculateNormals();

		gameObject.GetComponent<MeshFilter>().mesh = msh;
		gameObject.GetComponent<MeshRenderer>().material = mat;

		AssetDatabase.CreateAsset(msh, @"Assets\Meshes\" + file.name);
		Destroy(gameObject.GetComponent<OffToMesh>());
		PrefabUtility.CreatePrefab("Assets/Prefabs/" + file.name + ".prefab", gameObject);
		Destroy(gameObject);
	}
}
