using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemyPro01 : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Vector3 direction = player.position - transform.position;

        direction.Normalize();
        movement = direction;

        //Animação
        if (player.position.x >= movement.x)
        {
            anim.SetFloat("Horizontal", 2);
        }
        if (player.position.x < movement.x)
        {
            anim.SetFloat("Horizontal", 1);
        }

    }
    private void FixedUpdate()
    {
        moveCharacter(movement);

    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position
            + (direction * moveSpeed * Time.deltaTime));
    }
}
