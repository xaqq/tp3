using UnityEngine;
using System.Collections;

public class AICommand_Chase : AICommand {
	
	private AIScript ai_;
	private Vector3 TargetPosition;
	private float Speed = 1.0f;
	
	// Use this for initialization
	void Start () {
	
	}		
	public override void StartExecute (Vector3 _position)
	{
		TargetPosition = _position;		
		ai_ = this.GetComponent<AIScript>();
		Speed = Speed + Speed * ai_.GetLevel() * 0.5f;
		print ("Start chasing");
	}
	
	public override void Execute()
	{
		TargetPosition = ai_.GetTarget().position;
		if ((this.transform.position - TargetPosition).magnitude <= Speed * Time.deltaTime)
		{
			this.transform.position = TargetPosition;
			HasCompleted = true;
			print("CHASE COMPLETE");
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
