using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int depth = 10;
    public int width = 256;
    public int height = 256;
	public float scale = 14f;
	public float offsetX;
	public float offsetY;

	void Start(){
		offsetX = Random.Range(0f, 99999f);
		offsetY = Random.Range(0f, 99999f);
	}

    void Update() {
		Terrain terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);

		offsetX += Time.deltaTime * 5f;
    }

	private TerrainData GenerateTerrain(TerrainData terrainData) {
		terrainData.heightmapResolution = width + 1;
		terrainData.size = new Vector3(width, depth, height);
		terrainData.SetHeights(0, 0, GenerateHeights());

		return terrainData;
	}

	private float[,] GenerateHeights() {
		float [,] heights = new float[width,height];

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++)
				heights[x,y] = CalculateHeight(x, y);
		}
		return heights;
	}

	private float CalculateHeight(int x, int y) {
		float xCord = (float)x / width * scale/* + offsetX*/;
		float yCord = (float)y / height * scale/* + offsetY*/;

		return Mathf.PerlinNoise(xCord, yCord);
	}
}
