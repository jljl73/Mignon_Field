using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{

    public float Moving_Speed;
    private bool is_running = false;
    public PhotonView PV;
    public Text Nicknametxt;

    Vector3 curPos;
    Quaternion curRot;

    void Awake()
    {
        Nicknametxt.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        Nicknametxt.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }

    }

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().SetBool("Is_Running", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (PV.IsMine)
        {
            is_running = false;
            if (Input.GetKey(KeyCode.W))
            {
                is_running = true;
                transform.rotation = Quaternion.Euler(0, 305, 0);
                transform.position += new Vector3(-1, 0, 1f) * Time.deltaTime * Moving_Speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                is_running = true;
                transform.rotation = Quaternion.Euler(0, 215, 0);
                transform.position += new Vector3(-1f, 0, -1f) * Time.deltaTime * Moving_Speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                is_running = true;
                transform.rotation = Quaternion.Euler(0, 135, 0);
                transform.position += new Vector3(1f, 0, -1f) * Time.deltaTime * Moving_Speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                is_running = true;
                transform.rotation = Quaternion.Euler(0, 45, 0);
                transform.position += new Vector3(1, 0, 1f) * Time.deltaTime * Moving_Speed;
            }

            if (is_running)
            {
                GetComponent<Animator>().SetBool("Is_Running", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("Is_Running", false);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, curRot, Time.deltaTime * 10);
        }


        Nicknametxt.transform.rotation = Quaternion.Euler(45, -transform.rotation.y - 45, 0);
    }
}
