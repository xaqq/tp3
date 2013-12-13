using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {
	
	bool PanelOpen = false;
	public TweenRotation ArrowTween;
	public TweenPosition PanelTween;
	public float Hauteur_Perspective = 10;
	public float Hauteur_FirstPerson = 0;
	public float Offset_FirstPerson = 0.5f;
	public float Hauteur_ThirdPerson = 4;
	public float Vertical_Sensibility = 1;
	public float Horizontal_Sensibility = 1;
	public GameObject Camera_Target;
	public enum Camera_Mode {
		ORTHOGRAPHIC,
		PERSPECTIVE,
		FIRST_PERSON,
		THIRD_PERSON
	};
	public Camera_Mode CameraMode = Camera_Mode.ORTHOGRAPHIC; 
	
	// Use this for initialization
	void Start () {

	}
	
	void UpdateOrthographic()
	{
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
		if ((CameraMode == Camera_Mode.PERSPECTIVE || CameraMode == Camera_Mode.THIRD_PERSON) && Input.GetAxis("Fire2") > 0)
		{
			float h = Input.GetAxis("Mouse X");
			float v = Input.GetAxis("Mouse Y");
			transform.Rotate(-v * Vertical_Sensibility, 0, 0);
			transform.RotateAround(Vector3.zero, Vector3.up, h * Horizontal_Sensibility);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		eventHandling();
		Screen.lockCursor = false;
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
}
