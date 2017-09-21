using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "PlayerParent" || other.gameObject.tag != "CollisionIgnore" || other.gameObject.tag !="weapon" || other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
