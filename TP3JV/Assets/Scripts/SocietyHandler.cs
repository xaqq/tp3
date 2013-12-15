using UnityEngine;
using System.Collections;

public class SocietyHandler : MonoBehaviour {
	
	private int NumberOfAgent;
	private int NumberOfResources;
	private int NumberOfCollisions;
	private int NumberOfPotentialCollisions;
	public int SocietyNumber;
	
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
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
