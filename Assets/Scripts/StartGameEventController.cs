﻿using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StartGameEventController : MonoBehaviour
{
    private Vector3 startingPosition;
    private float gazing;
    private bool gazedAtObject;
    public float gazeTime;
    private bool triggered;

    void Start()
    {
        triggered = false;
        gazing = 0f;
        gazeTime = gazeTime != 0f ? gazeTime : 3f;
        gazedAtObject = false;
        startingPosition = transform.localPosition;
        SetGazedAt(false);
    }

    void Update()
    {
        if (gazing >= gazeTime & !triggered)
        {
            triggered = true;
            StartCoroutine(GameController.gameController.LoadLevel("GameJam"));
        }

        if (gazedAtObject)
        {
            gazing = gazing + (1 * Time.deltaTime);
        }

    }

    public void SetGazedAt(bool gazedAt)
    {
        gazedAtObject = gazedAt ? true : false;
    }

}
