using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (Player.PV.IsMine)
            {
                transform.gameObject.SetActive(true);
            }
        }
        catch
        {

        }
    }


    // Update is called once per frame
    void Update() 
    {
        // 카메라 위치를 Player의 일정거리만큼 떨어진 곳으로 고정
        try
        {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position + new Vector3(8, 12, -8), Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(45, Player.transform.rotation.y - 45, 0), Time.deltaTime * 10);
        }
        catch
        {
        }
    }
}
