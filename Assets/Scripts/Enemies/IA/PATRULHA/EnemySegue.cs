using Assets._CORE_.IA_INIMIGO.Script.IA.PATRULHA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySegue : EnemyBase
{
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
        //Raycast_active(INIMIGO_SEGUE__1-PLAYER CHEGA PERTO EM UMA DETERMINADA)    
        if (RaycastToPlayer().collider && RaycastToPlayer().distance <= 3)
        {
            //INCOMPLETO , AQUIE É O  OBJETIVO DO INIMIGO Q SEGUE

            //2-INIMIGO CORRE(OK)
            speed = 4;      // VELOCIDADE INIMIGO AUMENTADA

            //3- INIMIGO ATIVA A ANIMAÇÃO SUBIMISSÃO NO PLAYER
            if (RaycastToPlayer().distance <= 0.6f) {
                //4- PLAYER CAPITURADO , APAREÇE TELA DE CONTINUAR A JOGAR
                menuOBJ.SetActive(true);
                //5- PLAYER RETORNA AO INICIO
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
        else if(direction == -1)
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
}
