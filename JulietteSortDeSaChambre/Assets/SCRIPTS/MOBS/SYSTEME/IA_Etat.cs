﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class IA_Etat : MonoBehaviour
{

    protected IA_Agent agent;

	protected NavMeshAgent nav;
	protected Animator anim;
	protected Rigidbody rb;
    protected GameObject princesse;
//	protected princesse_vie princesseVie;
//	protected princesse_arme princesseArme;
	protected IA_PointInteret[] pointsInteret;

    // Use this for initialization
    void Awake()
    {
        init();
    }

    protected void init()
    {
		agent = this.GetComponent<IA_Agent>();
        nav = agent.getNav();
		anim = agent.getAnimator ();
		rb = agent.getRigidbody ();
        princesse = agent.getPrincesse();
//		princesseVie = agent.getPrincesse_Vie();
//		princesseArme = agent.getPrincesse_Arme ();
        pointsInteret = agent.getPointsInteret();
    }

    public abstract void entrerEtat();
    public abstract void faireEtat();
    public abstract void sortirEtat();

	protected void changerEtat(IA_Etat nouvelEtat)
    {
        agent.changerEtat(nouvelEtat);
    }

    protected void setAnimation(string nomAnimation)
    {
        agent.setAnimation(nomAnimation);
    }
}
