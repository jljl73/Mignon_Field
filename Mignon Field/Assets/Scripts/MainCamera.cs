using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 위치를 Player의 2, 3, -2 만큼 떨어진 곳으로 고정
        transform.position = Player.transform.position + new Vector3(2, 3, -2);
    }
}
