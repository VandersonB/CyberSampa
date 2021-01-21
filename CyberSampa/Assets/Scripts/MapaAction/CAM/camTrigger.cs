using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTrigger : MonoBehaviour
{
    public AudioSource alarmeAudio;

    private void Start()
    {
        alarmeAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player_Esta-Na_visão_da_camera!");
            alarmeAudio.Play();
        }
    }
}
