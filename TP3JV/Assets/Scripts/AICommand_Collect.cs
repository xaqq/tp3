using UnityEngine;
using System.Collections;

public class AICommand_Collect : AICommand {
	
	#region Methods
	
	public override void StartExecute (Vector3 _target)
	{
	}
	
	public override void Execute ()
	{
		print ("YO IM COLLECTIN BABE");
	}
	
	#endregion
	
	void Update()
	{
		Execute ();
	}
}
