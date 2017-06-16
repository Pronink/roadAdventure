using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteracuadorController : MonoBehaviour {

    public Transform target;
    public float offsetIC;
    private Vector3 offsetW;
    private Vector3 offsetA;
    private Vector3 offsetS;
    private Vector3 offsetD;
    public List<Collider> triggerList = new List<Collider>();
    public Canvas miui;
    public Text textui;
    public Image imageui;


    public Material avatarAlex;
    private int stateAlex = 0;
    public Material avatarIan_Feliz;
    public Material avatarIan_Enfadado;
    public Material avatarIan_Llorando;
    private int stateIan = 0;
    public Material avatarPeter;
    private int statePeter = 0;

    public ParticleSystem chorroAgua;
    private bool llaveInglesa = false;
    private bool misionCompletada = false;

    // Use this for initialization
    void Start()
    {
        offsetW = new Vector3(0, 0, -offsetIC);
        offsetA = new Vector3(offsetIC, 0, 0);
        offsetS = new Vector3(0, 0, offsetIC);
        offsetD = new Vector3(-offsetIC, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (miui.gameObject.active == true)
            {
                miui.gameObject.SetActive(false);
            }
            else
            {
                if (triggerList.Count > 0)
                {
                    testHablar(triggerList[0].name);
                }
            }
        }
        // El offset es lo que hara que se posicione de una forma u otra
        switch (target.GetComponent<JugadorController>().lastDirection)
        {
            case 'w':
                transform.position = target.transform.position + offsetW;
                break;
            case 'a':
                transform.position = target.transform.position + offsetA;
                break;
            case 's':
                transform.position = target.transform.position + offsetS;
                break;
            case 'd':
                transform.position = target.transform.position + offsetD;
                break;
            default:
                break;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        triggerList.Add(other);
    }
    void OnTriggerExit(Collider other)
    {
        triggerList.Remove(other);
    }


    private void testHablar(string name)
    {
        if (name == "Alex")
        {
            switch(stateAlex)
            {
                case 0:
                    hablar(name, avatarAlex, "- Alex: Da la vuelta. Por este sentido no hay nada.");
                    break;
                case 1:
                    hablar(name, avatarAlex, "- Alex: ¿Crees que venir aquí te va a salvar?");
                    break;
                case 2:
                    hablar(name, avatarAlex, "- Alex: Ya he ordenado mis cosas. ¿Las has ordenado tú?");
                    break;
                case 3:
                    hablar(name, avatarAlex, "- Alex: ¿ No te sientes culpable ?");
                    break;
                default:
                    hablar(name, avatarAlex, "- Alex: ¡ Déjame en paz, anda !");
                    break;
            }
            stateAlex++;
        }
        if (name == "Coche_Renault5")
        {
            hablar(null, null, "Parece que el coche no va a moverse más.");
        }
        if (name == "Muro_Invisible")
        {
            hablar(null, null, "Una fuerza invisible me impide avanzar.");
        }
        if (name == "Muro_Invisible2")
        {
            hablar(null, null, "Otra fuerza invisible me impide avanzar... quien hizo esto no tenía mucha imaginación...");
        }
        if (name == "EsferaRara1" || name == "EsferaRara2" || name == "EsferaRara3")
        {
            hablar(null, null, "¡ Es un color tan puro que me hace daño en los ojos !");
        }
        if (name == "Ian")
        {
            switch (stateIan)
            {
                case 0:
                    hablar(name, avatarIan_Feliz, "- Ían: ¡ Hola buenas ! ¿ Sería tan amable de dejarme un teléfono para llamar a la centralita ?");
                    stateIan++;
                    break;
                case 1:
                    hablar(name, avatarIan_Enfadado, "- Ían: No pasa nada... gracias por nada.");
                    stateIan++;
                    break;
                case 2:
                    hablar(name, avatarIan_Enfadado, "- Ían: Si no vas a darme el teléfono mejor vete donde no pueda verte.");
                    stateIan++;
                    break;
                case 3:
                    hablar(name, avatarIan_Feliz, "- Ían: Ya que parece que quieres ayudarme. Por favor, toma esta llave inglesa y cierra la escotilla de agua del camión");
                    stateIan++;
                    llaveInglesa = true;
                    break;
                case 4:
                    if (misionCompletada)
                    {
                        hablar(name, avatarIan_Feliz, "- Ían: ¡ Muchas gracias ! Lo hubiera podido hacer sin ti... pero bueno. Te daria oro, pero no tengo suelto.");
                        stateIan++;
                    }
                    else
                    {
                        hablar(name, avatarIan_Enfadado, "- Ían: ¡ ES PARA HOY !");
                    }
                    break;
                case 5:
                    hablar(name, avatarIan_Llorando, "- Ían: El motor está completamente roto... vamos a tener que comprar un camión nuevo y me van a despedir...");
                    stateIan++;
                    break;
                case 6:
                    hablar(name, avatarIan_Feliz, "- Ían: Aunque no pasa nada. ¡ Mi sueño siempre fué ser soldado !");
                    stateIan++;
                    break;
                case 7:
                    hablar(name, avatarIan_Llorando, "- Ían: Jamás podré entrar al ejército...");
                    stateIan++;
                    break;
                case 8:
                    hablar(name, avatarIan_Feliz, "- Ían: Intenta buscar ayuda más adelante, por favor.");
                    stateIan++;
                    break;
                default:
                    hablar(name, avatarIan_Enfadado, "- Ían: ¡ QUE BUSQUES AYUDA, HE DICHO !");
                    stateIan++;
                    break;
            }
        }
        if (name == "Coche_CamionBomberos")
        {
            if (!llaveInglesa)
            {
                hablar(null, null, "Alguien abrió la escotilla de agua del camión de bomberos.");
            }
            else if (llaveInglesa && !misionCompletada)
            {
                hablar(null, null, "Cierras la escotilla del agua del camión de bomberos.");
                var em = chorroAgua.emission;
                em.rateOverTime = 1f;
                var ma = chorroAgua.main;
                ma.startSpeed = 4f;
                var sh = chorroAgua.shape;
                sh.angle = 3.5f;
                misionCompletada = true;
            }
            else
            {
                hablar(null, null, "Abres al máximo la escotilla del agua.");
                var em = chorroAgua.emission;
                em.rateOverTime = 40f;
                var ma = chorroAgua.main;
                ma.startSpeed = 10f;
                var sh = chorroAgua.shape;
                sh.angle = 20f;
                misionCompletada = false;
            }
            
        }
        if (name == "Peter")
        {
            switch (statePeter)
            {
                case 0:
                    hablar(name, avatarPeter, "- Peter: Hola viajero. Este es el final del camino y el final de la demo. Por ahora...");
                    statePeter++;
                    break;
                case 1:
                    hablar(name, avatarPeter, "- Peter: No hay más de aquí en adelante. Pero... ¿ y atrás ?");
                    statePeter++;
                    break;
                default:
                    hablar(name, avatarPeter, "- Peter: ¡ Gracias por jugar a la demo !");
                    statePeter++;
                    break;
            }
        }
    }
    private void hablar(string sujeto, Material material, string dialogo)
    {
        miui.gameObject.SetActive(true);
        textui.text = dialogo;
        if (sujeto == null)
        {
            imageui.enabled = false;
        }
        else
        {
            imageui.enabled = true;
            imageui.material = material;
        }
    }
}