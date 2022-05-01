using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabObj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kill") Destroy(gameObject);
    }
}
