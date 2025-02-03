using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private float transitionTime;
    bool pause = false;

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) OnPausePressed();
    }

    public void OnPausePressed(){

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

        for (float f = 0f; f < transitionTime; f += Time.unscaledDeltaTime) {
            float value = f / transitionTime;

            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1f, value);
            yield return null;
        }
    }

    private IEnumerator Play() {

        pause = false;
        var canvasGroup = pauseOverlay.GetComponent<CanvasGroup>();
        Time.timeScale = 1f;

        for (float f = 0f; f < transitionTime; f += Time.unscaledDeltaTime) {
            float value = f / transitionTime;

            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, value);
            yield return null;
        }


        pauseOverlay.SetActive(false);
    }
}