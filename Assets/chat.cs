using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class chat : MonoBehaviour
{
    List<string> msglist = new List<string>() { "" };
    public Text chatText, inputMessage;

    public void press()
    {
        GetComponent<PhotonView>().RPC("send", RpcTarget.All, PhotonNetwork.NickName + " : " + inputMessage.text);
    }

    [PunRPC]
    public void send(string msg)
    {
        Debug.Log(msg);
        msglist.Add(msg);
        if (msglist.Count > 100) msglist.RemoveAt(0);
        for (int i = 0; i < msglist.Count; i++)
        {
            chatText.text += msglist[i];
            Debug.Log(chatText.text);
        }
        inputMessage.text = "";
    }
}
