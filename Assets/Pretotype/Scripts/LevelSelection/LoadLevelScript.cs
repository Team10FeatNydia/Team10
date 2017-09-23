using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelScript : MonoBehaviour {



    public void LoadScene(string LevelName)
    {
        Application.LoadLevel(LevelName);
    }
}
