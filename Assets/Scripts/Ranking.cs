using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
public class Ranking : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log("login: " + success);
            if (success)
            {
                //サインイン成功
            }
            else
            {
                //サインイン失敗
            }
        });
        Social.ShowLeaderboardUI();
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIhNOd2YYLEAIQAA");
        SceneManager.LoadScene("select");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
