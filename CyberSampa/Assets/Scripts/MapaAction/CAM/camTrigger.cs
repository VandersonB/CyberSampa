using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public static CamTrigger instance;
    public AudioSource alarmeAudio;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        alarmeAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
    }
}
