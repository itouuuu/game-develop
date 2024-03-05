using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartButton : CustomButtonClass
{
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    float onPointerEnterScale = 1;
    [SerializeField]
    float onPointerDownScale = 1;
    [SerializeField]
    float duration = 0.1f;
    [SerializeField]
    Ease ease = Ease.OutQuad;

    protected override void AdditionalOnPointerEnterProcess()
    {
        ChangeButtonScale(onPointerEnterScale);
    }

    protected override void AdditionalOnPointerExitProcess()
    {
        ChangeButtonScale(1);
        Debug.Log("aaaaaa");
    }

    protected override void AdditionalOnPointerDownProcess()
    {
        ChangeButtonScale(onPointerDownScale);
    }

    protected override void AdditionalOnPointerUpProcess()
    {
        ChangeButtonScale(1);
    }

    void ChangeButtonScale(float scale)
    {
        rectTransform.DOScale(Vector3.one * scale, duration).SetLink(gameObject).SetEase(ease).SetUpdate(true);
    }
}
