using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Linq;

public class Drop : MonoBehaviour
{

    //連携するGameObject

    public static int level;
    public ToggleGroup toggleGroup;
    public static string selectedLabel;
    public static string Level1 = "Elementary school";
    public static string Level2 = "Junior high school";
    public static string Level3 = "High school";
    public static string Level4 = "College";
    public static string Level5 = "Professor";
    // Use this for initialization

    public void onClick()
    {
        //Get the label in activated toggles

        string selectedLabel = toggleGroup.ActiveToggles()
            .First().GetComponentsInChildren<Text>()
            .First(t => t.name == "Label").text;
        // isLevel.text = selectedLabel;
        Debug.Log("selected " + selectedLabel);
        if (Level1 == selectedLabel) level = 1;
        if (Level2 == selectedLabel) level = 2;
        if (Level3 == selectedLabel) level = 3;
        if (Level4 == selectedLabel) level = 4;
        if (Level5 == selectedLabel) level = 5;
        //if (TitleManager.rankpark)
        Debug.Log("selected " + selectedLabel);
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
        SceneManager.LoadScene("main");
    }
    public void OnRanking()
    {
        SceneManager.LoadScene("rank");
        }
    public void OnAchivement()
    {
        SceneManager.LoadScene("jisseki");
    }
}