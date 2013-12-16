using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {
	
	bool PanelOpen = false;
	public TweenRotation ArrowTween;
	public TweenPosition PanelTween;
	bool InterfaceOpen = false;
	public TweenPosition InterfaceTween;
	public float Hauteur_Perspective = 10;
	public float Hauteur_FirstPerson = 0;
	public float Offset_FirstPerson = 0.5f;
	public float Hauteur_ThirdPerson = 4;
	public float Vertical_Sensibility = 1;
	public float Horizontal_Sensibility = 1;
	private GameObject Camera_Target;
	public enum Camera_Mode {
		ORTHOGRAPHIC,
		PERSPECTIVE,
		FIRST_PERSON,
		THIRD_PERSON
	};
	public Camera_Mode CameraMode = Camera_Mode.ORTHOGRAPHIC;
	public SocietyHandler Society1;
	
	
	int unitNumber = 0;
	int prevUnitNumber = -1;
	
	private Camera _cam; 
	
	// Use this for initialization
	void Start () {
		_cam = GetComponent<Camera>();
	}
	
	void UpdateOrthographic()
	{
		_cam.fieldOfView = 60;
		transform.position = new Vector3(0, 88, 0);
		transform.LookAt(Vector3.zero);
	}

	void UpdatePerspective()
	{
		if (Input.GetAxis("Fire2") > 0)
		{
			Screen.lockCursor = true;
		}
		transform.position = new Vector3(0, Hauteur_Perspective, 0);
	}

	void UpdateFirstPerson()
	{
		_cam.fieldOfView = 60;
		if (Camera_Target != null)
		{
			transform.position = Camera_Target.transform.position + new Vector3(0, Hauteur_FirstPerson, 0);
			transform.eulerAngles = Camera_Target.transform.eulerAngles;
			transform.Translate(Vector3.forward * Offset_FirstPerson);
		}
	}

	void UpdateThirdPerson()
	{
		if (Input.GetAxis("Fire2") > 0)
		{
			Screen.lockCursor = true;
		}
		if (Camera_Target != null)
		{
			transform.position = Camera_Target.transform.position + new Vector3(0, Hauteur_ThirdPerson, 0);
		}
	}

	void eventHandling()
	{
		if ((CameraMode == Camera_Mode.PERSPECTIVE || CameraMode == Camera_Mode.THIRD_PERSON))
		{
			if (Input.GetAxis("Fire2") > 0)
			{
			float h = Input.GetAxis("Mouse X");
			float v = Input.GetAxis("Mouse Y");
			transform.Rotate(-v * Vertical_Sensibility, 0, 0);
			transform.RotateAround(Vector3.zero, Vector3.up, h * Horizontal_Sensibility);
			}
			_cam.fov += Input.GetAxis("Mouse ScrollWheel") * 100;
			if (_cam.fov >= 70)
				_cam.fov = 70;
			if (_cam.fov <= 10)
				_cam.fov = 10;
		}
		if ((CameraMode == Camera_Mode.FIRST_PERSON || CameraMode == Camera_Mode.THIRD_PERSON))
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
			   unitNumber = (unitNumber + 1) % Society1.GetComponent<SocietyHandler>().GetNumberOfAgent();	
		       updateTarget();				
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (unitNumber <= 0)
					unitNumber = Society1.GetComponent<SocietyHandler>().GetNumberOfAgent();
				unitNumber = (unitNumber - 1) % Society1.GetComponent<SocietyHandler>().GetNumberOfAgent();
		        updateTarget();				
			}
		}
	}
	
	private void updateTarget()
	{
		int count = 0;
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Faction_1"))
		{
			if (count == unitNumber)
			{
				Camera_Target = o ;
		return ;
			}
			count++;
			}
	}

	// Update is called once per frame
	void Update ()
	{
		eventHandling();
		Screen.lockCursor = false;
		if (!Camera_Target)
			updateTarget();
		switch (CameraMode)
		{
			case Camera_Mode.ORTHOGRAPHIC:
			UpdateOrthographic();
			break;
			case Camera_Mode.PERSPECTIVE:
			UpdatePerspective();
			break;
			case Camera_Mode.FIRST_PERSON:
			UpdateFirstPerson();
			break;
			case Camera_Mode.THIRD_PERSON:
			UpdateThirdPerson();
			break;
		}
	}
	


	void ToggleCameraPanel()
	{
		if (PanelOpen)
		{
			ArrowTween.Play(false);
			PanelTween.Play(false);
			PanelOpen = false;
		}
		else
		{
			ArrowTween.Play(true);
			PanelTween.Play(true);
			PanelOpen = true;
			
		}
	}
	
	void SwitchToFirst()
	{
		if (CameraMode != Camera_Mode.FIRST_PERSON)
			CameraMode = Camera_Mode.FIRST_PERSON;
	}
	
	void SwitchToThird()
	{
		if (CameraMode != Camera_Mode.THIRD_PERSON)
			CameraMode = Camera_Mode.THIRD_PERSON;
	}
	
	void SwitchToOrtho()
	{
		if (CameraMode != Camera_Mode.ORTHOGRAPHIC)
			CameraMode = Camera_Mode.ORTHOGRAPHIC;
	}
	
	void SwitchToPersp()
	{
		if (CameraMode != Camera_Mode.PERSPECTIVE)
			CameraMode = Camera_Mode.PERSPECTIVE;
	}
	
	void SwitchToInterface()
	{
		if (InterfaceOpen)
		{
			InterfaceTween.Play(false);
			InterfaceOpen = false;
		}
		else
		{
			InterfaceTween.Play(true);
			InterfaceOpen = true;
			
		}
	}
}
