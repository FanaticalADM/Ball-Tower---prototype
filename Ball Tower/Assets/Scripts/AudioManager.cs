using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private AudioClip normalModeMusic;
    [SerializeField]
    private AudioClip hardcoreModeMusic;
    [SerializeField]
    private AudioClip buttonSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        GameManager.Instance.onHardcoreModeToggle += MusicOn;
        audioSource.clip = normalModeMusic;
    }

    public void MusicOn(bool isHardcoreModeOn)
    {
        if (!isHardcoreModeOn)
            audioSource.clip = normalModeMusic;

        if (isHardcoreModeOn)
            audioSource.clip = hardcoreModeMusic;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
