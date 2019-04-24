using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour {

    public bool lookAtPlayer;

    Transform targetToLook;
    
    
	void Start () {
        
        if (lookAtPlayer)
            targetToLook = GameController.instance.player.transform;
	}
	
	void LateUpdate () {
     
        if (targetToLook != null && targetToLook.transform.position.z < transform.position.z)
            gameObject.transform.LookAt(targetToLook);
    }


}
