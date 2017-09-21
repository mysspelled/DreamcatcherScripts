using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour {

    public bool OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerParent")
        {
            return true;
        }
        else
            return false;
    }

    public bool OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerParent")
        {
            return false;
        }
        else
            return true;
    }
}
