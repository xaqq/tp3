using UnityEngine;
using System.Collections;

public class SoldierScript : AIScript {
		
	// Use this for initialization
	void Start () {
		CurrentCommand = Commands.MOVE;
	}
	
	void ChoseWhatToDo()
	{
		switch (CurrentCommand)
		{
		case Commands.MOVE:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_MoveTo");
				CurrObject = this.gameObject.GetComponent<AICommand>();
				Vector3 tmp = (Random.insideUnitSphere * 15) + this.transform.position;
				if (tmp.x > MaxPos)
					tmp.x = MaxPos;
				else if (tmp.x < MinPos)
					tmp.x = MinPos;
				if (tmp.z > MaxPos)
					tmp.z = MaxPos;
				else if (tmp.z < MinPos)
					tmp.z = MinPos;
				tmp.y = this.transform.position.y;
				CurrObject.StartExecute(tmp);
			}
			break;
		case Commands.CHASE:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_Chase");
				CurrObject = this.gameObject.GetComponent<AICommand>();
				Vector3 tmp = Target.position;
				tmp.y = this.transform.position.y;
				CurrObject.StartExecute(tmp);
			}
			break;
		case Commands.ATTACK:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_Attack");			
				CurrObject = this.gameObject.GetComponent<AICommand>();
				CurrObject.StartExecute(new Vector3());
			}
			break;
		default:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
			if (this.gameObject.GetComponent("AICommand") != null)
		{
			CurrObject = this.gameObject.GetComponent<AICommand>();
			if (CurrObject.HasCompleted)
			{
				if (CurrentCommand == Commands.CHASE)
					CurrentCommand = Commands.ATTACK;
				else if (CurrentCommand == Commands.ATTACK)
				{
					CurrentCommand = Commands.MOVE;
				}
				GameObject.Destroy(CurrObject);
			}
		}
		else
		{
			ChoseWhatToDo();
		}
	}
	
	
	void OnCollisionEnter(Collision collision) {
		if ((collision.gameObject.CompareTag("Faction_1") && this.CompareTag("Faction_2")) ||
			(this.CompareTag("Faction_1") && collision.gameObject.CompareTag("Faction_2")))
		{
			SetTarget(collision.gameObject.transform);
			SwitchTo (Commands.ATTACK);
		}
		else
		{
			//print("PAS UNE RESSOURCE");
		}
	}
}
