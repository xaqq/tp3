using UnityEngine;
using System.Collections;

public class InterfaceHandler : MonoBehaviour {
	
	public UILabel Fps;
	public UILabel MsPerTrame;
	public UILabel NumberAgent;
	public UILabel NumberCollision;
	public UILabel NumberPotentialCollision;
	
	public SocietyHandler Society1;
	public SocietyHandler Society2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Fps.text = "Fps : " + (1.0f / Time.smoothDeltaTime).ToString();
		MsPerTrame.text = "Mpf : " + (Time.smoothDeltaTime * 1000).ToString();
		NumberAgent.text = "Agent : " + (Society1.GetNumberOfAgent() + Society2.GetNumberOfAgent()).ToString();
		NumberCollision.text = "Collisions : " + (Society1.GetNumberOfCollisions() + Society2.GetNumberOfCollisions()).ToString();
		NumberPotentialCollision.text = "Potential : " + (Society1.GetNumberOfPotentialCollisions() + Society2.GetNumberOfPotentialCollisions()).ToString();
	}
}
