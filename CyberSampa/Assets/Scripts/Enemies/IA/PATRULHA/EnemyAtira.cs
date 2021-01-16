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

    }

    protected override void Update()
    {
        //RAYcast_active(chão,parede(collider))
        if (!RaycastGround().collider || RaycastWall().collider)
        {
            Flip();
        }
        if (RaycastToPlayer().collider && RaycastToPlayer().distance <= 3.5f)
        {
            speed = 0;
            RaycastTiro();
            /* não terminado
            if (RaycastTiro().collider.tag == "Player") {
                StartCoroutine(TempMenuOpen());
            
            }*/
        }
        else
            speed = 2;
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
        //Instantiate(duplicando minha bala no ponto rayPointBullet

        if (speed == 0)
        {
            GameObject goWeapon = (GameObject)Instantiate(bulletPrefab, rayPointBullet.position, Quaternion.identity);
            //Add força de tiro
            goWeapon.GetComponent<Rigidbody2D>().AddForce(Vector3.right * direction * weaponSpeed);
            if (hitToBullet.collider)
            {
                Destroy(goWeapon);
            }
            //___set direction_correspondente áo lado o inimigo
            if (direction == -1)
            {
                goWeapon.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
                goWeapon.GetComponent<SpriteRenderer>().flipX = false;
        }

        //send out desired raycast and record the result
        hitToBullet = Physics2D.Raycast(rayPointBullet.position, Vector2.right * direction, lenghtBullet, layerPlayer);

  
        //.... DETERMINE THE COLOR BASED ON IF THE RAYCAST HIT..
        Color color = hitToBullet ? Color.red : Color.green;

        //.....and draw the reg in the scene view
        Debug.DrawRay(rayPointBullet.position, Vector2.right * lenghtBullet, color);

        //return the results of the raycast
        return hitToBullet;
    }
    #endregion
}