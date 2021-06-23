using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Material basicMaterial;
    [SerializeField]
    private Material extraMaterial;
    private Rigidbody platformRb;

    private bool isGround;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        platformRb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.name == ("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meshRenderer.material = extraMaterial;

            //Normal Mode
            if (!GameManager.Instance.HardcoreModeOn)
            {
                if (!isGround)
                    platformRb.constraints = RigidbodyConstraints.FreezeAll;
            }

            //Hardcore Mode
            if (GameManager.Instance.HardcoreModeOn)
            {
                if (!isGround)
                    platformRb.constraints = RigidbodyConstraints.FreezePosition;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meshRenderer.material = basicMaterial;
        }
    }
}
