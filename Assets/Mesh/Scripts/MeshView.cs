using UnityEngine;

public class MeshView : MonoBehaviour {

	public MeshController meshCreator;
	private bool _meshMapButton;
	private Coroutine _meshMapCoroutine;

	public void LoadScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

	public void ActivateMeshMap() {
        _meshMapButton = !_meshMapButton;

        if(_meshMapButton) {
            _meshMapCoroutine = StartCoroutine(meshCreator.UpdateHeatMap());
        } else {
            StopCoroutine(_meshMapCoroutine);
            meshCreator.ResetColor();
        }
    }

}
