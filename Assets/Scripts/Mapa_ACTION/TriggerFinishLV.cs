using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinishLV: MonoBehaviour
{
    //SE O PLAYER IR NO TRIGGER ATIVA UMA NOVA SCENA ...
    //  _ NO CASO UM PROXIMO LEVEL
    public GameObject PlayerObj;
    public string scenaName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(scenaName);
            DontDestroyOnLoad(PlayerObj);
        }
    }
}
