using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitingOtherOnlineMemberOverlay : MonoBehaviour
{
    public TextMeshProUGUI messageTextMesh;
    public Button cancelButton;

    // Start is called before the first frame update
    void Start()
    {
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        int playerNum = 0;
        if (PhotonNetwork.InRoom)
        {
            playerNum=PhotonNetwork.CurrentRoom.PlayerCount;
        }

        messageTextMesh.text = $"ëºÇÃéQâ¡é“Çë“Ç¡ÇƒÇ¢Ç‹Ç∑Åc({playerNum}/4)";
    }

    public void OnCancelButtonClicked() 
    {
        this.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}
