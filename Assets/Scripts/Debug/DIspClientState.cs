using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DIspClientState : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI textMesh;
    List<ClientState> clientStates;
    public int dispStateNum=10; 

    // Start is called before the first frame update
    void Start()
    {
        clientStates=new List<ClientState>();
        clientStates.Add(PhotonNetwork.NetworkClientState);
    }

    // Update is called once per frame
    void Update()
    {
        if (clientStates[clientStates.Count - 1] != PhotonNetwork.NetworkClientState) 
        {
            clientStates.Add(PhotonNetwork.NetworkClientState);
        }
        textMesh.text ="";
        for (int i = clientStates.Count-dispStateNum; i < clientStates.Count; i++) 
        {
            if (i < 0)
            {
                textMesh.text += $"«\n";
            }
            else 
            {
                textMesh.text += $"«{clientStates[i].ToString()}\n";
            }
        }
        
    }

    private void Reset()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
}
