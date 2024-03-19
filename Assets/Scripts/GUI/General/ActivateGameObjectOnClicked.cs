using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ActivateGameObjectOnClicked : MonoBehaviour
{
    public GameObject TargetObject;
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
        if (TargetObject)
        {
            TargetObject.SetActive(!TargetObject.activeSelf);
        }
        else 
        {
            Debug.Log("TargetObjectÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
    } 


}
