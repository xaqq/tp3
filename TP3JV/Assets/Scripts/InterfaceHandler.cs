using UnityEngine;
using System.Collections;

public class InterfaceHandler : MonoBehaviour {
	
	public UILabel Fps;
	public UILabel MsPerTrame;
	public UILabel NumberAgent;
	public UILabel NumberCollision;
	public UILabel NumberPotentialCollision;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Fps.text = "Fps : " + (1.0f / Time.deltaTime).ToString();
	}
}
