using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomSettingOnMatchingScene : MonoBehaviourPunCallbacks
{

    public struct Roominfo
    {
        public string Name;
        public string ownerName;
        public int OwnerID;
        public string keyword;
        public int CurMemberNum;
        public bool IsAbleToEnter;
        public RoomInfoOnMatchingScene InfoDispObj;

        public Roominfo(string name,string ownerName,int ownerID, string keyword,int curMemberNum, RoomInfoOnMatchingScene infoDispObj,bool  isAbleToEnter) 
        {
            Name = name;
            this.ownerName = ownerName;
            OwnerID = ownerID;
            this.keyword = keyword;
            CurMemberNum = curMemberNum;
            InfoDispObj = infoDispObj;
            IsAbleToEnter = isAbleToEnter;
        }
    }

    [SerializeField]RoomInfoOnMatchingScene roomInfoPrefab;
    private List<Roominfo> _roomInfos = new List<Roominfo> ();

    [SerializeField] private Transform viewportContent;
    public MessageStackDescription messageStackDescription;
    public Button UpdateRoomListButton;
    public GameObject NoRoomNowObj;
    //public GameObject matchingWaitingOverlayObj;


    // Start is called before the first frame update
    void Start()
    {
        UpdateRoomListButton.onClick.AddListener(RemoveClosedRoomInfos);
        ResizeViewPortContent();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResizeViewPortContent()
    {
        float contentYSize=(20+20+100)*_roomInfos.Count+150;
        Vector2 contentScale= ((RectTransform)viewportContent).sizeDelta;
        contentScale.y = contentYSize;
        ((RectTransform)viewportContent).sizeDelta = contentScale;
    }

    public void DispNoRoomNowIfSo() 
    {
        if (_roomInfos.Count == 0)
        {
            NoRoomNowObj.SetActive(true);
        }
        else 
        {
            NoRoomNowObj.SetActive(false);
        }
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"PhotonNetwork.InLobby:{PhotonNetwork.InLobby}");

        Debug.Log($"ルームリストが更新されました:{roomList.Count}");


        foreach (var aRoomInfo in roomList)
        {
            int aRoomIndex = _roomInfos.FindIndex((aData) => { return aData.Name == aRoomInfo.Name; });//部屋がリストにすでに入っている場合、そのインデックス。(そうでない場合-1)
            bool isRandom = aRoomInfo.CustomProperties.GetCastValue<string>(Consts.IS_RANDOM_MATCHING, "") == "true" ? true : false;//部屋がランダムマッチング用の部屋か
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList|| aRoomInfo.PlayerCount>=4|| isRandom)//部屋がリスト入っているべきで無いなら(入れないなら)
            {
                if (aRoomIndex >= 0)//すでにリストに入っているなら
                {
                    //isAbleToEnterをfalseにして、表示を変える
                    Roominfo temp = _roomInfos[aRoomIndex];
                    temp.IsAbleToEnter = false;
                    _roomInfos[aRoomIndex] =temp;
                    _roomInfos[aRoomIndex].InfoDispObj.SetValues(_roomInfos[aRoomIndex]);
                    /*
                    if (_roomInfos[aRoomIndex].InfoDispObj) 
                    {
                        Destroy(_roomInfos[aRoomIndex].InfoDispObj.gameObject);//その部屋の情報を表示するGameObjectを消去
                    }
                    _roomInfos.RemoveAt(aRoomIndex);//部屋の情報を格納しているリストからも消す
                    */
                }
                continue;
            }

            if (aRoomIndex < 0)//リストに入っていないなら
            {
                string keyword = aRoomInfo.CustomProperties.GetCastValue<string>(Consts.ROOM_PASSWARD,"");
                string ownerNickName= aRoomInfo.CustomProperties.GetCastValue<string>(Consts.ROOM_OWNER_NICKNAME, "nickName");
                Debug.Log($"{aRoomInfo.Name}'sKeyword:{ keyword}");
                
                RoomInfoOnMatchingScene newInfoObj = Instantiate(roomInfoPrefab, viewportContent); //部屋の情報を表示するGameObjectを作成
                //newInfoObjにシーン内のコンポーネントやGameObjectの参照を渡す
                //newInfoObj.waitingMatchingOverlayObj = matchingWaitingOverlayObj;
                _roomInfos.Add(new Roominfo(aRoomInfo.Name, ownerNickName, aRoomInfo.masterClientId, keyword, aRoomInfo.PlayerCount, newInfoObj,true));//リストに部屋の情報を追加
                newInfoObj.SetValues(_roomInfos[_roomInfos.Count - 1]);//部屋の情報を表示するGameObjectに、情報を表示する為の情報を渡す
            }
            else    //すでにリストに入っているなら
            {
                Roominfo temp = _roomInfos[aRoomIndex];
                temp.CurMemberNum = aRoomInfo.PlayerCount;
                _roomInfos[aRoomIndex]=temp;//部屋の情報を表示するGameObjectに、情報を表示する為の情報を渡す
                
            }
        }
        ResizeViewPortContent();
        DispNoRoomNowIfSo();
    }

    //_roomInfosから、閉じている部屋を除外。対応するゲームオブジェクトも削除
    public void RemoveClosedRoomInfos() 
    {
        List<int> removedInfosIndexes=new List<int>();
        for ( int i=0;i<_roomInfos.Count;i++ ) 
        {
            Roominfo aRoomInfo = _roomInfos[i];
            if (!aRoomInfo.IsAbleToEnter) 
            {
                removedInfosIndexes.Add(i);
            }
        }
        for (int i = removedInfosIndexes.Count - 1; i >= 0; i--) 
        {
            RoomInfoOnMatchingScene infoDispObj = _roomInfos[removedInfosIndexes[i]].InfoDispObj;
            if (infoDispObj)
            {
                Destroy(infoDispObj.gameObject);//その部屋の情報を表示するGameObjectを消去
            }
            _roomInfos.RemoveAt(removedInfosIndexes[i]);//部屋の情報を格納しているリストからも消す
        }
        ResizeViewPortContent();
        DispNoRoomNowIfSo();
    }
}
