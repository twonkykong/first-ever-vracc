using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class gamemanagerPlayerNoVR : MonoBehaviour
{
    public Text prefabCount, players, stats;
    public Canvas canvas;

    private void Update()
    {
        var playersList = PhotonNetwork.PlayerList;
        players.text = "players (" + PhotonNetwork.CountOfPlayers + "/" + PhotonNetwork.CurrentRoom.MaxPlayers + "):";
        foreach (Player player in playersList) players.text += "\n" + player.NickName;

        prefabCount.text = "" + GameObject.FindGameObjectsWithTag("prefabObj").Length;

        stats.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient) stats.text += "You are owner of this room";
    }

    public void hidecanvas()
    {
        if (canvas.enabled) canvas.enabled = false;
        else canvas.enabled = true;
    }
}
