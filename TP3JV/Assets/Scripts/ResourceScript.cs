using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour
{
	public int Quantity = 1;
	
	public int collect (int qt)
	{
		print ("Has " + Quantity + " available");
		if (qt < Quantity) {
			Quantity -= qt;
			return qt;
		}
		qt = Quantity;
		Quantity = 0;
		return qt;
	}
	
	// Use this for initialization
	void Start ()
	{
	}
	
	void SpawnNewResource ()
	{
		GameObject _newResource;
		System.Random rdn = new System.Random();
		Vector3 _randomPos = new Vector3(0,0.5f,0);
		
		_newResource = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Resource1"));
		_randomPos.x = rdn.Next (-50, 50);
		_randomPos.z = rdn.Next (-50, 50);
		_randomPos.y = 0.5f;
		_newResource.transform.position = _randomPos;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Quantity == 0) {
			print ("Im a dead resource");
			
			SpawnNewResource ();
			
			Destroy (this.gameObject);
		}
	}
}
