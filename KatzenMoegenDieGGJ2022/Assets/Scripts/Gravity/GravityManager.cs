using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public Transform playerTransform;
    PlayerController playerController;

    public float flipGravityTime = 1f;
    public bool upSideDown;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {  
    }

    public void StartFlipGravity()
    {
        StartCoroutine(FlipGravityRoutine(flipGravityTime));
    }

    IEnumerator FlipGravityRoutine(float time)
    {
        Physics.gravity *= -1;
        if (upSideDown)
        {
            playerController.upsideDown = false;
            playerController.stopMovementUntilHitGroundAgain = true;
        }
        else
        {
            playerController.upsideDown = true;
            playerController.stopMovementUntilHitGroundAgain = true;
        }
        playerController.forceAired = true;
        yield return new WaitForSeconds(time);
        playerController.forceAired = false;
        RotatePlayer();
    }

    void RotatePlayer()
    {
        if (upSideDown)
        {
            playerTransform.transform.eulerAngles = new Vector3(playerTransform.transform.eulerAngles.x, playerTransform.transform.eulerAngles.y, 0);
            upSideDown = false;
        }
        else
        {
            playerTransform.transform.eulerAngles = new Vector3(playerTransform.transform.eulerAngles.x, playerTransform.transform.eulerAngles.y, 180);
            upSideDown = true;
        }

    }

}
