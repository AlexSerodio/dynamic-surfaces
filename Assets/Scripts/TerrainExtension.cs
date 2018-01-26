using System.Collections;
using UnityEngine;

/// <summary>
/// A Terrain extension used to manipulate the terrain data.
/// </summary>
public class TerrainExtension {

    public static bool active = false;

    //private static float[,] heights;
    private static int _resolutionX;
    private static int _resolutionZ;

    private TerrainExtension() { }
    
    private static float[,] GetTerrainResolution(Terrain terrain) {
        //get the terrain heightmap width and height.
        //it will be used to determinate the terrain/array size
        _resolutionX = terrain.terrainData.heightmapWidth;
        _resolutionZ = terrain.terrainData.heightmapHeight;

        //GetHeights - gets the heightmap points of the terrain. 
        //Store those values in a float array.
        return terrain.terrainData.GetHeights(0, 0, _resolutionX, _resolutionZ);
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
        float[,] heights = GetTerrainResolution(terrain);
        for (int x = 0; x < _resolutionX; x++) {
            for (int z = 0; z < _resolutionZ; z++)
                heights[x, z] = .5f;
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    public static void RandomHeight(Terrain terrain, float height) {
        float[,] heights = GetTerrainResolution(terrain);
        //manipulate the height data.
        for (int x = 0; x < _resolutionX; x++) {
            for (int z = 0; z < _resolutionZ; z++)
                heights[x, z] = Random.Range(0, height);
        }
        //SetHeights - change the terrain heights.
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    public static IEnumerator ChangeHeight(Terrain terrain) {
        float step = 1/(float)_resolutionX;
        float[,] heights = GetTerrainResolution(terrain);
        while (active) {
            for (int x = 0; x < _resolutionX; x++) {
                for (int z = 0; z < _resolutionZ; z++)
                    heights[x,z] = Sine(x*step);
            }
            terrain.terrainData.SetHeights(0, 0, heights);
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
}
