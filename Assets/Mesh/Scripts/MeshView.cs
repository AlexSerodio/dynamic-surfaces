using UnityEngine;

public class MeshView : MonoBehaviour {

	public MeshController meshController;
    public GameObject containerPanel;
	private bool _meshMapButton;
    private bool _sineFunctionButton;
    private bool _sineFunction2Button;
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
        _sineFunction2Button = false;
        if (_sineFunctionButton) {
            meshController.function = FunctionOption.Sine;
            meshController.StartChanges();
        } else {
            meshController.StopChanges();
        }    
    }

    public void ActivateComplexSineFunction() {
        _sineFunction2Button = !_sineFunction2Button;
        _sineFunctionButton = false;
        if (_sineFunction2Button) {
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
