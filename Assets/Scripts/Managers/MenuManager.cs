using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private AudioClip clickSFX;
    
    public void OnStartButtonClicked() {
        SoundEffectsManager.instance.PlaySFXClip(clickSFX, transform.position);
        SceneManager.I.LoadCurrentLevel();
    }
    
    public void OnLevelButtonClicked(string level) {
        SoundEffectsManager.instance.PlaySFXClip(clickSFX, transform.position);
        SceneManager.I.LoadScene(level);
    }
}
