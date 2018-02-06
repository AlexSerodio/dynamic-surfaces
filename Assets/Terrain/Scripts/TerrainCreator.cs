using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainCreator : MonoBehaviour {

    public FunctionOption function;

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

    public IEnumerator ChangeHeight() {
        float step = 1/(float)_resolutionX;

        while (true) {
            for (int x = 0; x < _resolutionX; x++) {
                for (int z = 0; z < _resolutionZ; z++) {
                    if(function == 0)
                        _heights[x,z] = MathFunctions.Sine(x*step);
                    else
                        _heights[x,z] = 0.5f + MathFunctions.Sine(x*step, z*step);
                }
            }
            _terrain.terrainData.SetHeights(0, 0, _heights);
            yield return null;
        }
    }

    public IEnumerator UpdateHeatMap() {
        float[, ,] alphaMap = _terrain.terrainData.GetAlphamaps(0, 0, _terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight);
        while(true) {
            for (int x = 0; x < _resolutionX-1; x++) {
                for (int y = 0; y < _resolutionZ-1; y++) {
                    alphaMap[x, y, 0] = 1.0f - _heights[x, y];
                    alphaMap[x, y, 1] = _heights[x, y];
                    alphaMap[x, y, 2] = 0;
                }
            }
            _terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
            // GetComponent<Terrain>().materialType = Terrain.MaterialType.Custom; 
            // GetComponent<Terrain>().materialTemplate.color = Color.green;
            yield return null;
        }
    }

    public void ResetColor () {
        float[, ,] alphaMap = _terrain.terrainData.GetAlphamaps(0, 0, _terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight);
        
        for (int x = 0; x < _heights.GetLength(0)-1; x++) {
            for (int y = 0; y < _heights.GetLength(1)-1; y++) {
                alphaMap[x, y, 2] = 1;
                alphaMap[x, y, 1] = 0;
                alphaMap[x, y, 0] = 0;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
    }
}
