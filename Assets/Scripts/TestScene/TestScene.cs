using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Prefabs/Debug/ForDebugCube", new Vector3(Random.Range(-3,3),Random.Range(-3,3),0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
