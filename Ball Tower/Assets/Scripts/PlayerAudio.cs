using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip powerupSound;
    [SerializeField]
    private AudioClip floatSound;
    [SerializeField]
    private AudioClip burstSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        PlayerController.Instance.onJumpEvent += JumpSoundOn;
        PlayerController.Instance.onDoubleJumpRestoration+= PowerUpSoundOn;
        PlayerController.Instance.onFloatEvent += FloatSoundOn;
    }

    // Update is called once per frame
    void JumpSoundOn()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    void PowerUpSoundOn()
    {
        audioSource.PlayOneShot(powerupSound);
    }

    void FloatSoundOn()
    {
        audioSource.PlayOneShot(floatSound);
    }
}
