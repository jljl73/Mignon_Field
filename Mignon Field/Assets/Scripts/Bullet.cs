using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public float m_bulletspeed;
    Vector3 curPos;
    Quaternion curRot;

    void Start()
    {
        m_bulletspeed = 30.0f;
        GetComponent<Rigidbody>().AddForce(transform.forward * m_bulletspeed, ForceMode.Impulse);
        PV.RPC("DestroytimeRPC", RpcTarget.AllBuffered, 5f);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag != "Player")
        {
            if(other.gameObject.tag != "Bullet")
                PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
        // 총 맞은 게 플레이어고 주인이 아니면
        else if (PV.Owner != other.GetComponent<Player>().PV.Owner)
        {
            if (other.GetComponent<Player>().PV.IsMine)
            {
                other.GetComponent<Player>().Hit();
                PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
            }
        }
    }
    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);
    [PunRPC]
    void DestroytimeRPC(float t) => Destroy(gameObject, t);
}
