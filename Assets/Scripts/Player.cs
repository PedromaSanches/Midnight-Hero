using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Player
/// </summary>
public class Player : MonoBehaviour {

    #region Declaração de Variáveis

    public float speed; //Variável que define a velocidade de movimentação da personagem

    private Rigidbody2D player;

    #endregion

    #region Métodos

    /// <summary>
    /// Método Start para inicialização de componentes do Player
    /// </summary>
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Método Update que é invocado a cada frame
    /// </summary>
    void Update()
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
                print("Analog Stick Horizontal Value: " + hAxis);
            }
            if (vAxis != 0)
            {
                print("Analog Stick Vertical Value: " + vAxis);
            }
            if (dPadX != 0)
            {
                print("DPad Horizontal Value: " + dPadX);
            }
            if (dPadY != 0)
            {
                print("DPad Vertical Value: " + dPadY);
            }

            //Teste controlo botão A para saltar
            if (Input.GetButtonDown("X360_A"))
            {
                print("A Button");
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

    #endregion

}
