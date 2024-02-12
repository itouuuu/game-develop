using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : WizardParameter
{
    private CharacterController playerController;
    private Vector3 moveVelocity;
    void Start()
    {
        playerController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        moveVelocity = MoveTank();
        ShotShell(this.transform.position);
        SetLandmine(this.transform.position);
        transform.LookAt(Input.mousePosition);
    }

    void FixedUpdate() {
        //自身のオブジェクトのみ移動させる
        if(photonView.IsMine == true){
	        playerController.Move(moveVelocity * Time.deltaTime);
        }
    }
}
