using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Calculates the FPS of the Scene.
/// </summary>
/// Needs to be attached to a UI Text component.
public class FPS : MonoBehaviour {

	public Text text;
    private float mDeltaTime = 0.0f;
    private float mFps;
     
    void Update() {
        mDeltaTime += (Time.deltaTime - mDeltaTime) * 0.1f;
    }
    
    void OnGUI() {
        mFps = 1.0f / mDeltaTime;
        text.text = "" + (int) mFps;
    }
}