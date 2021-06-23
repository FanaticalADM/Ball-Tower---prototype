using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeedX;
    [SerializeField]
    private float rotationSpeedZ;
    [SerializeField]
    private AudioClip burstSound;
    private AudioSource audioSource;

    Vector3 startingPosition;
    Vector3 deactivationPosition;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerController.Instance.gameObject.transform;
        startingPosition = transform.position;
        deactivationPosition = startingPosition + 10 * Vector3.up;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeedX * Time.deltaTime * Vector3.right);
        transform.Rotate(rotationSpeedZ * Time.deltaTime * Vector3.forward);

        if (playerTransform.position.y > deactivationPosition.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(burstSound);
            GameManager.Instance.GameOver();
        }
    }

    private void OnEnable()
    {
        startingPosition = transform.position;
        deactivationPosition = startingPosition + 10 * Vector3.up;
    }
}
