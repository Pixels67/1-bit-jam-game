using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;
    [SerializeField] private GameObject SFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Vector3 pos, float volume)
    {
        GameObject audioObject = Instantiate(SFXObject, pos, Quaternion.identity);
        audioObject.transform.parent = transform;

        AudioSource audioSource = audioObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioObject, clipLength);
    }

    #region README
    // to use this, just call "SoundEffectsManager.instance.PlaySFXClip([SOUND], [POSITION TO PLAY SOUND], [RELATIVE VOLUME])" from whatever object you want to add sound to.
    // the "SFXObject" gameobject prefab handles the audio mixing, so only change [RELATIVE VOLUME] when the sound is loud compared to other SFX, otherwise leave it at 1f.

    #endregion
}
