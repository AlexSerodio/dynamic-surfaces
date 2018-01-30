using UnityEngine;

public static class MathFunctions {

	public static float Sine (float x) {
		//return Mathf.Sin(Mathf.PI * (x + Time.time);

        /*the expression bellow does the same as above but it keeps the range between 0 and 1 
        instead of -1 and 1. As the terrain height needs to be a value between 0 and 1 the sine value
        can't be negative.*/
        return (Mathf.Sin(Mathf.PI * (x + Time.time)) + 1) / 2f;
    }

	public static float Sine (float x, float z) {
        float t = Time.time;
		return 0.25f * Mathf.Sin(4f * Mathf.PI * x + 4f * t) * Mathf.Sin(2f * Mathf.PI * z + t) +
			0.10f * Mathf.Cos(3f * Mathf.PI * x + 5f * t) * Mathf.Cos(5f * Mathf.PI * z + 3f * t) +
			0.15f * Mathf.Sin(Mathf.PI * x + 0.6f * t);
	}

}
