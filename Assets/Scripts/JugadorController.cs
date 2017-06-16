using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    private float timePassed;
    public float updateDelay;
    private Rigidbody myrigidbody;
    private Renderer myrenderer;
    public float fuerza;
    public Material player_w_stop;
    public Material player_a_stop;
    public Material player_s_stop;
    public Material player_d_stop;
    public List<Material> player_w_move;
    public List<Material> player_a_move;
    public List<Material> player_s_move;
    public List<Material> player_d_move;
    public float timeMoving;
    public int moveStep;
    public float moveStepInterval;
    public AudioSource stepSound;
    public char lastDirection;
    public Canvas miui;

    // Use this for initialization
    void Start()
    {
        timePassed = 0f;
        myrigidbody = GetComponent<Rigidbody>();
        myrenderer = GetComponent<Renderer>();
        timeMoving = 0F;
        moveStep = 0;
        lastDirection = 's';
    }

    void animateMaterial(char direction)
    {
        timeMoving += myrigidbody.velocity.magnitude/500F; // Este valor va aumentando mientras que se está en movimiento
        int oldMoveStep = moveStep;
        moveStep = (Mathf.RoundToInt(timeMoving / moveStepInterval)) % 4; // Devuelve un valor {0,1,2,3}
        if(oldMoveStep/2 != moveStep/2) // Dividido entre 2 para que suene solo las transiciones par. La comparación para evitar que se reproduzca muchas veces
        {
            sonidoPisada();
        }

        switch (direction)
        {
            case 'n': // none
                timeMoving = 0F;
                setDirectionMaterial(lastDirection); // Si se deja de mover pongo el estado stop de la ultima tecla pulsada
                break;
            case 'w':
                myrenderer.material = player_w_move[moveStep%2]; // Solo uso 2 fotogramas, luego divido {0,1,2,3}%2 para que me de {0,1}
                lastDirection = 'w';
                break;
            case 'a':
                myrenderer.material = player_a_move[moveStep];
                lastDirection = 'a';
                break;
            case 's':
                myrenderer.material = player_s_move[moveStep%2];
                lastDirection = 's';
                break;
            case 'd':
                myrenderer.material = player_d_move[moveStep];
                lastDirection = 'd';
                break;
            default:
                break;
        }
    }

    void sonidoPisada()
    {
        System.Random r = new System.Random();
        int rInt = r.Next(50, 91); // Retorna un rango entre 50 y 90
        stepSound.pitch = (float)rInt / 100f;
        stepSound.Play();
    }

    void setDirectionMaterial(char direction)
    {
        switch (direction)
        {
            case 'w':
                myrenderer.material = player_w_stop;
                break;
            case 'a':
                myrenderer.material = player_a_stop;
                break;
            case 's':
                myrenderer.material = player_s_stop;
                break;
            case 'd':
                myrenderer.material = player_d_stop;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (miui.gameObject.active != true) // Si la UI del diálogo está abierta, no recibo teclas
        {
            timePassed += Time.deltaTime; // Control para que no se ejecute más veces de las necesarias
            if (timePassed >= updateDelay)
            {
                timePassed = 0f;

                // Recibo el teclado
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                {
                    myrigidbody.AddForce(-0.707F * fuerza, 0, -0.707F * fuerza);
                    animateMaterial('d'); // Este método se encarga de darle animación al personaje mientras se mueve
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                {
                    myrigidbody.AddForce(-0.707F * fuerza, 0, 0.707F * fuerza);
                    animateMaterial('d');
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                {
                    myrigidbody.AddForce(0.707F * fuerza, 0, 0.707F * fuerza);
                    animateMaterial('a');
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                {
                    myrigidbody.AddForce(0.707F * fuerza, 0, -0.707F * fuerza);
                    animateMaterial('a');
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    myrigidbody.AddForce(0, 0, -fuerza);
                    animateMaterial('w');
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    myrigidbody.AddForce(0, 0, fuerza);
                    animateMaterial('s');
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    myrigidbody.AddForce(fuerza, 0, 0);
                    animateMaterial('a');
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    myrigidbody.AddForce(-fuerza, 0, 0);
                    animateMaterial('d');
                }
                else
                {
                    animateMaterial('n');
                }
            }
        }
    }

}