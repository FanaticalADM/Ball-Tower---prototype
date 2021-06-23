using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private Rigidbody powerupRb;
    private Vector3 startingPosition;
    private Vector3 deactivationPosition;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerController.Instance.gameObject.transform;
        powerupRb = gameObject.GetComponent<Rigidbody>();
        startingPosition = transform.position;
        deactivationPosition = startingPosition + 10 * Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startingPosition.y) 
        powerupRb.velocity=Vector3.up * 3;

        if (playerTransform.position.y > deactivationPosition.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            PlayerController.Instance.FloatEvent();
            gameObject.SetActive(false);

        }
    }

    private void OnEnable()
    {
        startingPosition = transform.position;
        deactivationPosition = startingPosition + 10 * Vector3.up;
    }
}
