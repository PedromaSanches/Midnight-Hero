using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Player
/// </summary>
public class Player : MonoBehaviour {

    #region Declaração de Variáveis

    //Configurações
    [SerializeField] float speed = 5f; //Variável que define a velocidade de movimentação da personagem

    //Componentes em cache
    private Rigidbody2D player;
    private Animator animator;

    #endregion

    #region Métodos

    /// <summary>
    /// Método Start para inicialização de componentes do Player
    /// </summary>
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Método Update que é invocado a cada frame
    /// </summary>
    void Update()
    {
        //Chamada do método que permite a movimentação do Player
        PlayerMovement();

        //Chamada do método que troca a direção do Sprite do Player
        FlipSprite();
    }

    /// <summary>
    /// Método que define a movimentação do objeto Player
    /// </summary>
    void PlayerMovement()
    {
        #region Xbox

        //Condição que deteta se existe um Controller conectado permitindo nesse caso jogar com o comando
        if (Input.GetJoystickNames() != null)
        {
            //Teste controlos analog sticks
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            //Teste controlos com Dpad
            float dPadX = Input.GetAxis("X360_DPad_X");
            float dPadY = Input.GetAxis("X360_DPad_Y");

            if (hAxis != 0)
            {

                //Se a personagem se encontrava em modo Idle
                if (animator.GetBool("Idle") == true)
                {
                    //Coloca a falso o modo de Idle
                    animator.SetBool("Idle", false);

                    //Definição da deslocação do Player
                    Vector2 playerVelocity = new Vector2(hAxis * speed, player.velocity.y);
                    player.velocity = playerVelocity;

                    //Definição da Animação
                    animator.SetBool("Running", true);
                }

            }
            if (vAxis != 0)
            {
                //Para já não fazer nada...
            }
            if (dPadX != 0)
            {
                //Se a personagem se encontrava em modo Idle
                if (animator.GetBool("Idle") == true)
                {
                    //Coloca a falso o modo de Idle
                    animator.SetBool("Idle", false);

                    //Definição da deslocação do Player
                    Vector2 playerVelocity = new Vector2(hAxis * speed, player.velocity.y);
                    player.velocity = playerVelocity;

                    //Definição da Animação
                    animator.SetBool("Running", true);
                }
            }
            if (dPadY != 0)
            {
                //Para já não fazer nada...
            }

            //Teste controlo botão A para saltar
            if (Input.GetButtonDown("X360_A"))
            {
                //Para já não fazer nada...
            }
        }

        #endregion

        #region Teclado

        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //Movimento que poderá ser usado futuramente
        }

        //Movimento para a esquerda do Player 
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //Inserir animação de andar com o Player direcionado para a esquerda 
            //Definição da deslocação do Player
            transform.Translate(-speed * Time.deltaTime, 0, 0);

            //Definição da Animação
            animator.SetBool("Running", true);

            //No caso do personagem estar a andar para a esquerda e o jogador premir a tecla para baixo então o Player irá deslizar nessa direção
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {

            }
            //Movimento para saltar no caso do Player se encontrar em movimento para a esquerda
            else if (Input.GetKey(KeyCode.Space))
            {
                //Inserir animação para saltar o Player se encontra em movimento para a esquerda
            }
        }

        //Movimento para a direita do Player
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //Inserir animação de andar com o Player direcionado para a direita 
            transform.Translate(speed * Time.deltaTime, 0, 0);

            //Definição da Animação
            animator.SetBool("Running", true);
            
            //No caso do personagem estar a andar para a direita e o jogador premir a tecla para baixo então o Player irá deslizar nessa direção
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {

            }
            //Movimento para saltar no caso do Player se encontrar em movimento para a direita
            else if (Input.GetKey(KeyCode.Space))
            {
                //Inserir animação para saltar o Player se encontra em movimento para a direita
            }
        }
        
        //No caso de IDLE, ativar respetiva animação IDLE que exibe a última direção para a qual o Player estava direcionado
        else
        {
            //Inserir animação para IDLE com o Player direcionado para a esquerda ou direita consoante a sua última posição
            animator.SetBool("Idle", true);

            //Movimento para agachar no caso de IDLE
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //Inserir animação para agachar enquanto IDLE com o Player direcionado para a esquerda ou direita consoante a sua última posição
            }
            //Movimento para saltar no caso de IDLE
            else if (Input.GetKey(KeyCode.Space))
            {
                //Inserir animação para saltar enquanto IDLE com o Player direcionado para a esquerda ou direita consoante a sua última posição
            }

        }

        #endregion
        
    }

    /// <summary>
    /// Método que troca a direção do Sprite do Player
    /// </summary>
    void FlipSprite()
    {
        //Condição que verifica se o Player está a realizar algum movimento na horizontal
        if (Mathf.Abs(player.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(player.velocity.x), 1f);
        }
    }

    #endregion

}
