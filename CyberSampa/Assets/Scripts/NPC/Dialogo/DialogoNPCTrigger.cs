using System.Collections;
using UnityEngine;

namespace Assets._CORE_.NPC.Dialogo
{
    public class DialogoNPCTrigger : MonoBehaviour
    {
        public GameObject MenuNPC;

        private void Start()
        {
            MenuNPC.SetActive(false);
        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {

                    MenuNPC.SetActive(true);

                FindObjectOfType<PlayerPlatformerController>().enabled = false;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "Player")
            {
                if (MenuNPC.activeSelf == true)
                {
                    FindObjectOfType<DialogoNPCManager>().StartDialogo(DialogoNPC.instance);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                MenuNPC.SetActive(false);
            }
        }
    }
}