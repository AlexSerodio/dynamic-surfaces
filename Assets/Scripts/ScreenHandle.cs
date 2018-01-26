using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenHandle : MonoBehaviour {

    [SerializeField] private Terrain terrain;
    private float[,] _heights;

    public void LoadHeightMapButton() {
        TerrainExtension.LoadTerrain("/Heightmaps/heightmap.raw", terrain.terrainData);
    }

    public void RandomHeightButton() {
        TerrainExtension.RandomHeight(terrain, .01f);
    }

    public void ResetHeightButton() {
        TerrainExtension.ResetHeight(terrain);
    }

    public void ChangeHeightButton() {
        TerrainExtension.active = !TerrainExtension.active;
        StartCoroutine(TerrainExtension.ChangeHeight(terrain));
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void QuitGame() {
        Application.Quit();
    }

}
