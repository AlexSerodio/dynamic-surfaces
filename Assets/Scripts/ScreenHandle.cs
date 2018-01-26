using UnityEngine;

public class ScreenHandle : MonoBehaviour {

    [SerializeField] private Terrain terrain;
    private float[,] _heights;


    void Start()
    {
            
    }

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

}
