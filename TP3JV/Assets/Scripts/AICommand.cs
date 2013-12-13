using UnityEngine;
using System.Collections;

public abstract class AICommand : MonoBehaviour {
	
	public bool HasCompleted = false;
	
	public abstract void StartExecute(Vector3 b);
	public abstract void Execute();
}
