using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {
    public static SceneManager I { get; private set; }
    
    private GameObject[] m_levelButtons;
    private int m_levelsUnlocked;

    private void Awake() {
        if (I == null) {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        m_levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 0);
    }

    private void Update() {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R)) PlayerPrefs.DeleteAll();
        #endif
        m_levelButtons = new[] {
            GameObject.FindGameObjectWithTag("1"),
            GameObject.FindGameObjectWithTag("2"),
            GameObject.FindGameObjectWithTag("3"),
            GameObject.FindGameObjectWithTag("4"),
            GameObject.FindGameObjectWithTag("5")
        };
        
        if (m_levelButtons.Length == 0) return;

        for (int i = 0; i < m_levelButtons.Length; i++) {
            m_levelButtons[i].GetComponent<Image>().enabled = m_levelsUnlocked >= i;
        }
    }

    public void OnLevelCompleted() {
        m_levelsUnlocked = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LevelsUnlocked", m_levelsUnlocked);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadCurrentLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_levelsUnlocked + 1);
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
}