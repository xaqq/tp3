using UnityEngine;
using System.Collections;

public enum Commands {MOVE, MOVETORESSOURCE, COLLECT};

public class AIScript : MonoBehaviour {
	
	private Commands CurrentCommand;
	private SocietyHandler MySociety;
	private AICommand CurrObject;
	protected Transform Target;
	private int Level = 1;
	private int Experience = 0;
	public int Health = 100;
	public int MinPos;
	public int MaxPos;
	
	private int RessourceQuantity = 0;
	
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
		print (_society.gameObject.name);
		MySociety = _society;
	}
	
	public int GetHealth()
	{
		return Health;
	}
	
	public int GetLevel()
	{
		return Level;
	}
	
	public void AddLevel(int l)
	{
		Level += l;
	}
	
	public int GetExperience()
	{
		return Experience;
	}
	
	public void AddExperience(int qt)
	{
		Experience += qt;
		
		if ((Level == 1 && Experience > 50) ||
			(Level == 2 && Experience > 150))
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
		case Commands.COLLECT:
			if (this.gameObject.GetComponent("AICommand") == null)
			{
				this.gameObject.AddComponent("AICommand_Collect");			
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
	
	public int GetRessourceQuantity()
	{
		return RessourceQuantity;
	}
	
	public void AddRessource(int qt)
	{
		RessourceQuantity += qt;
		MySociety.AddResources(qt);
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
		else
		{
			print("PAS UNE RESSOURCE");
		}
	}
}
