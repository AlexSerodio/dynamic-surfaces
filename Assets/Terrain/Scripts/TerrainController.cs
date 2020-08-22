using System.Collections;
using TerrainUtilsDLL;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    public FunctionOption function;

	private Terrain _terrain;
	private static int _resolutionX;
    private static int _resolutionZ;
	private static float[,] _heights;
    private TerrainHeight _utils;
    private Coroutine _changeHeight;
    private bool _changed = false;

	void Start () {        
		_terrain = GetComponent<Terrain>();
		_resolutionX = _terrain.terrainData.heightmapResolution;
        _resolutionZ = _terrain.terrainData.heightmapResolution;
		_heights = _terrain.terrainData.GetHeights(0, 0, _resolutionX, _resolutionZ);

        _utils = new TerrainHeight(_resolutionX, _resolutionZ);

        ResetHeight();
        ResetColor();
	}

	void Update () {
        if (_changed)
            _terrain.terrainData.SetHeights(0, 0, _utils.heights);
    }

    public void StartChanges () {
        _changed = true;
        if (_changeHeight != null)
            StopCoroutine(_changeHeight);
        _changeHeight = StartCoroutine(_utils.ChangeHeight((int)function));
    }

    public void StopChanges () {
        _changed = false;
        if (_changeHeight != null)
            StopCoroutine(_changeHeight);
    }

	private void ResetHeight () {
        for (int x = 0; x < _resolutionX; x++) {
            for (int z = 0; z < _resolutionZ; z++)
                _heights[x, z] = .5f;
        }
        _terrain.terrainData.SetHeights(0, 0, _heights);
    }
    
    public IEnumerator UpdateHeatMap () {
        HideDirt();
        float[, ,] alphaMap = _terrain.terrainData.GetAlphamaps(0, 0, _terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight);
        while(true) {
            for (int x = 0; x < _resolutionX-1; x++) {
                for (int y = 0; y < _resolutionZ-1; y++) {
                    alphaMap[x, y, 0] = 1.0f - _utils.heights[x, y];
                    alphaMap[x, y, 1] = _utils.heights[x, y];
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
                alphaMap[x, y, 0] = 0;
                alphaMap[x, y, 1] = 0;
                alphaMap[x, y, 2] = 1;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
    }

    private void HideDirt () {
        float[, ,] alphaMap = _terrain.terrainData.GetAlphamaps(0, 0, _terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight);
        
        for (int x = 0; x < _heights.GetLength(0)-1; x++) {
            for (int y = 0; y < _heights.GetLength(1)-1; y++) {
                alphaMap[x, y, 2] = 0;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
    }
}
