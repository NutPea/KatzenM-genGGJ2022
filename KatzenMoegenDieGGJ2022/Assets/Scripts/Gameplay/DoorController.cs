using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool locked;
    public Transform leftDoor;
    public Transform rightDoor;
    public LeanTweenType leanType;
    public float moveDistance;
    public float moveTime;

    public void OpenDoor()
    {
        if (!locked)
        {
            LeanTween.moveLocalZ(leftDoor.gameObject, moveDistance, moveTime).setEase(leanType);
            LeanTween.moveLocalZ(rightDoor.gameObject, -moveDistance, moveTime).setEase(leanType);
        }
    }
    public void CloseDoor()
    {
        if (!locked)
        {
            LeanTween.moveLocalZ(leftDoor.gameObject, 0, moveTime).setEase(leanType);
            LeanTween.moveLocalZ(rightDoor.gameObject, 0, moveTime).setEase(leanType);
        }
    }

    public void LockDoor()
    {
        locked = true;
    }

    public void UnlockDoor()
    {
        locked = false;
    }
}

