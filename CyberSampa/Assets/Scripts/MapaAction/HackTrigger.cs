using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackTrigger : MonoBehaviour
{


    public Transform pointObjInteraction;
    public GameObject HackMenu, InteracaoMenu;

    private void Start()
    {
        HackMenu.SetActive(false);
        InteracaoMenu.SetActive(false);

        InteracaoMenu.transform.position = pointObjInteraction.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        { 
            InteracaoMenu.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HackMenu.SetActive(true);
            }
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InteracaoMenu.SetActive(false);
        }
    }
}