using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    private float cameraSpeed = 3.0f;

    public GameObject LevelButtonPrefab;
    public GameObject LevelButtonContainer;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    private void Awake()
    {
        SoundManagerScript.Instance.PlayBGM(AudioClipID.BGM_TEST);
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");

        foreach (Sprite thumbnail in thumbnails)
        {
            Debug.Log("load");
            GameObject container = Instantiate(LevelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(LevelButtonContainer.transform, false);

            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(() => loadLevel(sceneName));
        }
    }

    private void Update()
    {
        if (cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, cameraSpeed * Time.deltaTime);
        }
    }

    void loadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }
}
