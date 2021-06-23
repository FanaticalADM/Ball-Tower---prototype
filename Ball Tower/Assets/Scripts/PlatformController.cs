using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float moveChanceValue;
    private float moveChance;
    private Rigidbody platformRb;

    [SerializeField]
    private float moveRange;

    Vector3 startingPosition;
    Vector3 deactivationPosition;

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerController.Instance.gameObject.transform;
        moveChance = Random.Range(0, 100);
        startingPosition = transform.position;
        deactivationPosition = startingPosition + 10 * Vector3.up;
        platformRb = gameObject.GetComponent<Rigidbody>();

        if (moveChance > 100 - moveChanceValue)
        {
            platformRb.MovePosition(startingPosition + Vector3.right * moveRange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveChance > 100 - moveChanceValue && transform.position.x >= startingPosition.x + moveRange)
        {

            platformRb.velocity = Vector3.left;

        }

        if (moveChance > 100 - moveChanceValue && transform.position.x <= startingPosition.x - moveRange)
        {

            platformRb.velocity = Vector3.right;
        }

        if (playerTransform.position.y > deactivationPosition.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        startingPosition = transform.position;
        deactivationPosition = startingPosition + 10 * Vector3.up;
    }

    private void OnDisable()
    {
        platformRb.velocity = new Vector3(0, 0, 0);
        platformRb.angularVelocity = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
    }

}
