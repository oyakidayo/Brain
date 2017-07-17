using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
public class achieve : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
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
        Social.ShowAchievementsUI();
        SceneManager.LoadScene("select");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
