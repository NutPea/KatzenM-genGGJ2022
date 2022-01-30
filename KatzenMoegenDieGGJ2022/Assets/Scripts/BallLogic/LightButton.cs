using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class LightButton : MonoBehaviour
{
    public float activeTime = 2;

    public Light lightComponent;

    public Color offColor;
    public Color onColor;
    private float fadeState = 0;
    public VisualEffect impactParticles;
    [SerializeField] private MeshRenderer meshRendererEmission;
    private static int emissionColorKey = -1;
    public bool isActive;
    public UnityEvent activateEvent;
    public UnityEvent deactivateEvent;

    private void Start()
    {
        if (emissionColorKey == -1)
            emissionColorKey = Shader.PropertyToID("_EmissionColor");
        
    }

    public void ActivateButton()
    {
        StartCoroutine(ColorFadeOn());
        StartCoroutine(Deactivate(activeTime));
        impactParticles.Play();
    }

    IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(ColorFadeOff());

    }

    IEnumerator ColorFadeOn()
    {
        while(fadeState < 1)
        {
            yield return new WaitForEndOfFrame();
            fadeState += Time.deltaTime / 0.5f;
            Color color = Color.Lerp(offColor, onColor, fadeState);
            lightComponent.color = color;
            meshRendererEmission.material.SetColor(emissionColorKey ,color);
        }
        fadeState = 1;
        
    }


    IEnumerator ColorFadeOff()
    {
        
        while (fadeState > 0)
        {
            yield return new WaitForEndOfFrame();
            fadeState -= Time.deltaTime / 0.5f;
            Color color = Color.Lerp(offColor, onColor, fadeState);
            lightComponent.color = color;
            meshRendererEmission.material.SetColor(emissionColorKey, color);
        }
        fadeState = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ball"))
        { 
            ActivateButton();
        }
    }


}
