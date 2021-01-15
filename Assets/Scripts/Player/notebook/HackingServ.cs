using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingServ : MonoBehaviour
{
    public static HackingServ instance;
    public Transform rightHandAnim;
    public GameObject rightHand, leftHandPhone;
    public GameObject HomePC, HomePCHacking;
    public int interaction = 0;
    public void Awake()
    {
        instance = this;
        rightHandAnim.gameObject.transform.Find("hand_R_IMG");
        
        HomePC.SetActive(true);
        HomePCHacking.SetActive(false);
        leftHandPhone.SetActive(false);
        rightHand.SetActive(true);

        interaction = 1;

    }

    private void LateUpdate()
    {
        
        if(interaction == 1)
        {

            //  1-Plugando USB NA PORTA DO NOTBOOK
            rightHandAnim.GetComponent<Animator>().SetBool("Plugg", true);
            // 
            leftHandPhone.SetActive(true);
        }
    }

    public void BtnActionHacking()
    {
        //tela notebook hacking
        HomePC.SetActive(true);
        HomePCHacking.SetActive(true);
       
    }
}