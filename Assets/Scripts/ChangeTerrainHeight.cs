using UnityEngine;

public class ChangeTerrainHeight : MonoBehaviour {

    public Terrain terrain;
    public float height;

    private float[,] heights;
    private int xRes;
    private int yRes;
    private int xBase;
    private int yBase;

    void Start() {
        GetTerrainConfigurations();
    }

    public void LoadHeightMap() {
        TerrainExtension.LoadTerrain("/Heightmaps/heightmap.raw", terrain.terrainData);
    }

    public void ChangeHeightButton() {
        //manipulate the height data.
        for (int i = 0; i < xRes; i++)
            for (int j = 0; j < yRes; j++)
                heights[i, j] = Random.Range(0, height);
                
        //SetHeights - change the terrain heights.
        terrain.terrainData.SetHeights(xBase, yBase, heights);
    }

    public void ResetHeightButton() {
        for (int i = 0; i < xRes; i++)
            for (int j = 0; j < yRes; j++)
                heights[i, j] = 0;

        terrain.terrainData.SetHeights(xBase, yBase, heights);
    }

    private void GetTerrainConfigurations() {
        //get the terrain heightmap width and height.
        //it will be used to determinate the terrain/array size
        xRes = terrain.terrainData.heightmapWidth;
        yRes = terrain.terrainData.heightmapHeight;

        //Debug.Log("X resolution: " + xRes);
        //Debug.Log("Y resolution: " + yRes);

        //in what position the heightmap array/terrain starts.
        xBase = 0;
        yBase = 0;

        //GetHeights - gets the heightmap points of the terrain. 
        //Store those values in a float array.
        heights = terrain.terrainData.GetHeights(xBase, yBase, xRes, yRes);
    }

}
