using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public GameObject PlayerOBJ;
    public GameObject[] InimigoOBJ;
    [SerializeField] private GameObject InteractionOBJ;
    [SerializeField] private Transform Pointer;

    private void Start()
    {
        InteractionOBJ.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            InteractionOBJ.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                PlayerOBJ.transform.position = Pointer.transform.position;
                InteractionOBJ.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteractionOBJ.SetActive(false);
        }
        if (collision.gameObject.tag == "Inimigo")
        {
            for (int i = 0; i < InimigoOBJ.Length; i++)
            {
                InimigoOBJ[i].transform.position = Pointer.transform.position;
            }
        }
    }
}
