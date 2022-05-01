using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerPC : MonoBehaviour
{
    public Camera cam;
    private void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;

        gameObject.GetComponentInChildren<bodyPC>().enabled = true;
        gameObject.GetComponentInChildren<headPC>().enabled = true;
        cam.enabled = true;
        GetComponent<gamemanagerPlayerPC>().enabled = true;
    }
}
