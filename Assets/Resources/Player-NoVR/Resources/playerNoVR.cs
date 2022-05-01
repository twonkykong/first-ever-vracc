using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerNoVR : MonoBehaviour
{
    public Camera cam;
    public GameObject canvas;
    private void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;

        gameObject.GetComponentInChildren<bodyNoVR>().enabled = true;
        gameObject.GetComponentInChildren<headNoVR>().enabled = true;
        canvas.SetActive(true);
        cam.enabled = true;
        GetComponent<gamemanagerPlayerNoVR>().enabled = true;
    }
}
