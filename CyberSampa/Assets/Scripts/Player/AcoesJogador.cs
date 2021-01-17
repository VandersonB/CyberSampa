using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//biblioteca usada para criar UnityEvents

public class AcoesJogador : MonoBehaviour
{
     
    
    private Rigidbody2D rb2D; //criação de variável de manipulação do rigidbody do player
    private Animator animator; //criação de variavel de manipulaçao do animator
    private AudioSource meuAudioSource;
    private Collider2D colisorPe;
    //private Carta[] item;
    //private Interface interfaceJogador;
    public Vector2 posicaoInicial { get; private set; }
    [SerializeField]
    private Transform groundCheck; //definida pela posição do GameObject "GroundCheck" do Player,
    [SerializeField]
    private AudioClip audioPulo;
    [SerializeField]
    private AudioClip audioMorte;

    [SerializeField]
    private KeyCode pulo;
    [SerializeField]
    private KeyCode pegar;
    [SerializeField]
    private KeyCode cancelar;
    [SerializeField]
    private KeyCode abaixar;
    [SerializeField]
    private KeyCode atacar;

    [SerializeField]
    private UnityEvent aoPressionarAbaixar;
    [SerializeField]
    private UnityEvent aoPressionarPulo;
    [SerializeField]
    private UnityEvent aoPressionarPegar;
    [SerializeField]
    private UnityEvent aoPressionarCancelar;
    [SerializeField]
    private UnityEvent aoPressionarAtaque;
    [SerializeField]
    private float RaioPulo = 0.1f;//define o raio de ação do CheckGound do Player para o pulo
    [SerializeField]
    private float velocidadeMorte;//define a velocidade de queda que acarretará na morte do jogador.
    [SerializeField]
    private float ajusteDeColisorAgaixado;
    [SerializeField]
    private GameObject plataforma;


    [SerializeField] // para aparecer no inspector do player
    private float velocidade = 4; // defini a velocidade do player
    [Range(10, 22)]
    public float forcapulo = 19.7f;  // defini a força do pulo do player
    private float horizontal;  //variavel para controlar player 1 Eixo X.
    private float raioDoItem = 1f;


    
    
    private float velocidadeQueda;
    private bool estaMorto;
    
