using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

	public Transform target;
	public Vector3 offset;

	// Use this for initialization
	void Start ()
	{
		offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
        // El offset es lo que hara que se posicione de una forma u otra
		transform.position = target.transform.position + offset;
	}
}
