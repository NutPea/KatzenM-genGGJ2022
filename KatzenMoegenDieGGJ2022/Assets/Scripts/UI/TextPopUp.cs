using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public GameObject textObject;
    public float time;
    public LeanTweenType type;
    void Start()
    {
        textObject.transform.localScale = new Vector3(0, 1, 1);
    }



    public void OnPopUp()
    {
        textObject.SetActive(true);
        LeanTween.cancel(textObject);
        LeanTween.scaleX(textObject, 1, time).setEase(type);
    }

    public void OnPopAway()
    {
        LeanTween.cancel(textObject);
        LeanTween.scaleX(textObject, 0, time).setEase(type).setOnComplete(OnDeactivate);
    }

    void OnDeactivate()
    {
        textObject.SetActive(false);
    }
}
