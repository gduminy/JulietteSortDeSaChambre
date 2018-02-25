﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageInventaire : MonoBehaviour {

	[HideInInspector]
	public List<ObjetInventaire> listObjet;
	private Dictionary<EnumObjetProgression,GameObject> dicoInventaire;
	private Dictionary<EnumObjetProgression,int> dicoNbObjet;
	public GameObject objetInventaire;
	public GameObject sac;
	public int tempsAffichage;
	private PrincesseObjetProgression juliette;

	private bool affiche;
	private float temps;

	// Use this for initialization
	void Start () {
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseObjetProgression> ();
		dicoInventaire = new Dictionary<EnumObjetProgression, GameObject> ();
		dicoNbObjet = new Dictionary<EnumObjetProgression, int> ();
		affiche = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.GetButtonDown ("Select")) {
			if (!affiche) {
				afficherToutObjet ();
				temps = Time.time;
				affiche = true;
			}

		}

		if (affiche && temps+tempsAffichage<=Time.time) {
			affiche = false;
			desafficherToutObjet ();
		}
	}

	private void afficherToutObjet(){
		
		foreach (GameObject go in dicoInventaire.Values) {
			go.SetActive (true);
		}
	}

	private void desafficherToutObjet(){

		foreach (GameObject go in dicoInventaire.Values) {
			go.SetActive (false);
		}
	}


	public void objetRamasse(EnumObjetProgression enu){
		sac.SetActive (true);
		foreach(ObjetInventaire objet in listObjet){
			if(objet.objet.Equals(enu)){
				if (dicoInventaire.ContainsKey (enu)) {
					addObjet (enu);
				} else {
					createObjet (objet, enu);
				}
			}
		}
	}

	public void objetPerdu(EnumObjetProgression enu){
		if(!juliette.hasItem()){
			sac.SetActive (false);
		}
		deleteObjet (enu);
	}

	private void createObjet(ObjetInventaire objet,EnumObjetProgression enu){
		GameObject temp= Instantiate(objetInventaire);
		temp.transform.parent = gameObject.transform.GetChild (0);
		temp.SetActive (true);
		temp.name = string.Concat (temp.name, "(", objet.objet.ToString(), ")");
		temp.GetComponent<RectTransform> ().localScale = objet.scale;
		temp.GetComponent<RectTransform> ().sizeDelta = objet.size;
		temp.GetComponent<UnityEngine.UI.Image> ().sprite = objet.image;
		temp.SetActive (false);
		dicoInventaire.Add (enu, temp);
		dicoNbObjet.Add(enu, 1);


	}

	private void addObjet(EnumObjetProgression enu){
		dicoNbObjet [enu]++;
		dicoInventaire [enu].GetComponentInChildren<UnityEngine.UI.Text> (true).text = "X" + dicoNbObjet [enu];
	}

	private void deleteObjet(EnumObjetProgression enu){
		dicoNbObjet [enu]--;
		if (dicoNbObjet [enu] == 0) {
			Destroy( dicoInventaire [enu]);
			dicoInventaire.Remove (enu);
			dicoNbObjet.Remove (enu);
		} else {
			
			dicoInventaire [enu].GetComponentInChildren<UnityEngine.UI.Text> (true).text = "X" + dicoNbObjet [enu];
		}
	}
}
