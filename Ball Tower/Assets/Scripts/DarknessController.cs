using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.MovePosition(Vector3.up * (SpawnManager.Instance.Hight - 20));
    }

}
