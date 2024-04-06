using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JoinRoomSettingOnMatchingScene : MonoBehaviourPunCallbacks
{
    public struct Roominfo
    {
        public string Name;
        public string ownerName;
        public int OwnerID;
        public string keyword;
        public int CurMemberNum;
        public RoomInfoOnMatchingScene InfoDispObj;

        public Roominfo(string name,string ownerName,int ownerID, string keyword,int curMemberNum, RoomInfoOnMatchingScene infoDispObj) 
        {
            Name = name;
            this.ownerName = ownerName;
            OwnerID = ownerID;
            this.keyword = keyword;
            CurMemberNum = curMemberNum;
            InfoDispObj = infoDispObj;
        }
    }

    [SerializeField]RoomInfoOnMatchingScene roomInfoPrefab;
    private List<Roominfo> _roomInfos = new List<Roominfo> ();

    [SerializeField] private Transform viewportContent;

    public MessageStackDescription messageStackDescription;
    //public GameObject matchingWaitingOverlayObj;


    // Start is called before the first frame update
    void Start()
    {
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


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"ルームリストが更新されました:{roomList.Count}");


        foreach (var aRoomInfo in roomList)
        {
            int aRoomIndex = _roomInfos.FindIndex((aData) => { return aData.Name == aRoomInfo.Name; });//部屋がリストにすでに入っている場合、そのインデックス。(そうでない場合-1)
            bool isRandom = aRoomInfo.CustomProperties.GetCastValue<string>(Consts.IS_RANDOM_MATCHING, "") == "true" ? true : false;//部屋がランダムマッチング用の部屋か
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList|| aRoomInfo.PlayerCount>=4|| isRandom)//部屋がリスト入っているべきで無いなら(入れないなら)
            {
                if (aRoomIndex >= 0)//すでにリストに入っているなら
                {
                    if (_roomInfos[aRoomIndex].InfoDispObj) 
                    {
                        Destroy(_roomInfos[aRoomIndex].InfoDispObj.gameObject);//その部屋の情報を表示するGameObjectを消去
                    }
                    _roomInfos.RemoveAt(aRoomIndex);//部屋の情報を格納しているリストからも消す
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
                _roomInfos.Add(new Roominfo(aRoomInfo.Name, ownerNickName, aRoomInfo.masterClientId, keyword, aRoomInfo.PlayerCount, newInfoObj));//リストに部屋の情報を追加
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
    }
}
