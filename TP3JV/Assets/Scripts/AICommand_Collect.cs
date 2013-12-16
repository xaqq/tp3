using UnityEngine;
using System.Collections;

public class AICommand_Collect : AICommand {
	
	private AIScript AiComponent;
	float Cooldown = 0;
	
	#region Methods
	
	public override void StartExecute (Vector3 _target)
	{
		AiComponent = this.GetComponent<AIScript>();
	}
	
	public override void Execute ()
	{
		if (Cooldown < 0.8f)
		{
			Cooldown += Time.deltaTime;
		    return;
		}
		Cooldown = 0;
		
		ResourceScript target = null;
		if (AiComponent.GetTarget())
		{
			target = AiComponent.GetTarget().GetComponent<ResourceScript>();
		}
		
		if (target)
		{
			int unitCollectQt = 1 * AiComponent.GetLevel();
			int qt = target.collect(unitCollectQt);
			AiComponent.AddRessource();
			AiComponent.AddExperience(qt);
			if (qt < unitCollectQt)
				HasCompleted = true;
		}
		else
		{
			HasCompleted = true;
		}
	}
	
	#endregion
	
	void Update()
	{
		Execute ();
	}
}
