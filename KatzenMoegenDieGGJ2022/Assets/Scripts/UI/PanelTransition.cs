using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTransition : MonoBehaviour
{

    public Image panelImage;
    public float transitionTime;
    // Start is called before the first frame update
    void Start()
    {
        panelImage = GetComponent<Image>();
        PanelInTransition();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        });
    }

}
