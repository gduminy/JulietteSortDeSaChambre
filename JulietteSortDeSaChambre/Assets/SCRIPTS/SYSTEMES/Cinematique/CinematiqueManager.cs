﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueManager : Evenement {

    public List<CinematiqueItemList> cinematique;
	public bool isInCinematique;

	private Vector3 posInit;
	private Vector3 forwardInit;

	private AffichageCinematique hudCinematique;
	private GameObject hud;

	private bool isPassable;
	private int indice;

	public bool pause;


	// Use this for initialization
	void Start () {
		//cinematique = new List<CinematiqueItemList> ();
		isInCinematique = false;
		hudCinematique = GameObject.FindGameObjectWithTag ("HUDAffichageCinematique").GetComponent<AffichageCinematique>();
		hud = GameObject.FindGameObjectWithTag ("HUD");
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isInCinematique) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().LockPrincesse ();
            if (isPassable) {
                if (InputManager.GetButtonDown("Pause")) {
					if(!cinematique [indice].desactiveCoupureSon){
						GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().stopSon();
					}
					cinematique [indice].stopCinematique ();
                    ActiveCinematique(false);
                }
            }
        }
	}

	public void ActiveCinematique(bool active){
		if(active){
			if (!cinematique [indice].desactiveBandeNoir) {
				hudCinematique.setActiveBandeNoir (true);
			}
			hudCinematique.setActivePassable (isPassable);
			hud.SetActive (false);
			posInit = Camera.main.transform.position;
			forwardInit = Camera.main.transform.forward;
			isInCinematique = true;

		}else{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().UnlockPrincesse ();
			if (!cinematique [indice].desactiveBandeNoir) {
				hudCinematique.setActiveBandeNoir (false);
			}
			hudCinematique.setActivePassable (false);
			hud.SetActive (true);
			Camera.main.transform.position = posInit;
			Camera.main.transform.forward = forwardInit;
			isInCinematique = false;
		}

	}


	public void lanceCinématique(int indice){
		Debug.Log ("LANCEMENT CINEMATIQUE");
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().LockPrincesse ();
		this.indice = indice;
		if (indice >= 0 && indice < cinematique.Count) {
			isPassable = cinematique [indice].isPassable;
			ActiveCinematique (true);
			cinematique[indice].item = 0;
			cinematique[indice].lancer ();

		}

	}

	override
	public bool evenementIsEnCours(){
		if (pause) {
			pause = false;
			return false;
		}
		return isInCinematique;
	}


}
