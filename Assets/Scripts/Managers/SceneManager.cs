using UnityEngine;

public class SceneManager : MonoBehaviour {
    public static SceneManager I { get; private set; }

    private void Awake() {
        if (I == null)
            I = this;
        else
            Destroy(gameObject);
    }

    public void LoadNextScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1
        );
    }
    
    public void ReloadCurrentScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame() {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}