using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsObject : MonoBehaviour
{
    
    [SerializeField]
    private const float tempoMaxDash = 1.0f; // define o tempo máximo de Dash
    [SerializeField]
    private float distanciaDash = 10;//determina a distância maxima de Dash
    [SerializeField]
    private float velocidadeAcrescimoVelocidade = 0.1f;//determimna o acrescimo de Dash

    public float tempoAtualDash = tempoMaxDash;
    public float minGroundNormalY = .65f; //Controla a altura mínima para considerar o player no "chão"
    public float gravityModifier = 1f; //modificador de gravidade, impactando no andar e no pulo;

    private Vector2 moveDash;

    protected Vector2 targetVelocity; //vetor de velocidade
    protected bool grounded; //verifica se o player está no chão.
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;



    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>(); //ativa o RigidBody quando o GO está ativo.
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeInputs();
    }

    protected virtual void ComputeInputs() //sem função nesse código. No outro scrip, é ele que fica lendo os inputs.
    {

    }

    void FixedUpdate()
    {
       
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime; //vetor de velocidade, considerando a gravidade e o modificador.
        velocity.x = targetVelocity.x; 
        grounded = false; //a variável é verificada todo frame. Por conta disso, ela é reiniciada antes de ser executada nos métodos;

        Vector2 deltaPosition = velocity * Time.deltaTime; //recebe o vetor posição baseado na velocidade calculada no inicio do Fixed Update
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Dash(moveAlongGround); //executa o método Dash, independente do botão ser pressionado

        Vector2 move = moveDash * deltaPosition.x; //calcula o vetor x, contanto ou não com o Dash;
        Movement(move, false); //executa o movimento em X
        move = Vector2.up * deltaPosition.y; //calcula o vetor de movimento em Y, de acordo com input do jogador;
        Movement(move, true);//executa o movimento em Y
        
        
    }

    void Movement(Vector2 move, bool yMovement) //método que executa recebe os inputs e executa o movimento.
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius); //retorna o inteiro correspondente aos colliders da cena em compara~ção ao RigidBody do GO
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)//cria uma lista a partir dessa contagem.
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;
                    if (yMovement) //se o movimento for de pulo.
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance; //executa o movimento do player
    }

    Vector2 Dash(Vector2 move) //precisa adequar a animação;
    {
      
        if (tempoAtualDash < tempoMaxDash)
        {
            moveDash += move* distanciaDash;
            tempoAtualDash += velocidadeAcrescimoVelocidade;
        }

        else
        {
            moveDash = move;
        }
        return moveDash;
    }
}