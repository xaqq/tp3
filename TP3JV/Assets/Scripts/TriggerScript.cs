using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
	
	public AIScript Agent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider target) {
		if (target.CompareTag("Ressource") && Agent.Attacker == null)
		{
			Agent.SwitchTo(Commands.MOVETORESSOURCE);
			Agent.SetTarget(target.transform);
			print (target.transform.position);
		}
		if ((target.CompareTag("Faction_1") && Agent.CompareTag("Faction_2")) ||
			(Agent.CompareTag("Faction_1") && target.CompareTag("Faction_2")))
		{
			SoldierScript soldier = target.GetComponent<SoldierScript>();
			if (soldier && soldier.GetTarget() == Agent.transform)
			{
				Agent.Attacker = soldier;
				Agent.SwitchTo(Commands.FLEE);
			}
		}
		if (target.gameObject.CompareTag("Faction_1") || target.gameObject.CompareTag("Faction_2"))
		{
			Agent.MySociety.AddPotentialCollision();
		}
    }
	
	void OnTriggerExit(Collider target) {
		if (target.CompareTag("Ressource"))
		{
			print("Not in range anymore");
		}
			if (Agent.Attacker == target.GetComponent<SoldierScript>())
			{
				Agent.Attacker = null;
				Agent.SwitchTo(Commands.MOVE);
			}
		if (target.gameObject.CompareTag("Faction_1") || target.gameObject.CompareTag("Faction_2"))
		{
			Agent.MySociety.RemovePotentialCollision();
		}
    }
}
