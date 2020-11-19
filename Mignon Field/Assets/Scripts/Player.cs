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
    public GameObject bullet;
    public int HP = 100;

    Vector3 curPos;
    Quaternion curRot;

    void Awake()
    {
        Nicknametxt.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        Nicknametxt.color = PV.IsMine ? Color.green : Color.red;

        if (PV.IsMine)
        {
            //var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            //CM.Follow = transform;
            //CM.LookAt = transform;
            GameObject.Find("Main Camera").GetComponent<MainCamera>().Player = this;
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
            // 플레이어 이동
            #region
            is_running = false;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                is_running = true;
                transform.position += new Vector3(-1, 0, 1f) * Time.deltaTime * Moving_Speed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                is_running = true;
                transform.position += new Vector3(-1f, 0, -1f) * Time.deltaTime * Moving_Speed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                is_running = true;
                transform.position += new Vector3(1f, 0, -1f) * Time.deltaTime * Moving_Speed;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                is_running = true;
                transform.position += new Vector3(1, 0, 1f) * Time.deltaTime * Moving_Speed;
            }
            #endregion


            // 플레이어 방향
            #region
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float dx = hit.point.x - transform.position.x;
                float dz = hit.point.z - transform.position.z;
                Vector3 dir = new Vector3(dx, 0f, dz);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10);
            }

            #endregion

            // 총
            #region
            if (Input.GetMouseButtonDown(1))
            {
                PhotonNetwork.Instantiate("Prefabs/bullet", transform.position + new Vector3(-0.4f, 0.35f, 0.4f), transform.rotation);
            }

            #endregion


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
