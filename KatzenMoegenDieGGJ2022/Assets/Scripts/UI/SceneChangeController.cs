using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    public int targetSceneIndex;
    public Image panelImage;
    public float transitionTime;
    public LeanTweenType tweenType;
    void Start()
    {
        PanelInTransition();
    }

    public void PanelInTransition()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        Color currPanelColor = panelImage.color;
        currPanelColor.a = 1;
        panelImage.color = currPanelColor;
        LeanTween.value(gameObject, 1, 0, transitionTime).setOnUpdate((float val) =>
        {
            Color panelColor = panelImage.color;
            panelColor.a = val;
            panelImage.color = panelColor;

        }).setEase(tweenType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PanelOutTransition();
        }
    }

    public void PanelOutTransition()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        Color currPanelColor = panelImage.color;
        currPanelColor.a = 0;
        panelImage.color = currPanelColor;
        LeanTween.value(gameObject, 0, 1, transitionTime).setOnUpdate((float val) =>
        {
            Color panelColor = panelImage.color;
            panelColor.a = val;
            panelImage.color = panelColor;

        }).setEase(tweenType).setOnComplete(ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(targetSceneIndex);
    }


}