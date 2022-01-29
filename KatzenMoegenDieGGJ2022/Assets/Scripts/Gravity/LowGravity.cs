using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : MonoBehaviour
{
    public float lowgravityValue;
    private float gravityResetValue;
    GravityManager gm;

    private void Start()
    {
        gm = GetComponent<GravityManager>();
        gravityResetValue = Physics.gravity.y;
    }
    public void ActivateLowGravity()
    {
        if(gm.upSideDown)
        {
            Physics.gravity = new Vector3(0, -lowgravityValue, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, lowgravityValue, 0);
        }
        
        
        
    }
    IEnumerator fadeLowGravity()
    {
        float time = 1;
        while( time > 0)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
            Physics.gravity = new Vector3(0,Mathf.Lerp(lowgravityValue, gravityResetValue, time),0);
        }
    }

    public void restoreNormalGravity()
    {
        Debug.Log(gravityResetValue);

        if (gm.upSideDown)
        {
            Physics.gravity = new Vector3(0, -gravityResetValue, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, gravityResetValue, 0);
        }

        
        
    }
}
