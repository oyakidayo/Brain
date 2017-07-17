using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class achievement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Social.ShowLeaderboardUI();
        SceneManager.LoadScene("main");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
