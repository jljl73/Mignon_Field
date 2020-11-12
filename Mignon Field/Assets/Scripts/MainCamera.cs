using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.PV.IsMine)
        {
            transform.gameObject.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 카메라 위치를 Player의 2, 3, -2 만큼 떨어진 곳으로 고정
        transform.position = Player.transform.position + new Vector3(3, 5, -3);
        transform.rotation = Quaternion.Euler(45, -transform.parent.rotation.y - 45, 0);
    }
}
