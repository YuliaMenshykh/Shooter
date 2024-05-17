using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamereFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.transform.position - transform.position;
    }


    void FixedUpdate()
    {
        transform.position = target.transform.position - offset;
    }
}
