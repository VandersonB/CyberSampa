using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackTrigger : MonoBehaviour
{
    public GameObject HackMenu, InteracaoMenu;

    private void Start()
    {
        HackMenu.SetActive(false);
        InteracaoMenu.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteracaoMenu.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                HackMenu.SetActive(true);
            }
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteracaoMenu.SetActive(false);
        }
    }
}