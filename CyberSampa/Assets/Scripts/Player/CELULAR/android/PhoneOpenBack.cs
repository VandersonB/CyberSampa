using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.CELULAR.android
{
    public class PhoneOpenBack : MonoBehaviour
    {
        // instance(um static da própria classe
        // colocando o instance = this no Awake()
        //nos permite acessar tudo q tem neste codigo em 
        //qualquer outro com o comando abaixo
        /// <summary>
        /// 
        /// ---> Class.instance.cam <-----
        /// 
        /// coloque esse codigo em qualquer outro script
        /// que irá reconhecer imediatamente a camera que 
        /// esta nesse codigo ou qualquer item
        /// 
        /// exemplo : PhoneOpenBack.instance.cam;
        /// 
        /// 
        /// </summary>
        public static PhoneOpenBack instance;
        [Header("Config")]
        public Transform playerPos; //posição player(set_to_cinemanchine)
        bool active; // bool (true e false, para o menu abrir = true, e fecha = false
        public CinemachineVirtualCamera cam; // minha config CinemanchineVirtualCam

        //instance= só funciona no awake
        private void Awake()
        {
            instance = this;//reconhecimento de todos os public deste codigo

        }
        private void Start()
        {
            gameObject.SetActive(false); //deixe inicialmente desativado o menu
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.P)) // aperte o P(PARA ABRIR E FECHAR
            {
                if (active == false)
                {
                  
                    cam.Follow = playerPos;     //pegando minha camera cinemanchine.Follow(e colocando o player no target)
                    gameObject.SetActive(true); // ative o menu
                    active = true;              //menu ativado active = ativado
                }
                else
                {
                    gameObject.SetActive(false);     //menu desativado
                  
                    active = false;                  //menu fecha
                }

            }
        }
    }
}