using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	CharacterController characterController;
	public Camera camera;

	public float speed = 6f;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Awake () {
		characterController = GetComponent<CharacterController> ();
		//characterController.attachedRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
		//characterController.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
		//characterController.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 eulerAngles = camera.transform.rotation.eulerAngles;
		moveDirection = new Vector3(Input.GetAxis("Horizontal") * Mathf.Sin(eulerAngles.y * Mathf.Deg2Rad),0,Input.GetAxis("Vertical")* Mathf.Cos(eulerAngles.y * Mathf.Deg2Rad));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;
		Debug.Log (moveDirection);

		//camera.transform.localRotation = new Quaternion ();
		characterController.Move (moveDirection * Time.deltaTime);
	}
}
