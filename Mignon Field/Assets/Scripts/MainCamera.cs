using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    public Player Player;
    public Camera Camera;
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
        //LayerMask mask = LayerMask.GetMask("Buildings");
        int mask = 1 << 9;
        if (Physics.Raycast(transform.position, transform.forward, out hit, mask)){
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name); 
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 15.0f, mask);

        for(int i=0;i<hits.Length;i++){
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if(rend){
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
        }


    }
}
