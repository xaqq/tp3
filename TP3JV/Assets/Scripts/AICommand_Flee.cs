using UnityEngine;
using System.Collections;

public class AICommand_Flee : AICommand {
	
	private AIScript ai_;
	private Vector3 TargetPosition;
	private float Speed = 1.0f;
	
	// Use this for initialization
	void Start () {
	
	}		
	public override void StartExecute (Vector3 _position)
	{		
		ai_ = this.GetComponent<AIScript>();
		Speed = Speed + Speed * ai_.GetLevel() * 0.5f;
		print ("Start fleeing");
	}
	
	public override void Execute()
	{
		if (!ai_.Attacker)
		{
			HasCompleted = true;
			return;
		}
		TargetPosition = ai_.transform.position + ai_.Attacker.transform.forward;
						if (TargetPosition.x > ai_.MaxPos)
					TargetPosition.x = ai_.MaxPos;
				else if (TargetPosition.x < ai_.MinPos)
					TargetPosition.x = ai_.MinPos;
				if (TargetPosition.z > ai_.MaxPos)
					TargetPosition.z = ai_.MaxPos;
				else if (TargetPosition.z < ai_.MinPos)
					TargetPosition.z = ai_.MinPos;
		if ((this.transform.position - TargetPosition).magnitude <= Speed * Time.deltaTime)
		{
			this.transform.position = TargetPosition;
			HasCompleted = true;
			print("FLEE COMPLETE");
			return ;
		}
		TargetPosition.y = this.transform.position.y;
		this.transform.LookAt(TargetPosition);
		this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		Execute();
	}
}
