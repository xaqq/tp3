using UnityEngine;
using System.Collections.Generic;

public class SocietyHandler : MonoBehaviour {
	private int NumberOfAgent;
	private int NumberOfResources;
	private int NumberOfCollisions;
	private int NumberOfPotentialCollisions;
	public int SocietyNumber;
	private float SoldierRatioTreshold = 0.2f;
		
	private List<AIScript> LevelUpUnits = new List<AIScript>(); // units who level-up prev frame
	
	// Use this for initialization
	void Start () {		
		NumberOfResources = 0;
		NumberOfCollisions = 0;
		NumberOfPotentialCollisions = 0;
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
	
	public int GetNumberOfResource()
	{
		return NumberOfResources;
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
	
	
	private float countMySoldier()
	{
		int count = 0;
		if (SocietyNumber == 1)
		{
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_1"))
			{
				if (o.GetComponent<SoldierScript>())
					count++;
			}
			return count;
		}
		if (SocietyNumber == 2)
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
		if (SocietyNumber == 1)
		{
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_1"))
			{
				if (o.GetComponent<SoldierScript>() == null && o != this.gameObject)
					return o.GetComponent<AIScript>();
			}
		}
		if (SocietyNumber == 2)
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
		
		while (NumberOfResources >= 5)
		{
			if ((float)(countMySoldier() / ((float)NumberOfAgent)) < SoldierRatioTreshold)
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
					GameObject child = temp.GetComponentInChildren<TriggerScript>().gameObject;
					Destroy (child.GetComponent<TriggerScript>());
					child.AddComponent<SoldierTriggerScript>();
					child.GetComponentInChildren<SoldierTriggerScript>().Agent = temp;
					Material mat;
					if (SocietyNumber == 1)
						mat = Resources.Load("Textures/Misc/Materials/GreenSoldier", typeof(Material)) as Material;
					else
						mat = Resources.Load("Textures/Misc/Materials/BlackSoldier", typeof(Material)) as Material;
					temp.renderer.material = mat;
			    	GameObject.DestroyImmediate(go);
				}
			}
			else{
				CreateAgent();
			}
			NumberOfResources -= 5;
		}
		LevelUpUnits.Clear();
	}
	
	void OnTriggerEnter(Collider target) {
		if (target.CompareTag("Faction_" + SocietyNumber))
		{
			if (target.gameObject.GetComponent<AIScript>() != null)
			{
				AIScript tmp = target.gameObject.GetComponent<AIScript>();
				if (tmp.AgentHasResource())
				{
					tmp.GiveResourceToForum();
					if (tmp.gameObject.GetComponent("AICommand") != null)
					{
						AICommand tmp2 = tmp.gameObject.GetComponent<AICommand>();
						tmp2.HasCompleted = true;
					}
				}
			}
		}
	}
}
