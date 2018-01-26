using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreator : MonoBehaviour {

	private Terrain _terrain;
	private static int _resolutionX;
    private static int _resolutionZ;
	private static float[,] _heights;

	void Start() {
		_terrain = GetComponent<Terrain>();
		_resolutionX = _terrain.terrainData.heightmapWidth;
        _resolutionZ = _terrain.terrainData.heightmapHeight;
		_heights = _terrain.terrainData.GetHeights(0, 0, _resolutionX, _resolutionZ);

		ResetHeight();
	}

	void Update() {
		ChangeHeight();
	}

	private void ResetHeight() {
        for (int x = 0; x < _resolutionX; x++) {
            for (int z = 0; z < _resolutionZ; z++)
                _heights[x, z] = .5f;
        }
        _terrain.terrainData.SetHeights(0, 0, _heights);
    }

	private void ChangeHeight() {
        float step = 1/(float)_resolutionX;
        for (int x = 0; x < _resolutionX; x++) {
            for (int z = 0; z < _resolutionZ; z++)
                _heights[x,z] = Sine(x*step);
        }
        _terrain.terrainData.SetHeights(0, 0, _heights);
    }

    private static float Sine (float x) {
        //return Mathf.Sin(Mathf.PI * (x + Time.time);

        /*the expression bellow do the same as above but it keeps the range between 0 and 1 
        instead of -1 and 1. As the terrain height needs to be a value between 0 and 1 the sine value
        can't be negative.*/
        return (Mathf.Sin(Mathf.PI * (x + Time.time)) + 1) / 2f;
    }
}
