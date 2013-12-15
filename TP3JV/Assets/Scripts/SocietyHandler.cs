﻿using UnityEngine;
using System.Collections.Generic;

public class SocietyHandler : MonoBehaviour {
	private int CurrentRessource;
	private int NumberOfAgent;
	private int NumberOfResources;
	private int NumberOfCollisions;
	private int NumberOfPotentialCollisions;
	public int SocietyNumber;
	public int SoldierRatioTreshold = 30;
		
	private List<AIScript> LevelUpUnits = new List<AIScript>(); // units who level-up prev frame
	
	// Use this for initialization
	void Start () {		
		NumberOfResources = 0;
		NumberOfCollisions = 0;
		NumberOfPotentialCollisions = 0;
		CurrentRessource = 0;
	}
	
	public void InitAgents (int _number) {
		GameObject _newAgent;
		AIScript _tmpScript;
		Vector3 _tmp;
		
		NumberOfAgent = _number;
		for (int i = 0; i < NumberOfAgent; i++)
		{
			_newAgent = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Agent" + SocietyNumber.ToString()));
			_tmp = (Random.insideUnitSphere * 25) + this.transform.position;
			_tmp.y = this.transform.position.y;
			_newAgent.transform.position = _tmp;
			_tmpScript = _newAgent.GetComponent<AIScript>();
			_tmpScript.SetSociety(this);
		}
		
	}
	
	private void CreateAgent()
	{
			GameObject _newAgent;
		AIScript _tmpScript;
		Vector3 _tmp;
		
		    NumberOfAgent++;
			_newAgent = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Agent" + SocietyNumber.ToString()));
			_tmp = (Random.insideUnitSphere * 25) + this.transform.position;
			_tmp.y = this.transform.position.y;
			_newAgent.transform.position = _tmp;
			_tmpScript = _newAgent.GetComponent<AIScript>();
			_tmpScript.SetSociety(this);
	
	}
	
	public void UnitLeveledUp(AIScript unit)
	{
		LevelUpUnits.Add(unit);
	}
	
	public int GetNumberOfAgent()
	{
		return NumberOfAgent;
	}
	
	public void SetNumberOfResources(int qte)
	{
		NumberOfResources = qte;
	}
	
	public void AddResources(int qte)
	{
		NumberOfResources += qte;
		CurrentRessource += qte;
		print ("I AM SOCIETY " + SocietyNumber.ToString() + " AND I HAVE " + NumberOfResources.ToString() + " RESOURCES");
	}
	
	public void AgentDead()
	{
		NumberOfAgent--;
	}
	
	public void AddCollision()
	{
		NumberOfCollisions++;
	}
	public void RemoveCollision()
	{
		NumberOfCollisions--;
	}
	public int GetNumberOfCollisions()
	{
		return NumberOfCollisions;
	}
	public void AddPotentialCollision()
	{
		NumberOfPotentialCollisions++;
	}
	public void RemovePotentialCollision()
	{
		NumberOfPotentialCollisions--;
	}
	public int GetNumberOfPotentialCollisions()
	{
		return NumberOfPotentialCollisions;
	}
	
	
	private int countMySoldier()
	{
		int count = 0;
		if (this.gameObject.CompareTag("Faction_1"))
		{
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_1"))
			{
				if (o.GetComponent<SoldierScript>())
					count++;
			}
			return count;
		}
				if (this.gameObject.CompareTag("Faction_2"))
		{
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_2"))
			{
				if (o.GetComponent<SoldierScript>())
					count++;
			}
			return count;
		}
		return count;
	}
	
	private AIScript pickRandomAgent()
	{
		if (this.gameObject.CompareTag("Faction_1"))
		{
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_1"))
			{

				if (o.GetComponent<SoldierScript>() == null && o != this.gameObject)
					return o.GetComponent<AIScript>();
			}
		}
				if (this.gameObject.CompareTag("Faction_2"))
		{
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_2"))
			{
				if (o.GetComponent<SoldierScript>() == null && o != this.gameObject)
					return o.GetComponent<AIScript>();
			}
		}
		return null;	
	}
	
	// Update is called once per frame
	void Update () {
		
		while (CurrentRessource >= 5)
		{
			if (countMySoldier() / ( NumberOfAgent == 0 ? 1 : NumberOfAgent) < SoldierRatioTreshold)
			{
				AIScript agent = pickRandomAgent();
				if (agent)
				{					
				//  becomes a level 1 soldier
				AIScript go = agent.gameObject.GetComponent<AIScript>();
				SoldierScript temp = (agent.gameObject.AddComponent("SoldierScript") as SoldierScript);
				temp.MinPos = -50;
				temp.MaxPos = 50;
				temp.SetSociety(this);
				temp.GetComponentInChildren<TriggerScript>().Agent = temp;
			    GameObject.DestroyImmediate(go);
				}
			}
			else{
				CreateAgent();
			}
			CurrentRessource -= 5;
		}
		LevelUpUnits.Clear();
	}
}
