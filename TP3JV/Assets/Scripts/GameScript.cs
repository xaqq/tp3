using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	public int PointToVictory = 20;
	public int NumberOfAgent = 5;
	public int NumberOfResources = 5;
	public SocietyHandler Society1;
	public SocietyHandler Society2;
	// Use this for initialization
	void Start () {
		GameObject _newResource;
		System.Random rdn = new System.Random();
		Vector3 _randomPos = new Vector3(0,0.5f,0);
		
		Society1.InitAgents(NumberOfAgent);
		Society2.InitAgents(NumberOfAgent);
		
		for (int i = 0; i < NumberOfResources; i++)
		{
			_newResource = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Resource1"));
			_randomPos.x = rdn.Next (-50, 50);
			_randomPos.z = rdn.Next (-50, 50);
			_newResource.transform.position = _randomPos;
		}
	}
	
	void win(int team)
	{
		PlayerPrefs.SetInt ("team", team);
		Application.LoadLevel("End");
	}
	
	// Update is called once per frame
	void Update () {
		if (Society1.GetNumberOfAgent() > PointToVictory)
			win (1);
		else if (Society2.GetNumberOfAgent() > PointToVictory)
			win (2);
	}
}
