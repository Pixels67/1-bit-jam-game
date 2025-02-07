using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager I { get; private set; }


    [Header("Level Selection")]
    public LevelSelectButton[] levels;
    public List<LevelSelectButton> unlockedLevels;

    private void Awake()
    {
        if (I == null)
            I = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //StartLevelSelect();
    }

    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1
        );
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #region Level Selection
    public void StartLevelSelect()
    {
        LoadPlayerPrefs();
    }
    public void OnLevelComplete(Scene scene) // call this with context of the current scene
    {
        List<string> list = new();
        foreach (LevelSelectButton s in levels) list.Add(s.SceneID);

        if (list.IndexOf(scene.name) == list.Count - 1)
            return; //maybe could add some "you completed the game" scene
        else if (list.IndexOf(scene.name) == -1)
        {
            Debug.LogError("next level not found, check SceneManager inspector for errors");
            return;
        }

        var newLevel = levels[list.IndexOf(scene.name) + 1];
        unlockedLevels.Add(newLevel);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameof(newLevel)); //  currently takes you to the new level, could also take you back to main menu
        SavePlayerPrefs();
    }
    public void LockButton(LevelSelectButton subject)
    {
        subject.button.GetComponent<Button>().enabled = false;
        subject.button.GetComponent<Image>().enabled = false;
    }
    public void UnlockButton(LevelSelectButton subject)
    {
        subject.button.GetComponent<Button>().enabled = true;
        subject.button.GetComponent<Image>().enabled = true;
    }
    public void OnLevelSelected(Scene scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameof(scene));
    }
    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("unlockCount", unlockedLevels.Count);
    }
    private void LoadPlayerPrefs()
    {
        foreach (LevelSelectButton lsb in levels)
        {
            LockButton(lsb);
        }

        if (!PlayerPrefs.HasKey("unlockCount")) PlayerPrefs.SetInt("unlockCount", 1);

        unlockedLevels = new List<LevelSelectButton>();
        for (int i = 0; i < PlayerPrefs.GetInt("unlockCount"); i++)
        {
            unlockedLevels.Add(levels[i]);
        }
        foreach (LevelSelectButton lsb in unlockedLevels)
        {
            UnlockButton(lsb);
        }
    }
    #endregion
}

[System.Serializable]
public class LevelSelectButton
{
    public string SceneID;
    public GameObject button;
}