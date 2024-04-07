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
        errorTextMesh.text = $"{returnCode+12345}:�������쐬�ł��܂���ł���";
        if (returnCode == 32766)
        {
            errorTextMesh.text = $"���łɓ������O�̕��������݂��Ă��܂��B";
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        punErrorOverlayObj.SetActive(true);
        errorTextMesh.text = $"{returnCode}:�����ɎQ���ł��܂���ł���";
        if (returnCode == 32765)
        {
            errorTextMesh.text = $"�����̃����o�[��������ɒB���Ă��܂����B";
        }
    }
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        //�����N����Ȃ��͂�
        punErrorOverlayObj.SetActive(true);
        errorTextMesh.text = $"�F�؂Ɏ��s���܂����B";
    }
}
