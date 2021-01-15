using System.Collections;
using UnityEngine;

namespace Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA
{
    public class EnemyBase : MonoBehaviour
    {
        [Header("MENU_")]
        public GameObject menuOBJ;
        [Header("Enemy Propriedades")]
        public float speed;
        //[HideInInspector]
        public int direction;

        [Header("RayCast Propriedades")]
        public LayerMask layerGround;          // LAYER QUE RECONHEÇE O chão
        public LayerMask layerPlayer;          // LAYER QUE RECONHEÇE O PLAYER
        public float lenghtGround;             // tamanhoChão
        public float lenghtWall;               //parede
        public float lenghtEyeVision;          //olhosInimigo

        // RAYCAST VAR        
        public Transform rayPointGround;       //visualiza chão
        public RaycastHit2D hitGround;         //chão Collider

        public Transform rayPointWall;         //visualiza parede
        public RaycastHit2D hitWall;           //parede Collider

        public Transform rayPointToPlayer;     //visualiza o player
        public RaycastHit2D hitToPlayer;       //player Collider


        protected Animator animator;
        protected Rigidbody2D rb;

        public void Start()
        {
            menuOBJ.SetActive(false);
        }
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            direction = (int)transform.localScale.x;

        }
        protected virtual void Update()
        {

        }

        protected virtual void Flip()
        {
            direction = (int)(-Mathf.Sign(rb.velocity.x));
            transform.localScale = new Vector2(6, transform.localScale.y);
        }
        /// <RAyCAST>
        ///     [BASE EM TODOS OS INIMIGOS]
        ///     -> RaycastGround() = CHÃO
        ///     -> RaycastWall()   = PAREDE
        ///     
        ///     [ADD NO INIMIGO NORMAL Q APENAS SEGUE]
        ///     -> RaycastToPlayer() = QUANDO VER O JOGADOR O SIGA
        ///     
        ///     [ADD NO INIMIGO COM ARMA QUE ATIRA]
        ///     -> RaycastBulletToPlayer() = QUANDO VER O JOGADOR ATIRE
        /// 
        /// </RAyCAST>

        #region RAYCAST_
        protected virtual RaycastHit2D RaycastGround()
        {
            //send out desired raycast and record the result
            hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lenghtGround, layerGround);
            
            //.... DETERMINE THE COLOR BASED ON IF THE RAYCAST HIT..
            Color color = hitGround ? Color.red : Color.green;
            
            //.....and draw the reg in the scene view
            Debug.DrawRay(rayPointGround.position, Vector2.down * lenghtGround, color);

            //return the results of the raycast
            return hitGround;
        }
        
        protected virtual RaycastHit2D RaycastWall()
        { 

            //envio result raycast
            hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right * direction, lenghtWall, layerGround);

            //cor baseado no meu raycast
            Color color = hitWall ? Color.yellow : Color.blue;

            //DESENHANDO COM A COR
            Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lenghtWall, color);
            
            //return resultado do meu raycast
            return hitWall;
        }

        protected virtual RaycastHit2D RaycastToPlayer()
        {
            //send out desired raycast and record the result
            hitToPlayer = Physics2D.Raycast(rayPointToPlayer.position, Vector2.right * direction, lenghtEyeVision, layerPlayer);
            
            //.... DETERMINE THE COLOR BASED ON IF THE RAYCAST HIT..
            Color color = hitToPlayer ? Color.red : Color.green;
            
            //.....and draw the reg in the scene view
            Debug.DrawRay(rayPointToPlayer.position, Vector2.right * lenghtEyeVision, color);

            //return the results of the raycast
            return hitToPlayer;
        }
        #endregion
    }
}