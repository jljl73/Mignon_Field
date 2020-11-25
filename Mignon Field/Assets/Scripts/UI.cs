using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    int Start_time = 0;
    public GameObject LobbyPanel;
    void OnEnable()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Photon.Pun.UtilityScripts.CountdownTimer.SetStartTime();
            Photon.Pun.UtilityScripts.CountdownTimer.TryGetStartTime(out int startTimestamp);
            Start_time = startTimestamp;
            Hashtable ht = new Hashtable { { "start_time", startTimestamp } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        }
        else
        {
            Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;
            Start_time = ht["start_time"].GetHashCode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && Input.GetKeyDown(KeyCode.Escape))
        {

            PhotonNetwork.LeaveRoom();
            LobbyPanel.SetActive(true);
            transform.gameObject.SetActive(false);
        }

        UpdateTime();
    }

    public void UpdateTime()
    {
        transform.Find("Time").GetComponent<Text>().text =
            ((PhotonNetwork.ServerTimestamp - Start_time) / 1000).ToString();
    }
}