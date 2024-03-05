using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "MagicAttack(Clone)") {
            // 衝突したら弾を削除する。
            //DestroyMagicAttack();
        }
    }
}
