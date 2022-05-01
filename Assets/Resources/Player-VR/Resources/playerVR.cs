using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerVR : MonoBehaviourPunCallbacks
{
    public Camera eye1, eye2;
    private void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;

        gameObject.GetComponentInChildren<bodyVR>().enabled = true;
        gameObject.GetComponentInChildren<headVR>().enabled = true;
        eye1.enabled = true;
        eye2.enabled = true;
    }
}
