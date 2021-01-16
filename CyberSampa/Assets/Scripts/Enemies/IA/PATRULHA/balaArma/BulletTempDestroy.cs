using Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTempDestroy : MonoBehaviour
{
    public GameObject prefabEffectTiro;

    private void Start()
    {
        prefabEffectTiro.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            FindObjectOfType<EnemyAtira>().menuOBJ.SetActive(true);
        }
        else
        {
            prefabEffectTiro.SetActive(true);
            Destroy(gameObject);
        }
    }
}
