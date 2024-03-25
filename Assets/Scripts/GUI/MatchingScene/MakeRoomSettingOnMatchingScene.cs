using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeRoomSettingOnMatchingScene : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_InputField _keywordInputField;
    [SerializeField] private GameObject _roomMakeingOverlayObj;
    [SerializeField] private GameObject _matchMakingOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClikedMakeRoomButton() 
    {
        string roomName = _nameInputField.text;
        string roomKeyword= _keywordInputField.text;

        ExitGames.Client.Photon.Hashtable roomProperty = new ExitGames.Client.Photon.Hashtable();
        roomProperty["Keyword"] = roomKeyword;
        string[] publicPropsForLobby=new string[] { "Keyword" };


        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.CustomRoomProperties = roomProperty;
        roomOptions.CustomRoomPropertiesForLobby = publicPropsForLobby;
        PhotonNetwork.CreateRoom(roomName,roomOptions);

        Debug.Log($"Name:{_nameInputField.text}  Keyword:{_keywordInputField.text}");
        _roomMakeingOverlayObj.SetActive(true);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("ÉãÅ[ÉÄÇÃçÏê¨Ç…ê¨å˜ÇµÇ‹ÇµÇΩÅI");
        _roomMakeingOverlayObj.SetActive(false);
        _matchMakingOverlay.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ÉãÅ[ÉÄÇÃçÏê¨Ç…é∏îsÇµÇ‹ÇµÇΩ:{message}");
    }
}
