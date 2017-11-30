using System.Collections;
using UnityEngine;

/// <summary>
/// A Terrain extension used to manipulate the terrain data.
/// </summary>
public class TerrainExtension {

    public static bool active = false;

    private static float[,] heights;
    private static int xRes;
    private static int yRes;

    private TerrainExtension() { }
    
    private static void GetTerrainConfigurations(Terrain terrain) {
        //get the terrain heightmap width and height.
        //it will be used to determinate the terrain/array size
        xRes = terrain.terrainData.heightmapWidth;
        yRes = terrain.terrainData.heightmapHeight;

        //GetHeights - gets the heightmap points of the terrain. 
        //Store those values in a float array.
        heights = terrain.terrainData.GetHeights(0, 0, xRes, yRes);
    }

    public static void LoadTerrain(string fileName, TerrainData terrainData) {
        //the file location (folder/fileName.raw) 
        fileName = Application.dataPath + fileName;

        //get the terrain heightmap width and height.
        int x = terrainData.heightmapWidth;
        int y = terrainData.heightmapHeight;

        //create a float array with the terrain x, y size
        float[,] data = new float[x, y];

        //read the raw file and convert to a byte array
        using (var file = System.IO.File.OpenRead(fileName))
        using (var reader = new System.IO.BinaryReader(file)) { 
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    float v = (float)reader.ReadUInt16() / 0xFFFF;
                    data[i, j] = v;
                }
            }
        }
        terrainData.SetHeights(0, 0, data);
    }

    public static void ResetHeight(Terrain terrain) {
        GetTerrainConfigurations(terrain);
        for (int y = 0; y < yRes; y++)
            for (int x= 0; x < xRes; x++)
                heights[y, x] = 0;

        terrain.terrainData.SetHeights(0, 0, heights);
    }

    public static void RandomHeight(Terrain terrain, float height) {
        GetTerrainConfigurations(terrain);
        //manipulate the height data.
        for (int y = 0; y < yRes; y++)
            for (int x = 0; x < xRes; x++)
                heights[y, x] = Random.Range(0, height);

        //SetHeights - change the terrain heights.
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    public static IEnumerator ChangeHeight(Terrain terrain) {
        while (active) {
            GetTerrainConfigurations(terrain);
            for (int y = 0; y < yRes; y++) {
                for (int x = 0; x < xRes; x++) {
                    if (heights[y, x] > 0) {
                        heights[y, x] += Random.Range(-.001f, .001f);
                        //TODO - suavização das posições vizinhas (rascunho no caderno)
                    }
                }
            }
            terrain.terrainData.SetHeights(0, 0, heights);
            yield return null;
        }
    }
}
