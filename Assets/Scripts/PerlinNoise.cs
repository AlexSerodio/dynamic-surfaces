using UnityEngine;

public class PerlinNoite : MonoBehaviour {

	public int width = 256;
	public int height = 256;
	public float scale = 20f;
	
	private float offsetX;
	private float offsetY;

	void Start() {

		//sets random values for the texture position
		offsetX = Random.Range(0f, 9999999f);
		offsetY = Random.Range(0f, 9999999f);

		//in order to change the texture in the default material of the object
		//we have to access the mesh renderer component
		//then access the material 
		//e finally change the texture

		//stores the MeshRenderer component reference
		Renderer renderer = GetComponent<Renderer>();

		//change the texture
		renderer.material.mainTexture = GenerateTexture();
	}
	
	private Texture2D GenerateTexture() {

		Texture2D texture = new Texture2D(width, height);

		//GENERATE A PERLIN NOISE MAP FOR THE TEXTURE
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				Color color = CalculateColor(x, y);
				texture.SetPixel(x, y, color);
			}
		}
		texture.Apply();
		return texture;
	}

	private Color CalculateColor(int x, int y) {
		float xCordinate = (float)x / width * scale + offsetX;
		float yCordinate = (float)y / height * scale + offsetY;

		float sample = Mathf.PerlinNoise(xCordinate, yCordinate);
		return new Color(sample, sample, sample);
	}
}
