using UnityEngine;
using System.Collections;

public class AICommand_MoveTo : AICommand {
	
	private Vector3 TargetPosition;
	private float Speed = 1.0f;
	
	#region Methods
	
	public override void StartExecute (Vector3 _position)
	{
		TargetPosition = _position;
	}
	
	public override void Execute ()
	{
		if ((this.transform.position - TargetPosition).magnitude <= Speed * Time.deltaTime)
		{
			this.transform.position = TargetPosition;
			HasCompleted = true;
			return ;
		}
		TargetPosition.y = this.transform.position.y;
		this.transform.LookAt(TargetPosition);
		this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
	}
	
	#endregion
	
	void Update()
	{
		Execute ();
	}
}
