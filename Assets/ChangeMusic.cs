using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    public List<AudioSource> allAudioSources;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Button soundButton;

    private bool isSoundOn = true;
    private const string soundPrefsKey = "SoundOn";

    void Start()
    {
        isSoundOn = PlayerPrefs.GetInt(soundPrefsKey, 1) == 1;
        SetVolume(isSoundOn ? 0.25f : 0f);
        UpdateButtonSprite();
        soundButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        SetVolume(isSoundOn ? 0.25f : 0f);
        PlayerPrefs.SetInt(soundPrefsKey, isSoundOn ? 1 : 0);
        PlayerPrefs.Save(); 
        UpdateButtonSprite();
    }

    void SetVolume(float volume)
    {
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    void UpdateButtonSprite()
    {
        if (isSoundOn)
        {
            soundButton.image.sprite = soundOnSprite;
        }
        else
        {
            soundButton.image.sprite = soundOffSprite;
        }
    }
}
