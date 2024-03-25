using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingOtherOnlineMemberOverlay : MonoBehaviour
{
    public TextMeshProUGUI messageTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
