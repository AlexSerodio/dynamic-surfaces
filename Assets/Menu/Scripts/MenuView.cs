using UnityEngine;

public class MenuView : MonoBehaviour {

    public void LoadScene(int index) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
