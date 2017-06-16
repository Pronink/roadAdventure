using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermitentesController : MonoBehaviour {

    public List<Light> intermitentes;
    public AudioSource sonidoIntermitentes;
    public float time;
    public bool reproducido = false;

    // Use this for initialization
    void Start () {
        intermitentes.ForEach(item => item.enabled = true);
        sonidoIntermitentes.Play();
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime*2.5f;
        if (((int)time)%2 == 0)
        {
            intermitentes.ForEach(item => item.enabled = true);
            if (reproducido)
            {
                sonidoIntermitentes.pitch = 1.0f;
                sonidoIntermitentes.Play();
                reproducido = false;
            }
        }
        else
        {
            intermitentes.ForEach(item => item.enabled = false);
            if (!reproducido)
            {
                sonidoIntermitentes.pitch = 0.8f;
                sonidoIntermitentes.Play();
                reproducido = true;
            }
        }
        /*if (!sonidoIntermitentes.isPlaying)
        {
            sonidoIntermitentes.Stop();
        }*/
    }
}
