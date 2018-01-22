using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour {

    public AudioMixer mixer;
    public GameObject pauseMenu;
    public Slider soundVolume;
    List<AudioSource> sounds = new List<AudioSource>();
    List<float> volumes = new List<float>();

	public void ChangeSoundVolume (float volume) {
        //mixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat("SoundVolume",volume);
	}
	
    void GetVolumes()
    {
        soundVolume.value = PlayerPrefs.GetFloat("SoundVolume", 0);
    }

    void Start()
    {
        GetVolumes();
        mixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("SoundVolume", 0)));
    }

	void Update () {
		if(Input.GetButtonDown("PauseMenu"))
        {
            if(pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                mixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("SoundVolume",0)));
            }
            else
            {
                pauseMenu.SetActive(true);
                mixer.SetFloat("SoundVolume", -80);
                Time.timeScale = 0f;
            }
        }
	}
}
