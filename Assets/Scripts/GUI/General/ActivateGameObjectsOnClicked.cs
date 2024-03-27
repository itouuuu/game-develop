using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ActivateGameObjectsOnClicked : MonoBehaviour
{
    public enum OnClickedMode
    {
        ActivateObject,
        DeactivateObject,
        SwichActivation
    }
    [SerializeField] private OnClickedMode _mode = OnClickedMode.SwichActivation;
    public GameObject[] TargetObjects;
    private Button button;

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
        foreach (GameObject TargetObject in TargetObjects) 
        {
            if (TargetObject)
            {
                TargetObject.SetActive(_mode==OnClickedMode.SwichActivation?!TargetObject.activeSelf: _mode == OnClickedMode.ActivateObject ?true:false);
            }
        }
        
    }


}
