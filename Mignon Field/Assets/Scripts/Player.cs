using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{

    public float Moving_Speed;
    private bool is_running = false;
    public PhotonView PV;
    public Text Nicknametxt;
    public GameObject bullet;
    public int HP;
    public Gun gun;


    Vector3 curPos;
    Quaternion curRot;
    string str_HP;

    void Awake()
    {
        Nicknametxt.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        Nicknametxt.color = PV.IsMine ? Color.green : Color.red;
        transform.Find("Minimap_Marker").GetComponent<MeshRenderer>().material.color = PV.IsMine ? Color.green : Color.red;
        if (PV.IsMine)
        {
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
        HP = 100;
        GetComponent<Animator>().SetBool("Is_Running", false);
        str_HP = "HP " + HP.ToString();
        GameObject.Find("Canvas").transform.Find("UI").transform.Find("HP").transform.GetComponent<Text>().text = str_HP;
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

    public void Hit()
    {
        HP -= 30;
        if (HP < 0)
        {
            Debug.Log("dsad");
            GameObject.Find("Canvas").transform.Find("Respawn Panel").gameObject.SetActive(true);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
        str_HP = "HP " + HP.ToString();
        GameObject.Find("Canvas").transform.Find("UI").transform.Find("HP").transform.GetComponent<Text>().text = str_HP;
    }

    public void Heal(int num)
    {
        HP += num;
        str_HP = "HP " + HP.ToString();
        GameObject.Find("Canvas").transform.Find("UI").transform.Find("HP").transform.GetComponent<Text>().text = str_HP;
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
}
