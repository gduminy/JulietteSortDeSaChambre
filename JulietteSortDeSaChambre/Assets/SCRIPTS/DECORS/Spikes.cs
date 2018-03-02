﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private Animator anim;


	[Header("Temps avant la premiere action :")]
	public float TimeBeforeStart;

	[Header("Temps entre chaque action :")]
	public float TimeRepos;

	private bool StopSpike;
	private bool BeginStopSpike;
	private bool CheckCall;

	[Range(0.1f, 5)]
	[Tooltip("multiplication de la vitesse de l'animation, pour une vitesse normale : 1")]
	public float SpeedMultiplier = 1f;

	// Use this for initialization
	void Start () {
		CheckCall = true;
		StopSpike = false;
		BeginStopSpike = false;
		StartCoroutine(WaitBeforeStart());
		anim = gameObject.GetComponent<Animator> ();
		anim.speed = anim.speed * SpeedMultiplier;
	}

	void Update() {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BeginRepos") && !StopSpike && CheckCall) {
			CheckCall = false;
			StartCoroutine (WaitBeforeUp ());
		}
	}

	public void StopSpikes() {
		BeginStopSpike = true;
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BeginRepos"))
		{
			anim.SetBool ("CanUp", false);
			StopSpike = true;
			BeginStopSpike = false;
		} else {
			Invoke("StopSpikes",0f);
		}	
	}

	void StartSpikes() {
		if (!BeginStopSpike)
		{
			StopSpike = false;
		}
	}

	IEnumerator WaitBeforeStart()
	{
		yield return new WaitForSeconds(TimeBeforeStart);
		anim.SetBool ("CanUp", true);
	}

	IEnumerator WaitBeforeUp()
	{
		anim.SetBool ("CanUp", false);
		yield return new WaitForSeconds(TimeRepos);
		anim.SetBool ("CanUp", true);
		CheckCall = true;
	}
}

