using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	public int PointToVictory = 50;
	public int NumberOfAgent = 5;
	public int NumberOfResources = 5;
	public SocietyHandler Society1;
	public SocietyHandler Society2;
	private bool Over = false;
	// Use this for initialization
	void Start () {
		GameObject _newResource;
		System.Random rdn = new System.Random();
		Vector3 _randomPos = new Vector3(0,0.5f,0);
		
		for (int i = 0; i < NumberOfResources; i++)
		{
			_newResource = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Resource1"));
			_randomPos.x = rdn.Next (-50, 50);
			_randomPos.z = rdn.Next (-50, 50);
			_newResource.transform.position = _randomPos;
		}
		Society1.InitAgents(NumberOfAgent);
		Society2.InitAgents(NumberOfAgent);
	}
	
	void win(int team)
	{
		Over = true;
		print ("Team " + team + " won !");
	}
	
	// Update is called once per frame
	void Update () {
		int res_fct1 = 0;
		int res_fct2 = 0;
		foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Faction_1"))
		{
		AIScript ai = unit.GetComponent<AIScript>();
			if (ai)
			res_fct1 += ai.GetRessourceQuantity(); 
		}
				foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Faction_2"))
		{
		AIScript ai = unit.GetComponent<AIScript>();
			if (ai)
			res_fct2 += ai.GetRessourceQuantity(); 
		}
		
		if (res_fct1 > PointToVictory && res_fct2 > PointToVictory)
		{
			if (res_fct1 > res_fct2)
				win (1);
			else 
				win (2);
		}
		if (res_fct1 > PointToVictory)
			win (1);
		else if (res_fct2 > PointToVictory)
			win (2);
	}
}
