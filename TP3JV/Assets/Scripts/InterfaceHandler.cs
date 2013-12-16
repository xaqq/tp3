using UnityEngine;
using System.Collections;

public class InterfaceHandler : MonoBehaviour {
	
	public UILabel Fps;
	public UILabel MsPerTrame;
	public UILabel NumberAgent;
	public UILabel NumberCollision;
	public UILabel NumberPotentialCollision;
	
	public UILabel NumberAgent1;
	public UILabel NumberRes1;
	public UILabel NumberAgent2;
	public UILabel NumberRes2;
	
	public SocietyHandler Society1;
	public SocietyHandler Society2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void UpdateSociety()
	{
		NumberAgent1.text = "Agent : " + Society1.GetNumberOfAgent();
		NumberAgent2.text = "Agent : " + Society2.GetNumberOfAgent();
		NumberRes1.text = "Resource : " + Society1.GetNumberOfResource();
		NumberRes2.text = "Resource : " + Society2.GetNumberOfResource();
	}
	
	// Update is called once per frame
	void Update () {
		Fps.text = "Fps : " + (1.0f / Time.smoothDeltaTime).ToString();
		MsPerTrame.text = "Mpf : " + (Time.smoothDeltaTime * 1000).ToString();
		NumberAgent.text = "Agent : " + (Society1.GetNumberOfAgent() + Society2.GetNumberOfAgent()).ToString();
		NumberCollision.text = "Collisions : " + (Society1.GetNumberOfCollisions() + Society2.GetNumberOfCollisions()).ToString();
		NumberPotentialCollision.text = "Potential : " + (Society1.GetNumberOfPotentialCollisions() + Society2.GetNumberOfPotentialCollisions()).ToString();
		UpdateSociety();
	}
}
