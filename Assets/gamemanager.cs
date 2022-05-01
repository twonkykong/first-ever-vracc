using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemanager : MonoBehaviourPunCallbacks
{
    public GameObject prefabFullVR, prefabVR, prefabNoVR, prefabPC;

    public void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxPlayer ||Application.platform == RuntimePlatform.OSXPlayer)
        {
            PhotonNetwork.Instantiate(prefabPC.name, pos, Quaternion.identity);
        }
        else
        {
            if (PlayerPrefs.GetInt("controller") == 1 && PlayerPrefs.GetInt("vrmode") == 1) PhotonNetwork.Instantiate(prefabFullVR.name, pos, Quaternion.identity);
            else if (PlayerPrefs.GetInt("controller") == 0 && PlayerPrefs.GetInt("vrmode") == 1) PhotonNetwork.Instantiate(prefabVR.name, pos, Quaternion.identity);
            else PhotonNetwork.Instantiate(prefabNoVR.name, pos, Quaternion.identity);
        }
    }

    public void leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        Debug.Log("left " + otherPlayer.NickName);
    }
}
