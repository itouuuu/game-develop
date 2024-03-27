using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageStackDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionTextMesh;
    [SerializeField] GameObject descriptionPanelObj;


    List<(GameObject keyObj, string description)> _messageStack = new List<(GameObject keyObj, string description)>();
    public void DispNewDescription(GameObject keyObj, string description)
    {
        _messageStack.Add((keyObj, description));
        descriptionPanelObj.SetActive(true);
        descriptionTextMesh.text = description;

    }


    public void DeleteDescription(GameObject keyObj)
    {
        int deleteElementIndex = _messageStack.FindLastIndex((aData) => { return aData.keyObj == keyObj; });
        _messageStack.RemoveAt(deleteElementIndex);
        if (_messageStack.Count == 0)
        {
            descriptionPanelObj.SetActive(false);
        }
        else
        {
            descriptionTextMesh.text = _messageStack[_messageStack.Count - 1].description; ;
        }
    }

    private void Reset()
    {
        descriptionPanelObj = this.gameObject;
        Transform descriptionTextMeshTransform=this.transform.Find("Text (TMP)");
        if (descriptionPanelObj) 
        {
            descriptionTextMesh = descriptionTextMeshTransform.gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
}
