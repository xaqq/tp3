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
		if (target.CompareTag("Ressource"))
		{
			Agent.SwitchTo(Commands.MOVETORESSOURCE);
			Agent.SetTarget(target.transform);
			print (target.transform.position);
			print("ESH");
		}
    }
	
	void OnTriggerExit(Collider target) {
		if (target.CompareTag("Ressource"))
		{
			print("ESHLEFT");
		}
    }
}
