using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Class PlayerInput
/// </summary>
// DefaultExecutionOrder é uma funcionalidade que define a ordem de execução face aos outros scripts que devem ser 
// executados em simultâneo. Devemos garantir que o input do player seja a prioridade máxima para não haver delays.
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontal;      //Float que guarda os valores do horizontal input.
    [HideInInspector] public bool jumpHeld;         //Bool que regista o facto do evento jumpHeld (manter premido botão de salto) estar a ser executado ou não.
    [HideInInspector] public bool jumpPressed;      //Bool que regista o facto do evento jumpPressed (premir botão de salto) estar a ser executado ou não.
    [HideInInspector] public bool crouchHeld;       //Bool que regista o facto do evento crouchHeld (manter premido botão de agachamento) estar a ser executado ou não.
    [HideInInspector] public bool crouchPressed;    //Bool que regista o facto do evento crouchPressed (premir botão de agachamento) estar a ser executado ou não.

    bool dPadCrouchPrev;
    bool readyToClearInput;                         //Bool que permite manter a sincronização dos inputs


    private void Update()
    {
        //Limpa valores de input existentes
        ClearInput();

        //Função que processa os inputs de Teclado, Rato e Controller
        ProcessInputs();

        //Define que os valores que a variável horizontal poderá receber são exclusivamente entre -1 e 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);

    }

    private void FixedUpdate()
    {
        //Normalmente, usa-se o FixedUpdate() para processar Inputs de forma a prevenir possíveis delays que o método Update()
        //possa provocar. O FixedUpdate() corre normalmente a 50 fps, enquanto que o Update() é variável consoante a performance do jogo.
        //No entanto, o facto de usar os Inputs no FixedUpdate() não garante a prevenção de perdas. Podem haver inputs que simplesmente não
        //conseguiram fazer sync com o método Update(). Assim, utilizando a flag readyClearInput, vamos garantir que todos os inputs introduzidos
        //sejam processados assim que houver uma sincronização entre o FixedUpdate() e o Update(). Deste modo não haverá lag e todos os inputs serão usados.
        readyToClearInput = true;
    }

    private void ClearInput()
    {
        //Se não for o momento certo para utilizar este método (se a sincronização não estiver pronta)
        if (!readyToClearInput)
        {
            return;
        }

        //Reset dos valores de inputs
        horizontal = 0f;
        jumpHeld = false;
        jumpPressed = false;
        crouchHeld = false;
        jumpPressed = false;

        readyToClearInput = false;
    }

    private void ProcessInputs()
    {
        //Acumular de valores
        horizontal += Input.GetAxis("Horizontal");

        jumpPressed = jumpPressed || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("X360_A");
        jumpHeld = jumpHeld || Input.GetKey(KeyCode.Space) || Input.GetButton("X360_A");

        crouchPressed = crouchPressed || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        crouchHeld = crouchHeld || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }
}

