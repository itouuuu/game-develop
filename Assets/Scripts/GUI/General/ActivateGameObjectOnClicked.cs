using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ActivateGameObjectOnClicked : MonoBehaviour
{
    public enum OnClickedMode 
    {
        ActivateObject,
        DeactivateObject,
        SwichActivation
    }

    public GameObject TargetObject;
    private Button button;
    [SerializeField] private OnClickedMode _mode=OnClickedMode.SwichActivation;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked() 
    {
        if (TargetObject)
        {
            TargetObject.SetActive(_mode == OnClickedMode.SwichActivation ? !TargetObject.activeSelf : _mode == OnClickedMode.ActivateObject ? true : false);
        }
        else 
        {
            Debug.Log("TargetObjectが設定されていません");
        }
    } 


}
