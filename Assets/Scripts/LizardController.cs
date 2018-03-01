using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class LizardController : MonoBehaviour {

	public enum State
	{
		Patrol,
		Chase,
		Escape,
		Scared
	}


	public NavMeshAgent navMeshAgent;
	public State currentState;
	public State remainState;
	public Transform[] wayPointList;
	public Transform chaseTarget;
	public Transform escapeTarget;

	public AudioSource audioSource;

	public AudioClip walk;
	public AudioClip run;
	public AudioClip pain;
	public AudioClip jump;
	public AudioClip attack;

	public float scareTime;
	public bool scared;

	public int chaseRadius;

	public Animator animator;
	int chaseHash = Animator.StringToHash("Chase");
	int scareHash = Animator.StringToHash("Scare");
	int escapedHash = Animator.StringToHash("Escaped");

	public int nextWayPoint;

	private bool aiActive;

	bool jumping = true;

	// Use this for initialization
	void Awake () {
		navMeshAgent = this.GetComponent<NavMeshAgent>();
		currentState = State.Patrol;
		animator = GetComponentInChildren<Animator> ();
		chaseTarget = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource.clip = walk;
		audioSource.Play ();
		scared = false;
		jumping = false;

		//navMeshAgent.destination = wayPointList[0].position;
		//navMeshAgent.isStopped = false;
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("DistanceToPlayer", (transform.position - chaseTarget.position).magnitude);
		if (currentState.Equals(State.Patrol)){
			Patrol ();
		}
		if (currentState.Equals(State.Chase)){
			Chase ();
		}
		if (currentState.Equals(State.Escape)){
			Escape ();
		}

		if ((chaseTarget.position - transform.position).magnitude < chaseRadius && currentState.Equals(State.Patrol)) {
			ChasePlayer ();
		}

		if ((chaseTarget.position - transform.position).magnitude > chaseRadius && currentState.Equals(State.Escape)) {
			currentState = State.Patrol;
			audioSource.clip = walk;
			audioSource.Play ();
		}

		if ((chaseTarget.position - transform.position).magnitude < 4 && currentState.Equals(State.Chase) && !jumping) {
			jumping = true;
			audioSource.clip = jump;
			audioSource.Play ();
			StartCoroutine ("End");
		}

		Debug.Log((chaseTarget.position - transform.position).magnitude);
		navMeshAgent.isStopped = scared;
	}

	private void Patrol()
	{
		navMeshAgent.destination = wayPointList [nextWayPoint].position;
		//navMeshAgent.isStopped = false;

		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending) 
		{
			nextWayPoint = (nextWayPoint + 1) % wayPointList.Length;
		}
	}

	private void Chase()
	{
		navMeshAgent.destination = chaseTarget.position;
		//navMeshAgent.isStopped = false;
	}

	private IEnumerator Scared()
	{
		navMeshAgent.speed = 0f;
		scared = true;
		yield return new WaitForSeconds (scareTime);
		scared = false;
		currentState = State.Escape;
		navMeshAgent.speed = 3.5f;
		audioSource.clip = run;
		audioSource.Play();


	}

	private IEnumerator End(){
		yield return new WaitForSeconds (1f);
        StartCoroutine(GameController.gameController.LoadLevel("Menu"));
    }

    private void Escape()
	{
		int furthestWaypoint = 0;
		/*for (int i = 1; i < wayPointList.Length; i++) {
			Transform waypoint = wayPointList [i];
			if ((wayPointList[i].position - transform.position).magnitude > (wayPointList[furthestWaypoint].position - transform.position).magnitude) {
				furthestWaypoint = i;
			}
		}*/
		escapeTarget = wayPointList[furthestWaypoint];
		nextWayPoint = furthestWaypoint;
		navMeshAgent.destination = escapeTarget.position;
		//navMeshAgent.isStopped = false;
	}

	/*void OnTriggerEnter(Collider other){
		//Debug.Log ("debug: " + other.tag);
		if (other.tag.Equals ("Player")) {
			chaseTarget = other.transform;
			animator.SetTrigger (chaseHash);
			currentState = State.Chase;
		}
	}*/

	void ChasePlayer(){
		animator.SetTrigger (chaseHash);
		audioSource.clip = run;
		audioSource.Play();
		currentState = State.Chase;
	}

	/*void OnTriggerExit(Collider other){
		//Debug.Log ("debug: " + other.tag);
		if (other.tag.Equals ("Player")) {
			chaseTarget = other.transform;
			animator.SetTrigger (escapedHash);
			currentState = State.Patrol;
		}
	}*/

	public void Hit(){
		if (!currentState.Equals (State.Scared)) {
			currentState = State.Scared;
			StartCoroutine ("Scared");

			animator.SetTrigger (scareHash);
			audioSource.clip = pain;

		}
	}
}
