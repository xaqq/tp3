using UnityEngine;
using System.Collections;

public class SoldierTriggerScript : MonoBehaviour {
	
	public SoldierScript Agent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider target) {
		if ((target.CompareTag("Faction_1") && Agent.CompareTag("Faction_2")) ||
			(Agent.CompareTag("Faction_1") && target.CompareTag("Faction_2")))
		{
			if (Agent.GetTarget() == null)
			{
			Agent.SwitchTo(Commands.CHASE);
			Agent.SetTarget(target.transform);
			print ("Trigger for chasing " + target.transform.position);
			}
		}
    }
	
	void OnTriggerExit(Collider target) {
		if ((target.CompareTag("Faction_1") && Agent.CompareTag("Faction_2")) ||
			(Agent.CompareTag("Faction_1") && target.CompareTag("Faction_2")))
		{
			if (Agent.GetTarget() == target.GetComponent<AIScript>().transform)
			{
			print("Not in range anymore");
			Agent.SwitchTo(Commands.MOVE);
				Agent.SetTarget(null);
			}
			}
	}
}
