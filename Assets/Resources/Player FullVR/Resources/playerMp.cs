﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMp : MonoBehaviourPunCallbacks
{
    public Camera eye1, eye2;
    private void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;

        gameObject.GetComponentInChildren<bodyMp>().enabled = true;
        gameObject.GetComponentInChildren<headMp>().enabled = true;
        gameObject.GetComponentInChildren<handMp>().enabled = true;
        eye1.enabled = true;
        eye2.enabled = true;
    }
}
