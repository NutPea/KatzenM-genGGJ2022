using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : MonoBehaviour
{
    public float lowgravityValueMultiplyier;
    GravityManager gm;
    bool isLowGravity;

    private void Start()
    {
        gm = GetComponent<GravityManager>();
    }

    public void ChangeGravity()
    {
        if (isLowGravity)
        {
            RestoreNormalGravity();
            isLowGravity = false;
        }
        else
        {
            ActivateLowGravity();
            isLowGravity = true;
        }
    }

    private void ActivateLowGravity()
    {
        float newGravityValue = Physics.gravity.y * lowgravityValueMultiplyier;
        Debug.Log(newGravityValue);
        Physics.gravity = new Vector3(0, newGravityValue, 0);
         
    }


    private void RestoreNormalGravity()
    {
        float newGravityValue = Physics.gravity.y / lowgravityValueMultiplyier;
        Debug.Log(newGravityValue);
        Physics.gravity = new Vector3(0, newGravityValue, 0);



    }
}
