using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainView : MonoBehaviour {

	public TerrainCreator terrainCreator;
    private bool _terrainMapButton;
    private Coroutine _terrainMapCoroutine;

	public void LoadScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

	 public void ActivateTerrainMap() {
        _terrainMapButton = !_terrainMapButton;

        if(_terrainMapButton) {
            _terrainMapCoroutine = StartCoroutine(terrainCreator.UpdateHeatMap());
        } else {
            StopCoroutine(_terrainMapCoroutine);
            terrainCreator.ResetColor();
        }
    }
}
