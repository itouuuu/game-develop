using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoOnMatchingScene : MonoBehaviour
{
    public string RoomName;
    public int OwnerID;
    public string OwnerNickName="";
    public string keyword;
    public int CurMemberNum;
    public bool IsAbleToEnter;

    //以下4つが、上4つの変数に対応するテキストを表示する
    public TextMeshProUGUI roomNameTextMesh;
    public TextMeshProUGUI ownerTextMesh;
    public TextMeshProUGUI isSetKeywordTextMesh;
    public TextMeshProUGUI curMemberNumTextMesh;
    public Button button;
    public Image image;

    public DispDescriptionOnHover dispDescription;
    //public GameObject waitingMatchingOverlayObj;
    public KeywordInputOverLayOnMatchingScene keywordInputOverlay;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!keywordInputOverlay) 
        {
            KeywordInputOverLayOnMatchingScene[] tmpAry=FindObjectsByType<KeywordInputOverLayOnMatchingScene>(FindObjectsInactive.Include,FindObjectsSortMode.None);
            if (tmpAry.Length > 0)
            {
                Debug.Log("kakakaka");
                keywordInputOverlay = tmpAry[0];
            }
            else 
            {
                Debug.Log("wwwwww");
            }
        }
        /*
        if (!waitingMatchingOverlayObj) 
        {
            waitingMatchingOverlayObj = GameObject.Find("WaitingOverlay");
        }
        */
        if (!dispDescription) 
        {
            GameObject descriptionObj=GameObject.Find("Description");
            if (descriptionObj) 
            {
                dispDescription = descriptionObj.GetComponent<DispDescriptionOnHover>();
            }
        }
        button.onClick.AddListener(OnClicked);
        ResetTextMeshsText();

        

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Reset()
    {
        button=GetComponent<Button>();
        dispDescription = GetComponent<DispDescriptionOnHover>();
        Transform temp = this.transform.Find("RoomName");
        roomNameTextMesh=temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("RoomOwner");
        ownerTextMesh =temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("KeywordExistence");
        isSetKeywordTextMesh = temp.GetComponent<TextMeshProUGUI>();
        temp = this.transform.Find("MemberNum");
        curMemberNumTextMesh = temp.GetComponent<TextMeshProUGUI>();

    }

    public void SetValues(JoinRoomSettingOnMatchingScene.Roominfo roomInfo) 
    {
        RoomName = roomInfo.Name;
        OwnerNickName = roomInfo.ownerName;
        OwnerID = roomInfo.OwnerID;
        keyword = roomInfo.keyword;
        CurMemberNum = roomInfo.CurMemberNum;
        IsAbleToEnter = roomInfo.IsAbleToEnter;
        ResetTextMeshsText();
        ResetButtonTransition();
    }

    //実際に表示される情報を、現在のRoomInfo/OwnerID/IsSetKeyword/CurMemberNumの値に合わせて変更する。
    public void ResetTextMeshsText() 
    {
        roomNameTextMesh.text = RoomName.Substring(Consts.ROOM_MATCHING_NAME_PREFIX.Length);
        ownerTextMesh.text = $"{OwnerNickName}#{OwnerID}";
        isSetKeywordTextMesh.text = keyword!="" ? "あり" :"無し";
        curMemberNumTextMesh.text = IsAbleToEnter?$"{CurMemberNum}/4":"Closed";
        dispDescription.DispMessage = IsAbleToEnter?$"部屋「{RoomName}」に参加する(残り{4-CurMemberNum}名)":"この部屋には現在参加できません";
    }

    public void ResetButtonTransition() 
    {
        if (IsAbleToEnter)
        {
            button.transition = Selectable.Transition.ColorTint;
            ColorBlock cb= button.colors;
            cb.normalColor =new Color(1,1,1);
            button.colors = cb;
        }
        else 
        {
            button.transition = Selectable.Transition.None;
            image.color = new Color(0.8f, 0.8f, 0.8f);
        }
    }

    public void OnClicked() 
    {
        if (!IsAbleToEnter) 
        {
            return;
        }

        if (keyword != "")//合言葉無しなら 
        {
            keywordInputOverlay.ActivateOverlay(RoomName, keyword);
        }
        else 
        {
            PhotonNetwork.JoinRoom(RoomName);
            //waitingMatchingOverlayObj.SetActive(true);
        }
    }


}
