using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {
	
	public UILabel TextLabel;
	
	// Use this for initialization
	void Start () {
		int tmp = PlayerPrefs.GetInt("team");
		TextLabel.text = "Team " + tmp + " won !";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
