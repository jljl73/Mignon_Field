using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int gun_type = 0;
    float delay;
    float shooting_delay;
    public int m_remain_bullet;
    // Start is called before the first frame update
    void Start()
    {
        delay = 10f;
        shooting_delay = 0.1f;
        m_remain_bullet = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<Player>().PV.IsMine)
        {
            GameObject.Find("Canvas").transform.Find("UI").transform.Find("B").transform.GetComponent<Text>().text = m_remain_bullet.ToString();
            delay += Time.deltaTime;
            if (Input.GetMouseButton(1))
            {
                if (delay > shooting_delay && m_remain_bullet > 0)
                {
                    delay = 0f;
                    Shoot(gun_type);
                    m_remain_bullet--;
                }
            }
        }
    }

    void Shoot(int t)
    {
        //머신건
        if(t == 0 || t == 1)
        {
            PhotonNetwork.Instantiate("Prefabs/bullet", transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
        else if(t == 2)//샷건
        {
            PhotonNetwork.Instantiate("Prefabs/bullet",
                transform.position + new Vector3(0, 0.5f, 0),
                transform.rotation * Quaternion.Euler(new Vector3(0, Random.Range(-7f, 7f), 0)));
            PhotonNetwork.Instantiate("Prefabs/bullet",
                transform.position + new Vector3(0, 0.5f, 0),
                transform.rotation * Quaternion.Euler(new Vector3(0, Random.Range(-7f, 7f), 0)));
            PhotonNetwork.Instantiate("Prefabs/bullet",
                transform.position + new Vector3(0, 0.5f, 0),
                transform.rotation * Quaternion.Euler(new Vector3(0, Random.Range(-7f, 7f), 0)));
            PhotonNetwork.Instantiate("Prefabs/bullet",
                transform.position + new Vector3(0, 0.5f, 0),
                transform.rotation * Quaternion.Euler(new Vector3(0, Random.Range(-7f, 7f), 0)));
            PhotonNetwork.Instantiate("Prefabs/bullet", transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
    }

}
