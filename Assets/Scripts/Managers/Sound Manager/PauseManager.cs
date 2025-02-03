using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private float transitionTime;
    private bool pause;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) OnPausePressed();
    }

    public void OnPausePressed() {
        StopAllCoroutines();
        if (pause)
            StartCoroutine(Play());
        else
            StartCoroutine(Pause());
    }

    private IEnumerator Pause() {
        pause = true;

        Time.timeScale = 0f;
        pauseOverlay.SetActive(true);

        var canvasGroup = pauseOverlay.GetComponent<CanvasGroup>();

        for (var f = 0f; f < transitionTime; f += Time.unscaledDeltaTime) {
            var value = f / transitionTime;

            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1f, value);
            yield return null;
        }
    }

    private IEnumerator Play() {
        pause = false;
        var canvasGroup = pauseOverlay.GetComponent<CanvasGroup>();
        Time.timeScale = 1f;

        for (var f = 0f; f < transitionTime; f += Time.unscaledDeltaTime) {
            var value = f / transitionTime;

            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, value);
            yield return null;
        }


        pauseOverlay.SetActive(false);
    }
}