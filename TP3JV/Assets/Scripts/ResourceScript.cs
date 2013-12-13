using UnityEngine;
using System.Collections;

public class ResourceScript : AIScript {
	
	private int quantity_;
	
	public int collect(int qt)
	{
		print ("Has " + quantity_ + " available");
		  if (qt < quantity_)
    {
      quantity_ -= qt;
      return qt;
    }
  qt = quantity_;
  quantity_ = 0;
		return qt;
	}
	
	// Use this for initialization
	void Start () {
		quantity_ = 95;
	}
	
	
	void SpawnNewResource()
	{
		// how ??
	}
	
	// Update is called once per frame
	void Update () {
		if (quantity_ == 0)
		{
			print ("Im a dead resource");
			
			SpawnNewResource();
			
			Destroy(this.gameObject);
		}
	}
}
