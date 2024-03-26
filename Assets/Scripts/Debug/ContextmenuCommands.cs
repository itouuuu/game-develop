using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextmenuCommands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Mov_dispDescription")]
    private void Mov_dispDescriptionTodispDescription()
    {
        RoomInfoOnMatchingScene[] ary= FindObjectsByType<RoomInfoOnMatchingScene>(FindObjectsSortMode.None);
        for (int i = 0; i < ary.Length; i++) 
        {
            Debug.Log(ary[i].name);
            ary[i].dispDescription = ary[i].dispDescription;
        }
    }
}
