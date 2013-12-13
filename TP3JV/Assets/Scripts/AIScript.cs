using UnityEngine;
using System.Collections;

public enum Commands {MOVE, MOVETORESSOURCE, COLLECT};

public class AIScript : MonoBehaviour {
	
	private Commands CurrentCommand;
	private AICommand CurrObject;
	private Transform Target;
	
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
				Vector3 tmp = (Random.insideUnitSphere * 3) + this.transform.position;
				tmp.y = this.transform.position.y;
				CurrObject.StartExecute(tmp);
			}
			break;
		case Commands.MOVETORESSOURCE:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_MoveTo");
				CurrObject = this.gameObject.GetComponent<AICommand>();
				Vector3 tmp = Target.position;
				tmp.y = this.transform.position.y;
				CurrObject.StartExecute(tmp);
			}
			break;
		case Commands.COLLECT:
			this.gameObject.AddComponent("AICommand_Collect");
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
				if (CurrentCommand == Commands.MOVETORESSOURCE)
					CurrentCommand = Commands.COLLECT;
				GameObject.Destroy(CurrObject);
			}
		}
		else
		{
			ChoseWhatToDo();
		}
	}
	
	public void SetTarget(Transform _target)
	{
		Target = _target;
	}
	
	public void SwitchTo(Commands _newCommand)
	{
		if (this.gameObject.GetComponent("AICommand") != null)
		{
			CurrObject = this.gameObject.GetComponent<AICommand>();
			GameObject.Destroy(CurrObject);
		}
		CurrentCommand = _newCommand;
	}
}
