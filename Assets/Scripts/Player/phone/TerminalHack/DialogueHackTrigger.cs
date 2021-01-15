using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHackTrigger : MonoBehaviour
{
    public DialogueHack dialogue;
    //ativando codigo via botão
    public void TriggerDialogue()
    {
        FindObjectOfType<ProgressHackManager>().StartDialogue(dialogue);
    }
}
