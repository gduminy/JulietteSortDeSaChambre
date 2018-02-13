﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_AttaquerHorizontalement : IA_Etat {

	public int degats;
	public float forceRecul;
	public float distanceParcourue;
	public AudioClip sonAttaque;
	public float vitesse;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		colliderArme = GetComponent<IA_TriggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		agent.getSoundEntity().playOneShot(sonAttaque, 1.0f);
		degatsAttaqueEffectues = false;
		setAnimation (TRO_Animations.ATTAQUER_HORIZONTALEMENT);
		nav.enabled = true;
		nav.speed = vitesse;
		agent.definirDestination (this.transform.position + this.transform.forward * this.distanceParcourue);
		timer = Time.time + 1.0f;
	}

	public override void faireEtat()
	{
		if (timer > Time.time) { // l'attaque est toujours en cours
			if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

				princesseVie.blesser (degats, this.gameObject, forceRecul);
				degatsAttaqueEffectues = true;
			}
		} else {
			changerEtat(this.GetComponent<TRO_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
