using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceCreator : MonoBehaviour {

	private int resolution = 255;
	private int width = 10;
	private int length = 10;
	private int height = 3;

	private Mesh _mesh;
	private Vector3[] _vertices;
	private Vector3[] _normals;
	private Color[] _colors;
	
	void Start () {
		if (_mesh == null) {
			_mesh = new Mesh();
			_mesh.name = "Surface Mesh";
			GetComponent<MeshFilter>().mesh = _mesh;
		}
		CreateGrid();
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		ChangeHeight();
	}

	private void CreateGrid () {
		_mesh.Clear();
		_vertices = new Vector3[(resolution + 1) * (resolution + 1)];
		_colors = new Color[_vertices.Length];
		_normals = new Vector3[_vertices.Length];
		Vector2[] uv = new Vector2[_vertices.Length];
		float stepSize = 1f / resolution;
		for (int v = 0, z = 0; z <= resolution; z++) {
			for (int x = 0; x <= resolution; x++, v++) {
				_vertices[v] = new Vector3(x * stepSize - 0.5f, 0f, z * stepSize - 0.5f);
				_colors[v] = Color.black;
				_normals[v] = Vector3.up;
				uv[v] = new Vector2(x * stepSize, z * stepSize);
			}
		}
		_mesh.vertices = _vertices;
		_mesh.colors = _colors;
		_mesh.normals = _normals;
		_mesh.uv = uv;

		int[] triangles = new int[resolution * resolution * 6];
		for (int t = 0, v = 0, y = 0; y < resolution; y++, v++) {
			for (int x = 0; x < resolution; x++, v++, t += 6) {
				triangles[t] = v;
				triangles[t + 1] = v + resolution + 1;
				triangles[t + 2] = v + 1;
				triangles[t + 3] = v + 1;
				triangles[t + 4] = v + resolution + 1;
				triangles[t + 5] = v + resolution + 2;
			}
		}
		_mesh.triangles = triangles;

		transform.localScale = new Vector3(width, height, length);
		transform.localPosition = new Vector3(0, height/2f, 0);
	}

	public void ChangeHeight() {
        float step = 1/(float)resolution;
        for (int v = 0, y = 0; y <= resolution; y++) {
			for (int x = 0; x <= resolution; x++, v++) {
                _vertices[v].y = Sine(x * step);
			}
        }
		_mesh.vertices = _vertices;
		_mesh.RecalculateNormals();
    }

	private static float Sine (float x) {
        return Mathf.Sin(Mathf.PI * (x + Time.time));
    }

}
