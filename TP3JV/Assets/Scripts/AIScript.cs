using UnityEngine;
using System.Collections;

public enum Commands {MOVE, MOVETORESSOURCE, COLLECT};

public class AIScript : MonoBehaviour {
	
	private Commands CurrentCommand;
	private AICommand CurrObject;
	protected Transform Target;
	private int level_ = 1;
	private int experience_ = 0;
	public int health_ = 100;
	
	private int resourceQuantity_ = 0;
	
	// Use this for initialization
	void Start () {
		CurrentCommand = Commands.MOVE;
	}
	
	
	public int GetHealth()
	{
		return health_;
	}
	
	public int GetLevel()
	{
		return level_;
	}
	
	public void AddLevel(int l)
	{
		level_ += l;
	}
	
	public int GetExperience()
	{
		return experience_;
	}
	
	public void AddExperience(int qt)
	{
		experience_ += qt;
		
		if ((level_ == 1 && experience_ > 50) ||
			(level_ == 2 && experience_ > 150))
			AddLevel(1);
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
			CurrObject = this.gameObject.GetComponent<AICommand>();
			CurrObject.StartExecute(new Vector3());
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
				else if (CurrentCommand == Commands.COLLECT)
					CurrentCommand = Commands.MOVE;
				GameObject.Destroy(CurrObject);
			}
		}
		else
		{
			ChoseWhatToDo();
		}
	}
	
	public int GetResourceQuantity()
	{
		return resourceQuantity_;
	}
	
	public void AddRessource(int qt)
	{
		resourceQuantity_ += qt;
	}
	
	
	public void SetTarget(Transform _target)
	{
		Target = _target;
	}
	
	public Transform GetTarget()
	{
		return Target;
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
