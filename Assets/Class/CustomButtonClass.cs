using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class CustomButtonClass : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    //ボタン画像
    [SerializeField]
    Image image;
    //
    [SerializeField]
    CanvasGroup canvasGroup;
    //ボタン上のテキスト
    [SerializeField]
    TextMeshProUGUI text;

    //アクティブ状態の判定
    [SerializeField]
    bool isInteractable = true;
/*
    [SerializeField]
    ColorSettings buttonColorSettings = new();
    [SerializeField]
    ColorSettings textColorSettings = new();*/
    [SerializeField]
    ButtonEvents events = new();

    //ColorSet currentButtonColorSet;
    //ColorSet currentTextColorSet;

    public bool IsInteractable
    {
        get { return isInteractable; }
        set
        {
            if (value == isInteractable)
            {
                return;
            }

           // SetButtonInteractable(value);
        }
    }

    public ButtonEvents Events { get { return events; } set { events = value; } }

    void Start()
    {
        InitButton();
    }

    //ボタンの初期化を呼び出す関数
    protected virtual void InitButton()
    {
        //SetButtonInteractable(isInteractable);
    }

    //ボタンの初期化を行う
 /*   void SetButtonInteractable(bool value)
    {
        isInteractable = value;

        if (value)
        {
            ChangeButtonColor(buttonColorSettings.normalColor);
            ChangeTextColor(textColorSettings.normalColor);
        }
        else
        {
            ChangeButtonColor(buttonColorSettings.disabledColor);
            ChangeTextColor(textColorSettings.disabledColor);
        }
    }
*/
    //ポインターが重なると呼ばれる
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteractable = false)
        {
            return;
        }

        //ChangeButtonColor(buttonColorSettings.onPointerEnterColor);
        //ChangeTextColor(textColorSettings.onPointerEnterColor);
        AdditionalOnPointerEnterProcess();

        events.onPointerEnter.Invoke();
    }
    //継承先でポインターが重なった時の処理を書く
    protected virtual void AdditionalOnPointerEnterProcess()
    {

    }

    //ポインターが範囲外に出ると呼ばれる
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInteractable = false)
        {
            return;
        }

        //ChangeButtonColor(buttonColorSettings.normalColor);
        AdditionalOnPointerExitProcess();

        events.onPointerExit.Invoke();
    }

    //継承先でポインターが範囲外に出た時の処理を書く
    protected virtual void AdditionalOnPointerExitProcess()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractable = false)
        {
            return;
        }

        AdditionalOnPointerClickProcess();

        events.onClick.Invoke();
    }

    //継承先でポインターが重なった処理を書く
    protected virtual void AdditionalOnPointerClickProcess()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractable = false)
        {
            return;
        }

        //ChangeButtonColor(buttonColorSettings.onPointerDownColor);
        AdditionalOnPointerDownProcess();

        events.onPointerDown.Invoke();
    }

    //継承先でポインターが重なった処理を書く
    protected virtual void AdditionalOnPointerDownProcess()
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInteractable = false)
        {
            return;
        }

        //ChangeButtonColor(buttonColorSettings.normalColor);
        AdditionalOnPointerUpProcess();

        events.onPointerUp.Invoke();
    }

    //継承先でポインターが重なった処理を書く
    protected virtual void AdditionalOnPointerUpProcess()
    {

    }
/*
    public void ChangeButtonColor(Color targetColor, float duration, Ease ease = Ease.Linear)
    {
        if (duration < 0)
        {
            return;
        }

        image.DOColor(targetColor, duration).SetLink(gameObject).SetUpdate(true).SetEase(ease);
        canvasGroup.DOFade(targetColor.a, duration).SetLink(gameObject).SetUpdate(true).SetEase(ease);
    }

    void ChangeButtonColor(ColorSet colorSet)
    {
        if (currentButtonColorSet == colorSet)
        {
            return;
        }

        ChangeButtonColor(colorSet.color, colorSet.duration, colorSet.ease);

        currentButtonColorSet = colorSet;
    }

    public void ChangeTextColor(Color targetColor, float duration, Ease ease = Ease.Linear)
    {
        if (text == null || duration < 0)
        {
            return;
        }

        //text.DOColor(targetColor, duration).SetLink(gameObject).SetUpdate(true).SetEase(ease);
    }

    void ChangeTextColor(ColorSet colorSet)
    {
        if (colorSet == currentTextColorSet)
        {
            return;
        }

        ChangeTextColor(colorSet.color, colorSet.duration, colorSet.ease);

        currentTextColorSet = colorSet;
    }

    [Serializable]
    sealed class ColorSettings
    {

        public ColorSet normalColor;
        public ColorSet disabledColor;
        public ColorSet onPointerEnterColor;
        public ColorSet onPointerDownColor;

    }

    [Serializable]
    sealed class ColorSet
    {

        public Color color = Color.white;
        [Min(0)]
        public float duration = 0;
        public Ease ease = Ease.Linear;

    }*/

}

[Serializable]
public sealed class ButtonEvents
{

    public UnityEvent onPointerEnter = new();
    public UnityEvent onPointerExit = new();
    public UnityEvent onClick = new();
    public UnityEvent onPointerDown = new();
    public UnityEvent onPointerUp = new();

}