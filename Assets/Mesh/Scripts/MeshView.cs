using UnityEngine;

public class MeshView : MonoBehaviour {

	public MeshController meshCreator;
    public GameObject containerPanel;
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

    public void ActivateSineFunction() {
        meshCreator.function = FunctionOption.Sine;
    }

    public void ActivateComplexSineFunction() {
        meshCreator.function = FunctionOption.ComplexSine;
    }

    public void HideButton() {
        if(containerPanel.activeSelf)
            containerPanel.SetActive(false);
        else
            containerPanel.SetActive(true);
    }

    public void ActivateNormalsRecalculation() {
        meshCreator.recalculateNormals = !meshCreator.recalculateNormals;
        meshCreator.ResetNormals();
    }
}
