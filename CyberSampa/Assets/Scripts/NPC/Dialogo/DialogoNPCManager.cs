using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._CORE_.NPC.Dialogo
{
    public class DialogoNPCManager : MonoBehaviour {

        public Text nameText;
        public Text dialogoText;

        public Animator animText;
        public static DialogoNPCManager instance;
        public Queue<string> sentences;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            sentences = new Queue<string>();
           
        }

        public void StartDialogo(DialogoNPC dialogo)
        {
           

            animText.SetBool("IsOpen", true);
            nameText.text = dialogo.name;

            sentences.Clear();

            foreach(string sentence in dialogo.sentences)
            {
                sentences.Enqueue(sentence);
   
            }
            DisplayNextSentence();
        }    
        public void DisplayNextSentence()
        {
            if(sentences.Count == 0)
            {
                EndDialogo();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        IEnumerator TypeSentence(string sentence)
        {
            dialogoText.text = "";

            foreach(char letter in sentence.ToCharArray())
            {
                dialogoText.text += letter;
                yield return null;
          
            }
        }
        void EndDialogo()
        {
            FindObjectOfType<PlayerPlatformerController>().enabled = true;
            animText.SetBool("IsOpen", false);
            gameObject.SetActive(false);
        }
    }
}