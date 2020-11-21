using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public float m_bulletspeed = 8.0f;
    Vector3 curPos;
    Quaternion curRot;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * m_bulletspeed, ForceMode.Impulse);
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {   
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else
    //    {
    //        curPos = (Vector3)stream.ReceiveNext();
    //        curRot = (Quaternion)stream.ReceiveNext();
    //    }

    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
        // 총 맞은 게 플레이어고 주인이 아니면
        else if (PV.Owner != other.GetComponent<Player>().PV.Owner)
        {
            if(other.GetComponent<Player>().PV.IsMine)
                other.GetComponent<Player>().Hit();
            Destroy(gameObject);
        }
    }
}
