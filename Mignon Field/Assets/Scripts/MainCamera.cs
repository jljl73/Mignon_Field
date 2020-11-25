using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    public Player Player;
    public Camera Camera;
    private List<Renderer> TrRendList = new List<Renderer>();

    private RaycastHit hit;
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

        int mask = 1 << 9; // 9 == Buildings Layer 
        /*if (Physics.Raycast(transform.position, transform.forward, out hit, mask)) { 
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name); 
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }*/ // RayCast 디버깅

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 15.0f, mask);


        for (int i = 0; i < TrRendList.Count; i++) // 이전 프레임에서 반투명했던 오브젝트들이 시야를 가리지 않게 되었다면 원래 쉐이더로 복구
        {
            Renderer PrevHitRend = TrRendList[i];
            bool continous_hit = false;
            for (int j = 0; j < hits.Length; j++)
            {
                if (PrevHitRend == hits[j].transform.GetComponent<Renderer>())
                {
                    continous_hit = true;
                    break;
                }
            }
            if (!continous_hit)
            {
                PrevHitRend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = PrevHitRend.material.color;
                tempColor.a = 1.0F;
                PrevHitRend.material.color = tempColor;
            }
        }

        TrRendList.Clear();

        for (int i = 0; i < hits.Length; i++) { // 메인카메라-플레이어 사이의 Building Object를 반투명하게 만들고 리스트에 담기
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend) {
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
            TrRendList.Add(rend);
        }

        


    }
}
