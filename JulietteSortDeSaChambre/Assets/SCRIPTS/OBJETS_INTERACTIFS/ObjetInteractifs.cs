﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjetInteractifs : MonoBehaviour {

	public string nomObjet;
	public string descriptionObjet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public abstract void Activation();
}