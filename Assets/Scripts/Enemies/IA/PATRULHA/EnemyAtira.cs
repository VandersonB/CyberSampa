using System.Collections;
using UnityEngine;
using Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA;

public class EnemyAtira : EnemyBase
{

    [Header("TIRO")]
    public GameObject bulletPrefab;
    public int weaponSpeed;
    public float lenghtBullet;          //olhosInimigo

    // RAYCAST VAR        
    public Transform rayPointBullet;     //visualiza bala_tiro
    public RaycastHit2D hitToBullet;       //bala_Tiro Collider
                                           //private bool idle;
    protected override void Awake()
    {
        base.Awake();
        menuOBJ.SetActive(false);
    }
    protected override void Update()
    {
        //RAYcast_active(chão,parede(collider))
        if (!RaycastGround().collider || RaycastWall().collider)
        {
            Flip();
        }

        //Raycast_active(INIMIGO_SEGUE__1-PLAYER CHEGA PERTO EM UMA DETERMINADA)    
        if (RaycastToPlayer().collider && RaycastToPlayer().distance <= 4)
        {
            speed = 0;                      // deixe o inimigo imóvel
                                            //animator.SetTrigger("Fire");    // ativando animação tiro
                                            //2-INIMIGO atira

            if (RaycastTiro().collider.tag == "Player")
            {

                //menuOBJ.SetActive(true);

                //3- bala atinge player
                //if (RaycastToPlayer().distance <= 0.6f)
                //{/
                //4- PLAYER CAPITURADO , APAREÇE TELA DE CONTINUAR A JOGAR

                ///menuOBJ.SetActive(true);
                //5- PLAYER RETORNA AO INICIO
                //}
            }
        }
        else
        {
            speed = 2;
        }
    }
    private void FixedUpdate()
    {
        Movement();
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
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        //idle = false;
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


    protected virtual RaycastHit2D RaycastTiro() //EmDesenvolvimento
    {
        ///            //send out desired raycast and record the result
        hitToBullet = Physics2D.Raycast(rayPointBullet.position, Vector2.right * direction, lenghtBullet, layerPlayer);

        //.... DETERMINE THE COLOR BASED ON IF THE RAYCAST HIT..
        Color color = hitToBullet ? Color.red : Color.green;

        //.....and draw the reg in the scene view
        Debug.DrawRay(rayPointBullet.position, Vector2.right * lenghtBullet, color);


        //Instantiate(duplicando minha bala no ponto rayPointBullet
        GameObject goWeapon = (GameObject)Instantiate(bulletPrefab, rayPointBullet.position, Quaternion.identity);
        //Set_NEW OBJ_ROTATE

        //FLIP_ICON_BALA_DIREÇÃO_DO_OLHAR_DO_INIMIGO]
        if(direction == -1)
        {
            goWeapon.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            goWeapon.GetComponent<SpriteRenderer>().flipX = false;
        //

        //TIRO NA DIREÇÃO DO OLHAR DO INIMIGO
        goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction, ForceMode2D.Impulse);

        /////////////////////////////////
        //return the results of the raycast
        return hitToBullet;
    }
    #endregion
}