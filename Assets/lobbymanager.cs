using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine.SceneManagement;
using System;

public class lobbymanager : MonoBehaviourPunCallbacks
{
    public Text stats, sensors, nicknameInput, nameJoin, nameCreate, maxPlayersCounter;
    public Slider maxPlayers;
    string nickname;
    string roomName;
    public GameObject controller, vrmode;

    private void Start()
    {
        if (PlayerPrefs.GetInt("controller") == 1) controller.GetComponent<Toggle>().isOn = true;
        else controller.GetComponent<Toggle>().isOn = false;

        if (PlayerPrefs.GetInt("vrmode") == 1) vrmode.GetComponent<Toggle>().isOn = true;
        else vrmode.GetComponent<Toggle>().isOn = false;

        if (PlayerPrefs.GetString("nickname") == "") nickname = "Player " + UnityEngine.Random.Range(0, 100);
        else nickname = PlayerPrefs.GetString("nickname");

        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        maxPlayersCounter.text = "" + maxPlayers.value;

        if (nicknameInput.text != "")
        {
            PlayerPrefs.SetString("nickname", nicknameInput.text);
            PhotonNetwork.NickName = nicknameInput.text;
        }
        else PhotonNetwork.NickName = nickname;

        if (controller.GetComponent<Toggle>().isOn == true) PlayerPrefs.SetInt("controller", 1);
        else PlayerPrefs.SetInt("controller", 0);

        if (vrmode.GetComponent<Toggle>().isOn == true)
        {
            controller.SetActive(true);
            PlayerPrefs.SetInt("vrmode", 1);
        }
        else
        {
            controller.SetActive(false);
            PlayerPrefs.SetInt("vrmode", 0);
        }

        stats.text = "Region: " + PhotonNetwork.CloudRegion + "\nNickname: " + PhotonNetwork.NickName + "\nPlayers online: " + PhotonNetwork.CountOfPlayers + "\nRooms: " + PhotonNetwork.CountOfRooms;
        Input.compass.enabled = true;
        sensors.text = "Accelerometer: " + Input.acceleration + "\nCompass: " + Input.compass.magneticHeading + "\nGyroscope: " + Input.gyro.rotationRate;
    }

    public void createRoom()
    {
        if (nameCreate.text == "") roomName = "Room " + PhotonNetwork.CountOfRooms + 1;
        else roomName = nameCreate.text;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = Convert.ToByte(maxPlayers.value) } );
    }

    public void joinRoom()
    {
        if (nameJoin.text == "") PhotonNetwork.JoinRandomRoom();
        else PhotonNetwork.JoinRoom(nameJoin.text);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("online");
    }
}
