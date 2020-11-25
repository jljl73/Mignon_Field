using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField Nickname;
    public InputField Roomname;
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;
    public GameObject LobbyPanel;
    public GameObject UIPanel;
    // Start is called before the first frame update
    int Start_time = 0;
    public int ct = 0;

    void Awake()
    {
        Screen.SetResolution(1366, 768, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();


    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        PhotonNetwork.LocalPlayer.NickName = Nickname.text;
        DisconnectPanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }
    public void CreateRoom()
    {
        //Hashtable hashtable = new Hashtable();
        //int starttime = PhotonNetwork.ServerTimestamp;
        //hashtable.Add("start_time", starttime);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        //roomOptions.CustomRoomProperties = hashtable;
        PhotonNetwork.CreateRoom(Roomname.text, roomOptions, null);
    }

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void JoinRoom() => PhotonNetwork.JoinRoom(Roomname.text);

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        DisconnectPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        Spawn();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        //    PhotonNetwork.Disconnect();
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate("Prefabs/Player", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
        UIPanel.SetActive(true);

        //Start_time = hashtable["start_time"].GetHashCode();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        UIPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RespawnPanel.SetActive(false);
        DisconnectPanel.SetActive(true);
    }

    public void UpdateTime()
    {
        UIPanel.transform.Find("Time").GetComponent<Text>().text =
            ((PhotonNetwork.ServerTimestamp - Start_time) / 1000).ToString();
    }
}
