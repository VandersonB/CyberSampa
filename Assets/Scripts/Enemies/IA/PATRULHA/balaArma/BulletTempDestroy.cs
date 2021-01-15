using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTempDestroy : MonoBehaviour
{
    public GameObject anim;
    public GameObject prefabEffectTiro;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("bulletEffect");
        prefabEffectTiro.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            FindObjectOfType<EnemyAtira>().menuOBJ.SetActive(true);
            Destroy(gameObject);
        }
        else if(collision.collider)
        {
            anim.GetComponent<Animator>().SetTrigger("Fire");
            prefabEffectTiro.SetActive(true);
            Destroy(gameObject);
        }
    }
}
