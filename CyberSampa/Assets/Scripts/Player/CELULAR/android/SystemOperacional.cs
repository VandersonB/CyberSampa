using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemOperacional : MonoBehaviour
{
    public static SystemOperacional instance;
    public GameObject obj;
    public CinemachineVirtualCamera cam;
    [Header("PointerTransform")]
    public Transform playerPos, pos1, pos2, pos3, pos4;
    private int interaction = 0;
    public GameObject[] TriggerObjAction;
    private bool active;

    [Header("Escolha botão menu Phone")]
    public string code;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        obj.SetActive(false);
        instance.enabled = false;
    }
    void Update()
    {
        if (ProgressHackManager.instance.ActivePhone == 1)
        {
            if (Input.GetButton(code))
            {

                if (active == false)
                {
                    Time.timeScale = 1;
                    cam.Follow = playerPos;
                    obj.SetActive(true);
                    active = true;
                }
                else
                {
                    obj.SetActive(false);
                    Time.timeScale = 1;
                    active = false;
                }

            }
        }
    }
    public void Btn1()
    {
        cam.Follow = pos1;
        interaction = 1;
    }

    public void Btn2()
    {
        cam.Follow = pos2;
        interaction = 2;
    }
    public void Btn3()
    {
        cam.Follow = pos3;
        interaction = 3;
    }
    public void Btn4()
    {
        cam.Follow = pos4;
        interaction = 4;
    }

    public void BtnHacking()
    {
 
        if(interaction == 1)
        {
            TriggerObjAction[0].SetActive(false);

            TriggerObjAction[1].SetActive(true);
            TriggerObjAction[2].SetActive(true);
            TriggerObjAction[3].SetActive(true);
        }
        else if(interaction == 2)
        {
            TriggerObjAction[1].SetActive(false);

            TriggerObjAction[0].SetActive(true);
            TriggerObjAction[2].SetActive(true);
            TriggerObjAction[3].SetActive(true);
        }
        else if(interaction == 3)
        {
            TriggerObjAction[2].SetActive(false);

            TriggerObjAction[0].SetActive(true);
            TriggerObjAction[1].SetActive(true);
            TriggerObjAction[3].SetActive(true);
        }
        else if(interaction == 4)
        {
            TriggerObjAction[3].SetActive(false);

            TriggerObjAction[0].SetActive(true);
            TriggerObjAction[1].SetActive(true);
            TriggerObjAction[2].SetActive(true);
        }
    }

}
