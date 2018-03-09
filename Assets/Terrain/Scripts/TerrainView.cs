using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainView : MonoBehaviour {

	public TerrainController terrainController;
    public GameObject containerPanel;
    private bool _terrainMapButton;
    private bool _sineFunctionButton;
    private bool _sineFunction2Button;
    private Coroutine _terrainMapCoroutine;

	public void LoadScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

	 public void ActivateTerrainMap() {
        _terrainMapButton = !_terrainMapButton;

        if(_terrainMapButton) {
            _terrainMapCoroutine = StartCoroutine(terrainController.UpdateHeatMap());
        } else {
            StopCoroutine(_terrainMapCoroutine);
            terrainController.ResetColor();
        }
    }

    public void ActivateSineFunction() {
        _sineFunctionButton = !_sineFunctionButton;
        _sineFunction2Button = false;
        if (_sineFunctionButton) {
            terrainController.function = FunctionOption.Sine;
            terrainController.StartChanges();
        } else {
            terrainController.StopChanges();
        }            
    }

    public void ActivateComplexSineFunction() {
        _sineFunction2Button = !_sineFunction2Button;
        _sineFunctionButton = false;
        if (_sineFunction2Button) {
            terrainController.function = FunctionOption.ComplexSine;
            terrainController.StartChanges();
        } else {
            terrainController.StopChanges();
        }
    }

    public void HideButton() {
        if(containerPanel.activeSelf)
            containerPanel.SetActive(false);
        else
            containerPanel.SetActive(true);
    }
}