    private int numeroCarta;
    private bool ataque =false;
    private bool eLadoDireito; //parametro para identificar o lado q o player está virado para o pivotamento de sprite
    private bool temParede;
    private bool grounded; //variavel de controle do pulo (condição para pular)



    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();         //Coleta os componentes do player
        animator = GetComponent<Animator>();        // --
        //interfaceJogador = GameObject.FindObjectOfType<Interface>();//puxa o script de interface, para poder mandar o comando de exibir o texto.
        meuAudioSource = this.GetComponent<AudioSource>();
        colisorPe = this.GetComponent<CapsuleCollider2D>();
        posicaoInicial = this.GetComponent<Transform>().position;   
    }

    private void Start()
    {
        //item = GameObject.FindObjectsOfType<Carta>(); //carrega todos as cartas que estão no jogo.
        eLadoDireito = transform.localScale.x > 0; // --
    }
    void Update()
    {
       grounded = Physics2D.OverlapCircle(groundCheck.position, RaioPulo, 1 << LayerMask.NameToLayer("Ground"));     
        if (Input.GetKeyDown(pulo) && grounded)
        {
            //if (!interfaceJogador.GetComponent<ControlePause>().jogoEstaParado){
            //    aoPressionarPulo.Invoke();
            //}
        }
        if (Input.GetKeyDown(pegar)){
            aoPressionarPegar.Invoke();
        }
        if (Input.GetKeyDown(cancelar)){
            aoPressionarCancelar.Invoke();
        }
        if(Input.GetKeyDown(abaixar)&& grounded){
            aoPressionarAbaixar.Invoke();
        }
        if (Input.GetKeyUp(abaixar)){
            animator.SetBool("abaixando", false);
        }
        if(Input.GetMouseButtonDown(0)||Input.GetKeyDown(atacar)){
            aoPressionarAtaque.Invoke();
        }
        AnimacaoPulo(grounded);

    }

    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector2.right, Color.blue, 0.0001f);
        //Debug.DrawRay(transform.position, Vector2.left, Color.blue, 0.0001f);
        if((Input.GetButton("Direita")&& animator.GetBool("abaixando") == false)||
        (Input.GetButton("Esquerda")&&animator.GetBool("abaixando")==false))
        {
        if(Input.GetButton("Direita"))
            {
                /*Debug.Log("Apertou para direita!");
                if(Physics2D.Raycast( transform.position, Vector2.right, 0.0001f))
                {
                    temParede = true;
                    Debug.Log("Encontrou parede a direita!");
                }
                else temParede = false;
                //if(!temParede)
                //{
                //    transform.position += new Vector3 (Input.GetAxis("Direita")*velocidade*Time.deltaTime, 0f, 0f);
                //    horizontal = 0.5f;
                //}*/
                if(ataque&&grounded)
                {
                    transform.position += new Vector3 (0f, 0f, 0f);
                    horizontal = 0.5f;
                }
                else
                {
                    transform.position += new Vector3 (Input.GetAxis("Direita")*velocidade*Time.deltaTime, 0f, 0f);
                    horizontal = 0.5f;
                }
                
            }
            if(Input.GetButton("Esquerda"))
            {
                /*Debug.Log("Apertou para equerda!");
                if(Physics2D.Raycast( transform.position, Vector2.right, 0.0001f))
                {
                    temParede = true;
                    Debug.Log("Encontrou parede a esquerda!");
                }
                else temParede = false;
                //if(!temParede)
                //{
                //    transform.position += new Vector3 (Input.GetAxis("Esquerda")*velocidade*Time.deltaTime, 0f, 0f);
                //    horizontal = -0.5f;
                //}*/
                if(ataque&&grounded)
                {
                    transform.position += new Vector3 (0f, 0f, 0f);
                    horizontal = -0.5f;
                }
                else
                {
                    transform.position += new Vector3 (Input.GetAxis("Esquerda")*velocidade*Time.deltaTime, 0f, 0f);
                    horizontal = -0.5f;
                }
                
            }
        }
        else horizontal = 0;      
        MudarDirecao(horizontal);                   //função de direção que recebe o valor do eixo X (-1~1)
        Animacao(horizontal);
        //horizontal = Input.GetAxis("Horizontal");   //coloca na variavel o valor o eixo Horizontal (config A e B na Unity)
        //Movimentar(horizontal);                     //funçao de deslocamento que recebe o valor do eixo X (-1~1)
        velocidadeQueda = Math.Abs(rb2D.velocity.y); //caso o jogador caia muito rápido ele morrerá, de acordo com o OnCollisionEnter no fim desse código.
    }
    private void MudarDirecao(float horizontal) //VIRADA DE SPRITE DO PLAYER
    {
        if (horizontal > 0 && !eLadoDireito || horizontal < 0 && eLadoDireito)
        {                                                                       // se (X > 0 && sprite pra esquerda < 0 OU X < 0 && sprite pra direita)
            eLadoDireito = !eLadoDireito;                                       // inverte o argumento e o sprite
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);// --
        }
    }
    private void Animacao(float horizontal)
    {
        animator.SetFloat("velocidade", Mathf.Abs(horizontal));               //Se tiver velocidade abs > 0 anima corrida
    }
    /*private void Movimentar(float h)//MOVIMENTAÇÃO LATERAL DO PLAYER
    {
        rb2D.velocity = new Vector2(h * velocidade, rb2D.velocity.y); //parametro velocidade do rb2D = (eixo X * velocidade, mantem eixo y)     
        Animacao(rb2D.velocity.x);
    }*/
    public void Morrer(bool morrer)
    {
        //executa animação de morrer, que seria o inverso do acordar
        //ControleAudio.instancia.PlayOneShot(audioMorte);
        //interfaceJogador.Reiniciar();       
        estaMorto = false;
        
    }
    public void Pulo()//PULO DO PLAYER bool j
    {
        rb2D.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);
        meuAudioSource.PlayOneShot(audioPulo);
    }
    public void AnimacaoPulo(bool jump) //CONTROLE DAS ANIMAÇÕES DO PLAYER
    {
        if (!jump)
        {
            animator.SetBool("pulando", true);           
        }            //Se não estiver no chão anima o pulo
        if (jump)
        {
            animator.SetBool("pulando", false);
        }          // --- situação oposta ...,
    }

    /*public void PegarItem()//Aqui o jogador pegará cartas no chão, que contará a história do jogo.
    {
        for(int i=0; i<item.Length; i++) //varre toda a lista para ver se tem algum item perto para pegar
        {
            var distancia = Vector2.Distance(this.transform.position, item[i].transform.position);//calcula a distancia do jogador para o item.
            if (distancia < raioDoItem)//se estiver próximo o suficiente.
            {
                //desativa a carta que foi coletada, mas a mantém na lista (não consegui encontrar uma forma de remover sem dar erro no próximo item.
                item[i].gameObject.SetActive(false);
                numeroCarta = item[i].NumeroCarta();
                interfaceJogador.MostrarCarta(numeroCarta);
            }
        }
    }*/

    public void Abaixar()
    {
        animator.SetBool("abaixando", true);    
    }
    public void Atacar()
    {
        animator.SetBool("atacando", true);
        ataque = true;
    }
    public void FimAtaque()
    {
        animator.SetBool("atacando", false);
        ataque = false;
    }
    void OnDrawGizmos()//desenha a esfera de detecção do chão para o pulo, apenas para visualização
    {                                               
        Gizmos.DrawWireSphere(groundCheck.position, RaioPulo);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (velocidadeQueda > velocidadeMorte && other.gameObject.tag=="Plataforma")
        {            
            //Debug.Log("Laco de morte");
            animator.SetBool("morreu", true);
            //ControleAudio.instancia.PlayOneShot(audioMorte);
            //estaMorto = true;
            //Morrer(estaMorto);
            velocidadeQueda = 0.0f;
        }
    }
    private void ChamaMorte()
    {
        animator.SetBool("morreu", false);
        estaMorto = true;
        Morrer(estaMorto);
    }
}
