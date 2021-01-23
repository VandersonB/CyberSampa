using System.Collections;
using UnityEngine;


public class EnemySegueV2 : MonoBehaviour
{
    [Header("RAY_CAST_VISOR")]
    public float lenghtEyeVision;          //olhosInimigo
    public Transform rayPointToPlayer;     //visualiza o player
    public RaycastHit2D hitToPlayer;       //player Collider


    [Header("Options")]
    float dirX;

    [SerializeField]
    float moveSpped = 3f;

    Rigidbody2D rb;

    bool facingRight = false;

    Vector3 localScale;

    private void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
    }

    private void Update()
    {
        if (transform.position.x < -9f)
        {
            dirX = 1f;
        }
        else if (transform.position.x > 9f)
        {
            dirX = -1f;
        }
        /* 
        if(RayVisorAoPlayer().collider);
        if (!RayVisorAoPlayer().collider) ;

        Debug.Log("ColliderRAY CAST : " + RayVisorAoPlayer());
        */
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpped, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }
    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0) || (!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
    public RaycastHit2D RayVisorAoPlayer()
    {
        //send out desired raycast and record the result
        hitToPlayer = Physics2D.Raycast(rayPointToPlayer.position, Vector2.right * dirX, lenghtEyeVision,LayerMask.NameToLayer("Player"));

        //.... DETERMINE THE COLOR BASED ON IF THE RAYCAST HIT..
        Color color = hitToPlayer ? Color.green : Color.red;

        //.....and draw the reg in the scene view
        Debug.DrawRay(rayPointToPlayer.position, Vector2.right * lenghtEyeVision, color);

        //return the results of the raycast
        return hitToPlayer;
    }
}
 
