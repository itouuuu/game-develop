using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomInfoOnMatchingScene : MonoBehaviour
{
    public string RoomName;
    public TextMeshProUGUI TextMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMesh.text = RoomName;
    }
}
