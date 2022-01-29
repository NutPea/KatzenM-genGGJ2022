using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : MonoBehaviour
{
    public float lowgravityValue;
    private float gravityResetValue;

    private void Start()
    {
        gravityResetValue = Physics.gravity.y;
    }
    public void ActivateLowGravity()
    {
        
        StartCoroutine(fadeLowGravity());
        
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

        Physics.gravity = new Vector3(0, gravityResetValue, 0);
        ;
    }
}
