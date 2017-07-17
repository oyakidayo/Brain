using UnityEngine;
using System.Collections;

public class SocialC : MonoBehaviour
{

    string text;
    string URL;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    string imagePath
    {
        get
        {
            //保存先を指定
            return Application.persistentDataPath + "/image.png";
        }
    }
    public void _share()
    {
        StartCoroutine(Share());
    }
    public void _gameOverShare()
    {
        StartCoroutine(GameOverShare());
    }
    public IEnumerator Share()
    {

        //スクリーンショットを撮影
        Application.CaptureScreenshot("image.png");
        yield return new WaitForEndOfFrame();

        if (Application.platform == RuntimePlatform.Android)
        {
            text = "";
            URL = "";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            text = "";
            URL = "";
        }
        yield return new WaitForSeconds(1);
        SocialConnector.Share(text, URL, imagePath);
    }
    public IEnumerator GameOverShare()
    {

        Application.CaptureScreenshot("image.png");
        yield return new WaitForEndOfFrame();

        if (Application.platform == RuntimePlatform.Android)
        {
            text = "";
            URL = "";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            text = "";
            URL = "";
        }
        yield return new WaitForSeconds(1);
        SocialConnector.Share(text, URL, imagePath);
    }
}