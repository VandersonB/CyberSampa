using System.Collections;
using UnityEngine;
using Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA;
public class EnemySegue : EnemyBase
{
    public static EnemySegue instance;
    private bool jumping;   //acessa o pulo do jogador
    public float RaioPulo;        //define o raio de ação do CheckGound do Player para o pulo

    [SerializeField]
    private float forcapulo = 1;  // defini a força do pulo do player

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Update()
    {
        //RAYcast_active(chão,parede(collider))

        if (!RaycastGround().collider && !RaycastWall().collider)
        { 
            Flip();
        }
      
    }
    private void FixedUpdate()
    {
        Movement();
        Pulo(jumping);
    }
    private void LateUpdate()
    {
        /*
            if (direction == 0)
            {
                animator.SetBool("idle", idle);
            }
            else*/
        if (direction == 1)
        {
            animator.SetFloat("Horizontal", 1);
        }
        else if (direction == -1)
        {
            animator.SetFloat("Horizontal", -1);
        }
    }
    private void Movement()
    {
        float horizontalVelocity = speed;
        horizontalVelocity = horizontalVelocity * direction;
        rbd2.velocity = new Vector2(horizontalVelocity, rbd2.velocity.y);
        //idle = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "actionPulo")
        {
            rbd2.AddForce(Vector2.up * 600f); 
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
    private void Pulo(bool j)
    {                                      //PULO DO PLAYER
        if (j)
        {
            rbd2.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);

            jumping = false;
        }
    }

    #region RAYCAST_




    #endregion
}