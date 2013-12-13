using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	public int PointToVictory = 50;
	private bool over = false;
	// Use this for initialization
	void Start () {
	
	}
	
	void win(int team)
	{
		over = true;
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
			res_fct1 += ai.GetResourceQuantity(); 
		}
				foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Faction_2"))
		{
		AIScript ai = unit.GetComponent<AIScript>();
			if (ai)
			res_fct2 += ai.GetResourceQuantity(); 
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
