using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarIconController : MonoBehaviour {

	public Transform target;
	public Transform player;

	public AudioSource audioSource;

	public AudioClip beep1;
	public AudioClip beep2;
	public AudioClip beep3;

	float maxVal = 0.05f;
	int radius = 10;

	void Awake(){
        target = GameObject.FindGameObjectWithTag("Lizard").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("RadarRefresh");
		StartCoroutine ("Beeps");
	}

	// Use this for initialization
	void Update () {
		
		//refreshPosition ();
		if ((target.position - player.position).magnitude > 20) {
			audioSource.clip = beep1;

		} else {
			if ((target.position - player.position).magnitude > 12) {
				audioSource.clip = beep2;
				//audioSource.Play();
			} else {
				audioSource.clip = beep3;
				//audioSource.Play();
			}
		}
	}
	
	IEnumerator RadarRefresh(){
		while (true) {
			

			//transform.position = target.position;
			yield return new WaitForSeconds (0.5f);
			refreshPosition ();
		}
	}

	IEnumerator Beeps(){
		while (true) {

			audioSource.Play();
			//transform.position = target.position;
			yield return new WaitForSeconds (0.5f);

		}
	}

	private void refreshPosition (){
		
		float posZ = transform.localPosition.z;
		Vector2 refPos = new Vector2(target.position.x - player.position.x, target.position.z - player.position.z);


		if (refPos.magnitude < 10) {
			Vector3 temp = new Vector3(maxVal * refPos.x,maxVal * refPos.y, posZ);
			transform.localPosition = temp;

			//transform.position.x = maxVal / refPos.x;
			//transform.position.y = maxVal / refPos.y;
		} else {
			refPos.Normalize ();
			refPos.Scale(new Vector2(radius,radius));
			transform.localPosition = new Vector3(maxVal * refPos.x,maxVal * refPos.y,posZ);

			//transform.position.x = maxVal / refPos.x;
			//transform.position.y = maxVal / refPos.y;
		}
	}
}
