using UnityEngine;
using System.Collections;

public class AICommand_Attack : AICommand {

	private AIScript ai_;
	private AIScript target_ = null;
	
	// Use this for initialization
	void Start () {
	
	}		
		
	public override void StartExecute (Vector3 _position)
	{		
		ai_ = this.GetComponent<AIScript>();
		
		if (ai_.GetTarget())
		{
			target_ = ai_.GetTarget().GetComponent<AIScript>();
		}		
	}
	
	public override void Execute()
	{
		if (target_)
		{
			target_.SetHealth(0);
			print ("Attack done");
			HasCompleted = true;
			return;
		}
		print ("Failled to attacked");
		HasCompleted = true;
	}
	
	// Update is called once per frame
	void Update () {
		Execute();
	}
}