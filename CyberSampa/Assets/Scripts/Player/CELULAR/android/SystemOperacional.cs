using Assets.Scripts.Player.CELULAR.android;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemOperacional : MonoBehaviour
{

    public static SystemOperacional instance; //explicado acima

    [Header("PointerTransform")]//posições Player
              // camera (pos1,pos2,pos3,pos4)
              //(posições do sprite com imagem de camera)
    public Transform playerPos, pos1, pos2, pos3, pos4;

    private int interaction = 0;
    public GameObject[] TriggerObjAction;
    /// <summary>
    ///  LEMBRANDO
    ///  
    /// TriggerObjAction[é a lista dos gameobject q possue o trigger
    /// dentro dele  e a luz]
    /// 
    /// de forma para tirar o trigger e desativar a camera de modo facil
    /// 
    /// os botões pareçe q desativa somente a luz , más lembre-se
    /// desativa o trigger q é a visão da camera, para der acesso dessa visão
    /// é facil  vá no GRID/TILEMAP_BASE/POS_CAM/POS_1/ CAM/GAMEOBJECT(<- É O TRIGGER
    /// VEJA TAMBEM O POS_2 EM SEGUIDA Q MOSTRA A VISÃO COM TODAS AS CAMERAS
    /// 
    /// 
    /// </summary>
    private bool active; //para ativar ou desativar a camera

    private void Awake()
    {
        instance = this;//explicado acima sobre o instance
    }
    private void Start()
    {
        instance.enabled = false;
    }
    public void Btn1()
    {
        //Puxando minha camera(cinemanchine_config)
        //que o script(PhoneOpenBack.cs_Possui)
        PhoneOpenBack.instance.cam.Follow = pos1;
        interaction = 1;                     //pos minha posição (script cam)
    }

    public void Btn2()
    {
        //Puxando minha camera(cinemanchine_config)
        //que o script(PhoneOpenBack.cs_Possui)
        PhoneOpenBack.instance.cam.Follow = pos2;
        interaction = 2;                     //pos minha posição (script cam)
    }
    public void Btn3()
    {
        //Puxando minha camera(cinemanchine_config)
        //que o script(PhoneOpenBack.cs_Possui)
        PhoneOpenBack.instance.cam.Follow = pos3;
        interaction = 3;                     //pos minha posição (script cam)
    }
    public void Btn4()
    {
        //Puxando minha camera(cinemanchine_config)
        //que o script(PhoneOpenBack.cs_Possui)
        PhoneOpenBack.instance.cam.Follow = pos4;
                                        //pos minha posição (script cam)
        interaction = 4;
    }

    public void BtnHacking()
    {
            //o objeto da luz é uma lista obj[i]
            // o codigo fiz apenas para aceitar 4 obj da lista
            // se por mais ou menos tem q melhorar os interaction
            // if e else abaixo td em um for(para o obj[Lenght]<-lista_

        //com o botão escolhido de cameras(desative as luzes)
    
        if(interaction == 1)
        {

            //desative o atual no caso 0
            TriggerObjAction[0].SetActive(false); //desative

            TriggerObjAction[1].SetActive(true);
            TriggerObjAction[2].SetActive(true);
            TriggerObjAction[3].SetActive(true);
        }
        else if(interaction == 2)
        { 
            //desative o atual no caso 1
            TriggerObjAction[1].SetActive(false);

            TriggerObjAction[0].SetActive(true);
            TriggerObjAction[2].SetActive(true);
            TriggerObjAction[3].SetActive(true);
        }
        else if(interaction == 3)
        {

            //desative o atual no caso 2
            TriggerObjAction[2].SetActive(false);

            TriggerObjAction[0].SetActive(true);
            TriggerObjAction[1].SetActive(true);
            TriggerObjAction[3].SetActive(true);
        }
        else if(interaction == 4)
        {
            //desative o atual no caso 3
            TriggerObjAction[3].SetActive(false);

            TriggerObjAction[0].SetActive(true);
            TriggerObjAction[1].SetActive(true);
            TriggerObjAction[2].SetActive(true);
        }
    }
    
}
