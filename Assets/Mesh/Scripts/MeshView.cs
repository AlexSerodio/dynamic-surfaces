using UnityEngine;

public class MeshView : MonoBehaviour {

	public MeshController meshController;
    public GameObject containerPanel;
	private bool _meshMapButton;
    private bool _sineFunctionButton;
    private bool _complexSineFunctionButton;
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
            meshController.SetMaterial(meshController.dirt);
        }
    }

    public void ActivateSineFunction() {
        _sineFunctionButton = !_sineFunctionButton;
        _complexSineFunctionButton = false;
        if (_sineFunctionButton) {
            meshController.function = FunctionOption.Sine;
            meshController.StartChanges();
        } else {
            meshController.StopChanges();
        }    
    }

    public void ActivateComplexSineFunction() {
        _complexSineFunctionButton = !_complexSineFunctionButton;
        _sineFunctionButton = false;
        if (_complexSineFunctionButton) {
            meshController.function = FunctionOption.ComplexSine;
            meshController.StartChanges();
        } else {
            meshController.StopChanges();
        }
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
