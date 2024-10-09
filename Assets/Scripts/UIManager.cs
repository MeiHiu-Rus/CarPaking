using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;




public class UIManager : MonoBehaviour
{
    [SerializeField] LinesDrawer linesDrawer;

    [Space]
    [SerializeField] private CanvasGroup availableLineCanvasGroup;
    [SerializeField] private GameObject availableLineHolder;
    [SerializeField] private Image availableLineFill;
    private bool isAvailableLineUIActive = false;

    [Space]
    
    [SerializeField] float fadeDuration;

    private Route activeRoute;


    private void Start()
    {
        
        availableLineCanvasGroup.alpha = 0f;

        linesDrawer.OnBeginDraw += OnBeginDrawHandler;
        linesDrawer.OnDraw      += OnDrawHandler;
        linesDrawer.OnEndDraw   += OnEndDrawHandler;
    }

    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;

        availableLineFill.color = activeRoute.carColor;
        availableLineFill.fillAmount = 1f;
        availableLineCanvasGroup.DOFade(1f, .3f).From(0f);
        isAvailableLineUIActive = true;
    }

    private void OnDrawHandler()
    {
        if (isAvailableLineUIActive)
        {
            float maxLineLength = activeRoute.maxLineLength;
            float lineLength = activeRoute.line.length;

            availableLineFill.fillAmount = 1 - (lineLength / maxLineLength);
        }
    }

    private void OnEndDrawHandler()
    {
        if (isAvailableLineUIActive)
        {
            isAvailableLineUIActive = false;

           activeRoute = null;

            availableLineCanvasGroup.DOFade(0f, .3f).From(1f);

        }
    }

    
    
}
