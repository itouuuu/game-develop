using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlyOneSelectedButton : MonoBehaviour
{
    bool isSelected=false;
    public List<OnlyOneSelectedButton> OtherButton;
    public Button ThisButton;
    public TextMeshProUGUI ThisTextMesh;
    public Image ThisImage;

    public Color SelectedNormalColor;
    public Color SelectedPressedColor;
    public Color SelectedHighlightedColor;
    public Color SelectedTextMeshColor;

    public Color UnSelectedNormalColor;
    public Color UnSelectedPressedColor;
    public Color UnSelectedHighlightedColor;
    public Color UnSelectedTextMeshColor;


    // Start is called before the first frame update
    void Start()
    {
        ThisButton.onClick.AddListener(switchSelect);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchSelect() 
    {
        if (isSelected)
        {
            UnSelect();
        }
        else 
        {
            Select();
        }
    }

    public void Select()
    {
        foreach (OnlyOneSelectedButton aButton in OtherButton) 
        {
            if (aButton == this) 
            {
                continue;
            }
            aButton.UnSelect();
        }

        ColorBlock colorBlock = ThisButton.colors;
        colorBlock.normalColor = SelectedNormalColor;
        colorBlock.selectedColor = SelectedNormalColor;
        colorBlock.disabledColor = SelectedNormalColor;
        colorBlock.pressedColor= SelectedNormalColor;
        colorBlock.highlightedColor = SelectedNormalColor;
        ThisButton.colors = colorBlock;
        ThisTextMesh.color = SelectedTextMeshColor;
        ThisImage.color = SelectedTextMeshColor;
        isSelected = true;
    }

    public void UnSelect() 
    {
        ColorBlock colorBlock = ThisButton.colors;
        colorBlock.normalColor = UnSelectedNormalColor;
        colorBlock.selectedColor = UnSelectedNormalColor;
        colorBlock.disabledColor = UnSelectedNormalColor;
        colorBlock.pressedColor = UnSelectedNormalColor; 
        colorBlock.highlightedColor = UnSelectedNormalColor;
        ThisButton.colors = colorBlock;
        ThisTextMesh.color = UnSelectedTextMeshColor;
        ThisImage.color = UnSelectedTextMeshColor;

        isSelected =false;

    }

    public void Reset()
    {
        ThisButton = GetComponent<Button>();
        ThisTextMesh = this.transform.FindAndGetComponent<TextMeshProUGUI>("Text (TMP)");
        ThisImage = this.transform.FindAndGetComponent<Image>("Image");

        SelectedNormalColor =new Color(121f/255,98f/255,74f / 255);
        SelectedHighlightedColor = new Color(121f / 255, 98f / 255, 74f / 255)*0.9f;
        SelectedPressedColor= new Color(121f / 255, 98f / 255, 74f / 255) * 0.7f;
        SelectedTextMeshColor=new Color(1,1,1);

        UnSelectedNormalColor = new Color(1, 1, 1);
        UnSelectedHighlightedColor = new Color(0.9f, 0.9f, 0.9f);
        UnSelectedPressedColor = new Color(0.7f, 0.7f, 0.7f);
        UnSelectedTextMeshColor = new Color(0, 0, 0);
    }
}
