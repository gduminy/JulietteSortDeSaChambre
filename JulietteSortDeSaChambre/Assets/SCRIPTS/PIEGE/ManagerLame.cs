﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLame : Evenement {

	private pendule[] Lames;

	// Use this for initialization
	void Start () {
		Lames = GetComponentsInChildren<pendule> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void StartAllLames()
	{
		foreach(pendule lame in Lames)
		{
			lame.startPendule();
		}
	}

	public void StopAllLames()
	{
		foreach(pendule lame in Lames)
		{
			lame.stopPendule();
		}
	}

}