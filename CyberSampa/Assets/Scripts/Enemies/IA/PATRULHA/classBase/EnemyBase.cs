using System.Collections;
using UnityEngine;

namespace Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA
{
    public class EnemyBase : MonoBehaviour
    {
        [Header("JUMP_FORCE")]
        public int jumpForce;
        bool jumping;
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
        protected Rigidbody2D rbd2;

        public void Start()
        {
            menuOBJ.SetActive(false);
        }
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            rbd2 = GetComponent<Rigidbody2D>();
            direction = (int)transform.localScale.x;

        }
        protected virtual void Update()
        {
           rayPointWall.LookAt(Player.instance.gameObject.transform);
        }
        protected virtual void FixedUpdate()
        {
            Pulo(jumping);
        }

        protected virtual void Flip()
        {
            direction = (int)(-Mathf.Sign(rbd2.velocity.x));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("TriggerJumpEnemy"))
            {
                Pulo(true);
            }
        }

        public void Pulo(bool j)
        {                                      //PULO DO PLAYER
            if (j)
            {
                rbd2.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
              
                jumping = false;
            }
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