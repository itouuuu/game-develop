using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingOtherOnlineMemberOverlay : MonoBehaviour
{
    public TextMeshProUGUI messageTextMesh;
    public Button cancelButton;
    public string BattleSceneName = "TestScene";


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

        messageTextMesh.text = $"‘¼‚ÌŽQ‰ÁŽÒ‚ð‘Ò‚Á‚Ä‚¢‚Ü‚·c({playerNum}/4)";
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount >= 4)
        {
            SceneManager.LoadScene(BattleSceneName);
        }
    }

    public void OnCancelButtonClicked() 
    {
        this.gameObject.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}
