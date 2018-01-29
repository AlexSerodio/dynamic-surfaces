using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenHandle : MonoBehaviour {

    public TerrainCreator terrainCreator;
    private float[,] _heights;
    private bool _terrainMapButton;
    private Coroutine _terrainMapCoroutine;

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void QuitGame() {
        Application.Quit();
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
