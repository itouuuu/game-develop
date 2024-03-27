using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKeyComp : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 posOffset = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) 
        {
            posOffset += Vector3.up * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            posOffset += Vector3.left * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            posOffset += Vector3.down * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            posOffset += Vector3.right * Time.fixedDeltaTime;
        }
        if (photonView.IsMine) 
        {
            this.transform.position += posOffset;

        }
    }
}
