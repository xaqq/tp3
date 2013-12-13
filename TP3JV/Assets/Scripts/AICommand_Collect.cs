using UnityEngine;
using System.Collections;

public class AICommand_Collect : AICommand {
	
	private AIScript ai_;
	float cooldown_ = 0;
	
	#region Methods
	
	public override void StartExecute (Vector3 _target)
	{
		ai_ = this.GetComponent<AIScript>();
	}
	
	public override void Execute ()
	{
		if (cooldown_ < 0.8f)
		{
			cooldown_ += Time.deltaTime;
		    return;
		}
		cooldown_ = 0;
		
		ResourceScript target = null;
		if (ai_.GetTarget())
		{
			target = ai_.GetTarget().GetComponent<ResourceScript>();
		}
		
		if (target)
		{
			int unitCollectQt = 10 * ai_.GetLevel();
			int qt = target.collect(unitCollectQt);
			ai_.AddRessource(qt);
			ai_.AddExperience(qt);
			print ("Collected " + qt + " resources. Now has "+ ai_.GetResourceQuantity());
			if (qt < unitCollectQt)
			HasCompleted = true;
		}
	}
	
	#endregion
	
	void Update()
	{
		Execute ();
	}
}
