using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Class PlayerMovement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Propriedades do Movimento do Personagem")]
    public float speed = 2f;                                //Velocidade de movimentação do personagem.
    public float crouchSpeedDivisor = 0.5f;                 //Redução da velocidade aquando agachamento.
    public float coyoteDuration = 0.5f;                     //Duração temporal de meio segundo de CoyoteTime (impede a queda nos jogos de plataformas quando o jogador ultrapassa o espaço permitido considerado ground).
    public float maxFallSpeed = -5f;                        //Velocidade máxima de queda.

    [Header("Propriedades de Salto do Personagem")]
    public float jumpForce = 1.5f;                          //Força de salto inicial.
    public float crouchJumpBoost = 0.5f;                    //Boost no salto aquando agachado.
    public float jumpHoldForce = 0.2f;                      //Força incremental no salto quando é mantido o botão de salto premido.
    public float jumpHoldDuration = 0.1f;                   //Duração de tempo que o botão de salto pode ser premido.

    [Header("Environment Check Properties")]
    public float groundDistance = 0.2f;                     //Distância a que o Player é considerado estar em relação ao chão.
    public Collision2D ground;                              //Definição e possivel colisão com Ground

    [Header("Flags de Estados do Player")]
    public bool isOnGround = true;                          //Bool se o Player se encontra no chão.
    public bool isJumping;                                  //Bool se o Player se encontra a saltar.
    public bool isCrouching;                                //Bool se o Player se encontra agachado.
    public bool isIDLE;                                     //Bool se o Player se encontra em IDLE.

    [Header("Outras Variáveis")]
    PlayerInput input;                                      //Variável que acede aos inputs para o player.
    Rigidbody2D rigidBody;                                  //Variável que contém o componente de física.

    float jumpTime;                                         //Variável usada para calculos de duração de manter premido o botão de saltar.
    float coyoteTime;                                       //Variável usada para calculos de duração do coyote time.

    float originalXScale;                                   //Escala original do Axis de X.
    int direction = 1;                                      //Int que indica a direção na qual o Player se está a mover

    private void Start()
    {
        //Recebe a referência dos componentes necessários do Player.
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        

        //Recebe a escala original do eixo de X do Player.
        originalXScale = transform.localScale.x;
    }

    private void FixedUpdate()
    {

        //Método que processa movimentos do Player no chão e no ar.
        GroundMovement();
        MidAirMovement();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ao ser detada uma colisão entre o Player e um Collider, verifica-se se se trata do BoxCollider2D com a Tag Ground
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Ao ser detada o fim de uma colisão entre o Player e um Collider, verifica-se se se trata do BoxCollider2D com a Tag Ground
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = false;
        }
    }

    private void GroundMovement()
    {
        //Se for premido o botão de agachamaneto e o Player não estiver agachado
        if (input.crouchHeld && !isCrouching)
        {
            Crouch();
        }
        //Se o Player estiver agachado mas não houver input de agachamento, levanta-se
        else if (!input.crouchHeld && isCrouching)
        {
            StandUp();
        }
        //Se o Player estiver agachado mas não estiver no chão, levanta-se
        else if (!isOnGround && isCrouching)
        {
            StandUp();
        }

        //Cálculo do deslocamento do Player nas horizontais (eixo X)
        float xVelocity = speed * input.horizontal;

        //Se a velocidade e a direção não tiverem o mesmo sinal, significa que é necessário mudar a direção em que o Player está direcionado
        if (xVelocity * direction < 0f)
        {
            FlipCharacterDirection();
        }

        //Aplicar a velocidade desejada no Player
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        //Se o Player se encontrar no chão extende-se a janela de coyote time
        if (isOnGround)
        {
            coyoteTime = Time.time * coyoteDuration;
        }
    }

    private void MidAirMovement()
    {
        //Se o Player se encontrar no chão ou dentro de numa janela coyote time 
        if (input.jumpPressed && !isJumping && (isOnGround || coyoteTime > Time.time))
        {
            //...verifica-se se está agachado
            if (isCrouching)
            {
                //... pois nesse caso deve levantar-se
                StandUp();
                rigidBody.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }

            //... o Player não está no chão e está a saltar
            isOnGround = false;
            isJumping = true;

            //... calculado o tempo de salto em que o Player não poderá prolongar mais o boost para o seu salto (isto impede que enquanto se esteja a clicar, o Player continue a saltar até ao infinito)
            jumpTime = Time.time + jumpHoldDuration;

            //... adiciona a força de impulso no rigidbody do Player para que este se movimente para cima de modo a simular o salto
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            
        }
        //Caso o Player se encontre já a saltar...
        else if(isJumping)
        {
            //... e o botão de salto continuar a ser premido, aplica-se uma força incremental no rigidbody do Player
            if (input.jumpHeld)
            {
                rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            }

            //.. e se o tempo de salto tiver esgotado, dá-se e indicação na respetiva flag que o salto terminou, iniciando a queda
            if (jumpTime <= Time.time)
            {
                isJumping = false;
            }
        }

        //Se o Player estiver em queda, a alta velocidade, garante-se que este não passa da velocidade máxima definida
        if (rigidBody.velocity.y < maxFallSpeed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
        }
    }

    private void FlipCharacterDirection()
    {
        //Muda a direção para onde o Player está direcionado.
        direction *= -1;

        //Recebe a escala do Player atual
        Vector3 scale = transform.localScale;

        //Modifica essa escala no eixo de x representando agora a mudança de direção
        scale.x = originalXScale * direction;

        //Aplica a nova escala
        transform.localScale = scale;
    }

    private void Crouch()
    {
        //se o Player está agachado, não está em pé
        isCrouching = true;

    }

    private void StandUp()
    {
        //Se o Player está em pé, não está agachado
        isCrouching = false;
    }

}
