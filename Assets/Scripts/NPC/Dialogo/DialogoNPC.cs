using Assets._CORE_.NPC.Dialogo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogoNPC : MonoBehaviour
{
   // public string name;
    [TextArea(3,10)]
    public string[] sentences;
    public static DialogoNPC instance;

    private void Awake()
    {
        instance = this;
    }


}
