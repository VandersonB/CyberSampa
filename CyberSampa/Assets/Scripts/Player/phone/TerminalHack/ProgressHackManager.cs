
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressHackManager : MonoBehaviour
{
	public static ProgressHackManager instance;
	public int ActivePhone = 0;
	public Text nameText;
	public Text dialogueText;
	public Animator animator;

	private Queue<string> sentences;

    private void Awake()
    {
		instance = this;
    }
	// Use this for initialization
	void Start()
	{

		Time.timeScale = 0;//CONGELANDO GAME
		sentences = new Queue<string>();
	}
	public void StartDialogue(DialogueHack dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		ActivePhone = 1;
		//  1-DesPlugando USB NA PORTA DO NOTBOOK	
		HackingServ.instance.interaction = 2;
		HackingServ.instance.rightHandAnim.GetComponent<Animator>().SetBool("Plugg", false);
		//tirando meu celular
		StartCoroutine(TempFecharTudo());

	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		
	}
	IEnumerator TempFecharTudo()
    {
		Time.timeScale = 1;//DESCONGELANDO GAME
		HackingServ.instance.leftHandPhone.SetActive(false);
		HackingServ.instance.rightHand.SetActive(false);
		HackingServ.instance.gameObject.SetActive(false);
		yield return new WaitForSeconds(3);
    }
}