using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PunErrorOverlay : MonoBehaviourPunCallbacks
{

    public GameObject punErrorOverlayObj;
    public TextMeshProUGUI errorTextMesh;
    public Button closeOverlayButton;

    // Start is called before the first frame update
    void Start()
    {
        punErrorOverlayObj.SetActive(false);
        closeOverlayButton.onClick.AddListener(() => { punErrorOverlayObj.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        punErrorOverlayObj.SetActive(true);
        errorTextMesh.text = $"{returnCode+12345}:部屋を作成できませんでした";
        if (returnCode == 32766)
        {
            errorTextMesh.text = $"すでに同じ名前の部屋が存在しています。";
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        punErrorOverlayObj.SetActive(true);
        errorTextMesh.text = $"{returnCode}:部屋に参加できませんでした";
        if (returnCode == 32765)
        {
            errorTextMesh.text = $"部屋のメンバー数が定員に達していました。";
        }
    }
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        //多分起こらないはず
        punErrorOverlayObj.SetActive(true);
        errorTextMesh.text = $"認証に失敗しました。";
    }
}
