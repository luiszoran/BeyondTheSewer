using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController gameController;
    private GameObject background;
    private GameObject radar;
    private GameObject flashLight;
    private GameObject player;


    private void Awake()
    {
        background = GameObject.FindGameObjectWithTag("Background");
        radar = GameObject.FindGameObjectWithTag("Radar");
        flashLight = GameObject.FindGameObjectWithTag("Light");
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = this;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadLevel("Menu"));
    }
	
    public IEnumerator LoadLevel(string sceneName)
    {
        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        PrepareLevel(sceneName);

        StartCoroutine(UnloadLevels(sceneName));
    }

    private void PrepareLevel(string sceneName)
    {
        if (sceneName == "Menu")
            MenuScene();
        else if (sceneName == "GameJam")
            GameScene();
    }

    public IEnumerator UnloadLevels(string levelToNotUnload)
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name != levelToNotUnload && SceneManager.GetSceneAt(i).name != "VRMain")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
            }
        }
    }

    private void MenuScene()
    {
        SetActive(background, true);
        SetActive(flashLight, false);
        SetActive(radar, false);
        MoveToPosition(Vector3.zero, Quaternion.identity);
    }

    private void GameScene()
    {
        SetActive(background, false);
        SetActive(flashLight, true);
        SetActive(radar, true);
        MoveToPosition(new Vector3(-0.12f, 0.87f, -5.51f), new Quaternion(0f, -180f, 0f, 0f));
    }

    private void MoveToPosition(Vector3 position, Quaternion rotation)
    {
        player.transform.position = position;
        player.transform.rotation = rotation;
    }


    private void SetActive(GameObject gameObject, bool state)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(state);
        }
    }
}
