using UnityEngine;

public class MeshView : MonoBehaviour {

	public MeshController meshController;
    public GameObject containerPanel;
	private bool _meshMapButton;
	private Coroutine _meshMapCoroutine;

	public void LoadScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

	public void ActivateMeshMap() {
        _meshMapButton = !_meshMapButton;

        if(_meshMapButton) {
            _meshMapCoroutine = StartCoroutine(meshController.UpdateHeatMap());
        } else {
            StopCoroutine(_meshMapCoroutine);
            // meshCreator.ResetColor();
            meshController.ApplyMaterial(meshController.mainMaterial);
        }
    }

    public void ActivateSineFunction() {
        meshController.function = FunctionOption.Sine;
    }

    public void ActivateComplexSineFunction() {
        meshController.function = FunctionOption.ComplexSine;
    }

    public void HideButton() {
        if(containerPanel.activeSelf)
            containerPanel.SetActive(false);
        else
            containerPanel.SetActive(true);
    }

    public void ActivateNormalsRecalculation() {
        meshController.recalculateNormals = !meshController.recalculateNormals;
        meshController.ResetNormals();
    }
}
