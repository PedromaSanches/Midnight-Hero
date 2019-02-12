using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Player
/// </summary>
public class Player : MonoBehaviour {

    #region Declaração de Variáveis

    //Configurações
    float speed = 1f; //Variável que define a velocidade de movimentação da personagem
    bool facingRight = true; //Variável que guarda a direção para onde o Player está virado
    public float fallMultiplier = 2.5f; //Multiplicador de queda ao saltar
    public float lowJumpMultiplier = 2f; //Multiplicador de salto 
    [Range(1, 10)]
    public float jumpVelocity;


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
    /// Método FixedUpdate que é invocado a cada instante
    /// </summary>
    void FixedUpdate()
    {
        //Chamada do método que permite a movimentação do Player
        PlayerMovement();
        
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
                    //Coloca a falso o modo de Idle e de Jumping
                    animator.SetBool("Idle", false);
                    animator.SetBool("Jumping", false);

                    //Definição da Animação
                    animator.SetBool("Running", true);

                    //Definição da deslocação do Player
                    Vector2 playerVelocity = new Vector2(hAxis * speed, player.velocity.y);
                    player.velocity = playerVelocity;

                    
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
                    //Coloca a falso o modo de Idle e de Jumping
                    animator.SetBool("Idle", false);
                    animator.SetBool("Jumping", false);

                    //Definição da Animação
                    animator.SetBool("Running", true);

                    //Definição da deslocação do Player
                    Vector2 playerVelocity = new Vector2(hAxis * speed, player.velocity.y);
                    player.velocity = playerVelocity;
                    
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

            //Valor da input 
            float hAxis = Input.GetAxis("Horizontal");

            //Condição que verifica se é necessário realizar a troca de direção do Player
            if (hAxis < 0 && facingRight)
            {
                FlipSprite();
            }

            //Definição da Animação
            animator.SetBool("Jumping", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
            
            //Definição da deslocação do Player
            player.velocity = new Vector2(hAxis * speed, player.velocity.y);
            
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
            //Valor da input 
            float hAxis = Input.GetAxis("Horizontal");

            //Condição que verifica se é necessário realizar a troca de direção do Player
            if (hAxis > 0 && !facingRight)
            {
                FlipSprite();
            }

            //Definição da Animação
            animator.SetBool("Jumping", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);

            //Definição da deslocação do Player
            player.velocity = new Vector2(hAxis * speed, player.velocity.y);
            
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
            //É suposto o Player passar ao estado de IDLE neste 'else', como tal é necessário cancelar qualquer outra animação
            animator.SetBool("Running", false);

            animator.SetBool("Jumping", false);

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

                //Definição do salto do Player
                player.velocity = Vector2.up * jumpVelocity;

                //Condição que verifica se o Player encontra-se em queda
                if (player.velocity.y < 0)
                {
                    player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // https://www.youtube.com/watch?v=7KiK0Aqtmzc
                }

                //Definição da Animação
                animator.SetBool("Idle", false);
                animator.SetBool("Jumping", true);
                
            }

        }

        #endregion
        
    }

    /// <summary>
    /// Método que troca a direção do Sprite do Player
    /// </summary>
    void FlipSprite()
    {
        facingRight = !facingRight; //Altera o estado atual do Player
        Vector3 theScale = transform.localScale; //Guarda o valor em 3D escala do player
        theScale.x *= -1; //Altera a escala do player em X, invertendo-a
        transform.localScale = theScale; //Guarda a nova escala do Player
    }

    #endregion

}
