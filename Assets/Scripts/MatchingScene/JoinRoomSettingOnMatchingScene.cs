using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomSettingOnMatchingScene : MonoBehaviourPunCallbacks
{
    [SerializeField]RoomInfoOnMatchingScene roomInfoPrefab;
    private List<string> _cachedRoomNames = new List<string>();
    private List<(string,RoomInfoOnMatchingScene)> _roomInfos = new List< (string, RoomInfoOnMatchingScene) > ();

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
        Debug.Log("ルームリストが更新されました");

        foreach (var aRoomInfo in roomList)
        {
            int aRoomIndex = _roomInfos.FindIndex((aData) => { return aData.Item1 == aRoomInfo.Name; });
            if (!aRoomInfo.IsVisible || !aRoomInfo.IsOpen || !aRoomInfo.RemovedFromList)
            {
                if (aRoomIndex >= 0)
                {
                    if (_roomInfos[aRoomIndex].Item2) 
                    {
                        Destroy(_roomInfos[aRoomIndex].Item2.gameObject);
                    }
                    _roomInfos.RemoveAt(aRoomIndex);
                }
                continue;
            }

            if (aRoomIndex<0)
            {
                RoomInfoOnMatchingScene newInfoObj = Instantiate(roomInfoPrefab);
                _roomInfos.Add((aRoomInfo.Name, newInfoObj));
            }
        }

        Debug.Log(_cachedRoomNames.Count);
    }
}
