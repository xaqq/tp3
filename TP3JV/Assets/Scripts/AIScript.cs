using UnityEngine;
using System.Collections;

public enum Commands {MOVE, MOVETORESSOURCE, MOVETOFORUM, COLLECT, ATTACK, CHASE, FLEE};

public class AIScript : MonoBehaviour {
	
	public Commands CurrentCommand;
	public SocietyHandler MySociety;
	protected AICommand CurrObject;
	protected Transform Target;
	public SoldierScript Attacker; // Attacker we flee from
	private int Level = 1;
	private int Experience = 0;
	public int Health = 100;
	public int MinPos;
	public int MaxPos;
	
	
	private bool HasResource = false;
	
	// Use this for initialization
	void Start () {
		CurrentCommand = Commands.MOVE;
	}
	
	public SocietyHandler GetSociety()
	{
		return MySociety;
	}
	public void SetSociety(SocietyHandler _society)
	{
		MySociety = _society;
	}
	
	public int GetHealth()
	{
		return Health;
	}
	
	public void SetHealth(int h)
	{
		Health = h;
	}
	
	public int GetLevel()
	{
		return Level;
	}
	
	public void AddLevel(int l)
	{
		Level += l;
		if (MySociety)
			MySociety.UnitLeveledUp(this);
	}
	
	public int GetExperience()
	{
		return Experience;
	}
	
	public void AddExperience(int qt)
	{
		Experience += qt;
		
		if ((Level == 1 && Experience > 3) ||
			(Level == 2 && Experience > 10))
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
		case Commands.MOVETOFORUM:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_MoveTo");
				CurrObject = this.gameObject.GetComponent<AICommand>();
				Vector3 tmp = MySociety.transform.position;
				tmp.y = this.transform.position.y;
				CurrObject.StartExecute(tmp);
			}
			break;
		case Commands.COLLECT:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_Collect");			
				CurrObject = this.gameObject.GetComponent<AICommand>();
				CurrObject.StartExecute(new Vector3());
			}
			break;
		case Commands.FLEE:	
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_Flee");			
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
		if (this.GetHealth() <= 0)
		{
			Destroy(this.gameObject);
			MySociety.AgentDead();
			return;
		}
		if (this.gameObject.GetComponent("AICommand") != null)
		{
			CurrObject = this.gameObject.GetComponent<AICommand>();
			if (CurrObject.HasCompleted)
			{
				if (CurrentCommand == Commands.MOVETORESSOURCE)
					CurrentCommand = Commands.COLLECT;
				else if (CurrentCommand == Commands.COLLECT)
					CurrentCommand = Commands.MOVETOFORUM;
				else if (CurrentCommand == Commands.MOVETOFORUM)
					CurrentCommand = Commands.MOVE;
				GameObject.Destroy(CurrObject);
			}
		}
		else
		{
			ChoseWhatToDo();
		}
	}
	
	public bool AgentHasResource()
	{
		return HasResource;
	}
	
	public void AddRessource()
	{
		HasResource = true;
	}
	
	public void GiveResourceToForum()
	{
		if (HasResource)
		{
			HasResource = false;
			MySociety.AddResources(1);
		}
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
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Ressource"))
		{
			SwitchTo (Commands.COLLECT);
		}
		else if (collision.gameObject.CompareTag("Faction_1") || collision.gameObject.CompareTag("Faction_2"))
		{
			MySociety.AddCollision();
		}
	}
	
	void OnCollisionExit(Collision collision) {
		if (collision == null || collision.gameObject == null)
			return;
		if (collision.gameObject.CompareTag("Faction_1") || collision.gameObject.CompareTag("Faction_2"))
		{
			if (MySociety)
				MySociety.RemoveCollision();
		}
	}
}
