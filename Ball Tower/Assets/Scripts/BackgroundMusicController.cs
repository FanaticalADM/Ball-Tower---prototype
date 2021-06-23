using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;

    public static BackgroundMusicController Instance
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
            audioSource.clip=normalModeMusic;

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
}
