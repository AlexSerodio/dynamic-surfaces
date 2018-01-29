using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        ResetColor();

        StartCoroutine(ChangeHeight());
	}

	void Update() {
        // ChangeHeight();
        // if (scene.name == "Terrain 2") 
            // UpdateHeatMap(); 
	}

	private void ResetHeight() {
        for (int x = 0; x < _resolutionX; x++) {
            for (int z = 0; z < _resolutionZ; z++)
                _heights[x, z] = .5f;
        }
        _terrain.terrainData.SetHeights(0, 0, _heights);
    }

    private IEnumerator ChangeHeight() {
        float step = 1/(float)_resolutionX;

        while (true) {
            for (int x = 0; x < _resolutionX; x++) {
                for (int z = 0; z < _resolutionZ; z++)
                    _heights[x,z] = Sine(x*step);
            }
            _terrain.terrainData.SetHeights(0, 0, _heights);
            yield return null;
        }
    }

    private static float Sine (float x) {
        //return Mathf.Sin(Mathf.PI * (x + Time.time);

        /*the expression bellow do the same as above but it keeps the range between 0 and 1 
        instead of -1 and 1. As the terrain height needs to be a value between 0 and 1 the sine value
        can't be negative.*/
        return (Mathf.Sin(Mathf.PI * (x + Time.time)) + 1) / 2f;
    }

    public IEnumerator UpdateHeatMap() {
        float[, ,] alphaMap = _terrain.terrainData.GetAlphamaps(0, 0, _terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight);
        while(true) {
            for (int x = 0; x < _resolutionX-1; x++) {
                for (int y = 0; y < _resolutionZ-1; y++) {
                    alphaMap[x, y, 1] = 1.0f - _heights[x, y];
                    alphaMap[x, y, 2] = _heights[x, y];
                }
            }
            _terrain.terrainData.SetAlphamaps(0, 0, alphaMap); 
            yield return null;
        }
    }

    public void ResetColor () {
        float[, ,] alphaMap = _terrain.terrainData.GetAlphamaps(0, 0, _terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight);
        
        for (int x = 0; x < _heights.GetLength(0)-1; x++) {
            for (int y = 0; y < _heights.GetLength(1)-1; y++) {
                alphaMap[x, y, 0] = 1;
                alphaMap[x, y, 1] = 0;
                alphaMap[x, y, 2] = 0;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
    }
}
