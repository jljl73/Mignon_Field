using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int item_type = 0;
    public int num = 50;
    // 총알, 체력, 총
    void Start()
    {
        switch (item_type)
        {
            case 0:
                transform.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 1:
                transform.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                transform.GetComponent<MeshRenderer>().material.color = Color.black;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch (item_type)
            {
                case 0:
                    other.GetComponent<Player>().gun.m_remain_bullet += num;
                    break;
                case 1:
                    other.GetComponent<Player>().Heal(num);
                    break;
                case 2:
                    other.GetComponent<Player>().gun.gun_type = num;
                    break;
            }
            Destroy(gameObject);
        }

    }
}
