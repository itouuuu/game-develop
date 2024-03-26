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
        public int OwnerID;
        public bool IsSetKeyword;
        public int CurMemberNum;
        public RoomInfoOnMatchingScene InfoDispObj;

        public Roominfo(string name,int ownerID,bool isSetKeyword,int curMemberNum, RoomInfoOnMatchingScene infoDispObj) 
        {
            Name = name;
            OwnerID = ownerID;
            IsSetKeyword = isSetKeyword;
            CurMemberNum = curMemberNum;
            InfoDispObj = infoDispObj;
        }
    }

    [SerializeField]RoomInfoOnMatchingScene roomInfoPrefab;
    private List<Roominfo> _roomInfos = new List<Roominfo> ();

    [SerializeField] private Transform viewportContent;

    public MessageStackDescription messageStackDescription;
    public GameObject matchingWaitingOverlayObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"ルームリストが更新されました:{roomList.Count}");


        foreach (var aRoomInfo in roomList)
        {
            int aRoomIndex = _roomInfos.FindIndex((aData) => { return aData.Name == aRoomInfo.Name; });
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || aRoomInfo.RemovedFromList)//部屋がリスト入っているべきで無いなら
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
                string keyword = "";
                if (aRoomInfo.CustomProperties.ContainsKey("Keyword") && aRoomInfo.CustomProperties["Keyword"] is string) 
                {
                    keyword = (string)aRoomInfo.CustomProperties["Keyword"];
                }
                Debug.Log($"{aRoomInfo.Name}7sKeyword:{ keyword}");
                
                RoomInfoOnMatchingScene newInfoObj = Instantiate(roomInfoPrefab, viewportContent); //部屋の情報を表示するGameObjectを作成
                //newInfoObjにシーン内のコンポーネントやGameObjectの参照を渡す
                newInfoObj.waitingMatchingOverlayObj = matchingWaitingOverlayObj;
                _roomInfos.Add(new Roominfo(aRoomInfo.Name, aRoomInfo.masterClientId, keyword!="", aRoomInfo.PlayerCount, newInfoObj));//部屋の情報を格納しているリストにその部屋の情報を追加
                newInfoObj.SetValues(_roomInfos[_roomInfos.Count - 1]);//部屋の情報を表示するGameObjectに、情報を表示する為の情報を渡す
            }
            else    //すでにリストに入っているなら
            {
                Roominfo temp = _roomInfos[aRoomIndex];
                temp.CurMemberNum = aRoomInfo.PlayerCount;
                _roomInfos[aRoomIndex]=temp;//部屋の情報を表示するGameObjectに、情報を表示する為の情報を渡す
                
            }
        }

    }
}
