using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vanderson(13/04) - código até o momento INATIVO, desmembrado o código entre "AcoesJogador" e "MovimentoJogador"
//Vanderson(12/04) - alterado ordenamento das variáveis (Serializadas, privadas, publicas) e diferenciado o seu nome, no caso, colocado as públicas em CapsLock para saber a diferenciação.
public class Player : MonoBehaviour
{
    [SerializeField] // para aparecer no inspector do player
    private float velocidade = 4; // defini a velocidade do player 
    [SerializeField]
    private float forcapulo = 1;  // defini a força do pulo do player
    [SerializeField]
    private bool grounded; //variavel de controle do pulo (condição para pular)
    
    private bool jumping;   //acessa o pulo do jogador
    private Rigidbody2D rb2D; //criação de variável de manipulação do rigidbody do player
    private bool eLadoDireito; //parametro para identificar o lado q o player está virado para o pivotamento de sprite
    private Animator animator; //criação de variavel de manipulaçao do animator
    private float horizontal;  //variavel para controlar player 1 Eixo X.

    public Transform GroundCheck; //definida pela posição do GameObject "GroundCheck" do Player,
    public Transform AxeAttack;   //definida pela posição do GameObject "AxeAttack" do Player
    public float RaioPulo;        //define o raio de ação do CheckGound do Player para o pulo
    
    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();         //Coleta os componentes do player
        animator = GetComponent<Animator>();        // --    
    }
    void Start() {
        eLadoDireito = transform.localScale.x > 0; // --
    }
    void Update(){
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        grounded = Physics2D.OverlapCircle(GroundCheck.position, RaioPulo, 1 << LayerMask.NameToLayer("Ground"));
        if(Input.GetKeyDown(KeyCode.W) && grounded) 
        {
            jumping = true;
        }
    }
    void FixedUpdate(){ //CONTROLES (asdw - esquerda,abaixar,direita,pulo)
        horizontal = Input.GetAxis("Horizontal");   //coloca na variavel o valor o eixo Horizontal (config A e B na Unity)
        Movimentar(horizontal);                     //funçao de deslocamento que recebe o valor do eixo X (-1~1)
        MudarDirecao(horizontal);                   //função de direção que recebe o valor do eixo X (-1~1)
        Pulo(jumping);
        Animacao(rb2D.velocity.x);
    }
    //******************************FUNÇÕES PARA OS CONTROLES***************************************************
    private void Pulo(bool j){                                      //PULO DO PLAYER
        if(j)
        {                                
            rb2D.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);   
            
            jumping = false;
        }
    }
    private void Animacao(float h) //CONTROLE DAS ANIMAÇÕES DO PLAYER
    {                                
        if(!grounded) animator.SetBool("pulando", true);            //Se não estiver no chão anima o pulo
        if(grounded) animator.SetBool("pulando", false);            // --- situação oposta ...
        animator.SetFloat("velocidade",Mathf.Abs(h));               //Se tiver velocidade abs > 0 anima corrida
    }
    private void Movimentar(float h)//MOVIMENTAÇÃO LATERAL DO PLAYER
    {                               
        rb2D.velocity = new Vector2(h*velocidade, rb2D.velocity.y); //parametro velocidade do rb2D = (eixo X * velocidade, mantem eixo y)              
    }

    private void MudarDirecao (float horizontal) //VIRADA DE SPRITE DO PLAYER
    {                              
        if(horizontal > 0 && !eLadoDireito || horizontal < 0 && eLadoDireito) { // se (X > 0 && sprite pra esquerda < 0 OU X < 0 && sprite pra direita)
            eLadoDireito = !eLadoDireito;                                       // inverte o argumento e o sprite
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);// --
        }
    }
    //******************************************TESTE DE COLISOES**********************************************************
    private void OnCollisionEnter2D(Collision2D collision2D) {           //Detecta se colidiu
        Debug.Log("COLIDIU com " + collision2D.gameObject.tag);    
    }
    private void OnCollisionExit2D(Collision2D collision2D) {           // Detecta se parou de colidir
        Debug.Log("PAROU DE COLIDIR com " + collision2D.gameObject.tag);    
    }
    void OnDrawGizmos() {                                               //desenha a esfera de detecção do chão para o pulo, apenas para visualização
        Gizmos.DrawWireSphere(GroundCheck.position, RaioPulo);  
    }
}
