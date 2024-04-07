using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeywordInputOverLayOnMatchingScene : MonoBehaviour
{
    public string keyword;
    public string roomName;
    //public GameObject waitingMatchingOverlay;
    public GameObject errorMessageObj;
    public Button okButton;
    public Button cancelButton;
    public TMP_InputField keywordInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateOverlay(string roomName,string keyword) 
    {
        this.roomName = roomName;
        this.keyword = keyword;
        this.gameObject.SetActive(true);
        errorMessageObj.SetActive(false);
        keywordInput.text = "";
        okButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(OnOKButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    public void OnOKButtonClicked() 
    {
        if (keywordInput.text == keyword)
        {
            PhotonNetwork.JoinRoom(roomName);
            this.gameObject.SetActive(false);
            //waitingMatchingOverlay.SetActive(true);

        }
        else 
        {
            errorMessageObj.SetActive(true);
        }
        
    }

    public void OnCancelButtonClicked() 
    {
        roomName = "";
        keyword = "";
        this.gameObject.SetActive(false);
    }
}
