using System.Collections;
using UnityEngine;

namespace Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA
{
    public class EnemyAtira : MonoBehaviour
    {
        
        [Header("MENU_")]
        public GameObject menuOBJ;
        [Header("Enemy Propriedades")]
        public float speed;
        //[HideInInspector]
        public int direction;

        [Header("TIRO")]
        public GameObject bulletPrefab;
        public int weaponSpeed;
        [Header("RayCast Propriedades")]
        public LayerMask layerGround;          // LAYER QUE RECONHEÇE O chão
        public LayerMask layerPlayer;          // LAYER QUE RECONHEÇE O PLAYER
        public float lenghtGround;             // tamanhoChão
        public float lenghtWall;               //parede
        public float lenghtBullet;          //olhosInimigo

        // RAYCAST VAR        
        public Transform rayPointGround;       //visualiza chão
        public RaycastHit2D hitGround;         //chão Collider

        public Transform rayPointWall;         //visualiza parede
        public RaycastHit2D hitWall;           //parede Collider

        public Transform rayPointBullet;     //visualiza bala_tiro
        public RaycastHit2D hitToBullet;       //bala_Tiro Collider


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

        protected virtual RaycastHit2D RaycastTiro() //EmDesenvolvimento
        {     
            GameObject goWeapon = (GameObject)Instantiate(bulletPrefab, rayPointBullet.position, Quaternion.identity);
          
            //send out desired raycast and record the result
            hitToBullet = Physics2D.Raycast(rayPointBullet.position, Vector2.right * direction, lenghtBullet, layerPlayer);
            goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector3.right * weaponSpeed);
            //.... DETERMINE THE COLOR BASED ON IF THE RAYCAST HIT..
            Color color = hitToBullet ? Color.red : Color.green;

            //.....and draw the reg in the scene view
            Debug.DrawRay(rayPointBullet.position, Vector2.right * lenghtBullet, color);

            //return the results of the raycast
            return hitToBullet;
        }
        #endregion
    }
}