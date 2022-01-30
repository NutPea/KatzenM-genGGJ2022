using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotCamHandler : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.y += speed * Time.deltaTime;
        transform.eulerAngles = rot;
    }
}
