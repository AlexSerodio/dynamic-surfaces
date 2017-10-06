using UnityEngine;

public class TerrainExtension {

    public static void LoadTerrain(string fileName, TerrainData terrainData) {
        //the file location (folder/fileName.raw) 
        fileName = Application.dataPath + fileName;

        //get the terrain heightmap width and height.
        int x = terrainData.heightmapWidth;
        int y = terrainData.heightmapHeight;

        //create a float array with the terrain x, y size
        float[,] data = new float[x, y];

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

}
