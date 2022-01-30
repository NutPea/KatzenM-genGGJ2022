using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonScaleOnHover : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public float scaleAmount= 1.1f;
    public float scaleTime = 0.1f;
    float startScaleAmount;
    public UnityEvent onHoverEvent;
    private void Start()
    {
        startScaleAmount = transform.localScale.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHoverEvent.Invoke();
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(scaleAmount, scaleAmount, 1), scaleTime).setIgnoreTimeScale(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(startScaleAmount, startScaleAmount, 1), scaleTime).setIgnoreTimeScale(true);
    }
}
