using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Moving_Speed;
    private bool is_running = false;
    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().SetBool("Is_Running", false);
    }

    // Update is called once per frame
    void Update()
    {
        is_running = false;
        if (Input.GetKey(KeyCode.W))
        {
            is_running = true;
            transform.rotation = Quaternion.Euler(0, 305, 0);
            transform.position += new Vector3(-1, 0, 1f) * Time.deltaTime * Moving_Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            is_running = true;
            transform.rotation = Quaternion.Euler(0, 215, 0);
            transform.position += new Vector3(-1f, 0, -1f) * Time.deltaTime * Moving_Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            is_running = true;
            transform.rotation = Quaternion.Euler(0, 135, 0);
            transform.position += new Vector3(1f, 0, -1f) * Time.deltaTime * Moving_Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            is_running = true;
            transform.rotation = Quaternion.Euler(0, 45, 0);
            transform.position += new Vector3(1, 0, 1f) * Time.deltaTime * Moving_Speed;
        }

        if (is_running)
        {
            GetComponent<Animator>().SetBool("Is_Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Is_Running", false);
        }
    }
}
