using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class MiniMax : MonoBehaviour
{

    string st;
    string imagePath;
    private static int[,] board = new int[9, 9];
    private string[] pice = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
    private string sr = "r";//private Vector2 pice;
    private Vector3 v;
    private string sg = "g";
    private object player;
    private static int SEL = 10;
    private static int Com = 1;
    private static int Man = -1;
    private static int sy;
    private static int sx;
    private static int detY = 0;
    public int turn;
    public Text isScore;
    public Text isManScore;
    public Text isResult;
    public Text isLevel;
    int scoreCom;
    int scoreMan;
    // public GUIText cpuPt;
    // Use this for initialization
    void Start()
    {
        int x, y;
        for (x = 0; x <= 8; x++)
            for (y = 0; y <= 8; y++)
                board[x, y] = UnityEngine.Random.Range(-9, 10);
        init(0);
        if (Drop.level == 1) isLevel.text = "Elementary school";
        if (Drop.level == 2) isLevel.text = "Junior high school";
        if (Drop.level == 3) isLevel.text = "High school";
        if (Drop.level == 4) isLevel.text = "College";
        if (Drop.level == 5) isLevel.text = "Professor";
    }

    // Update is called once per frame
    void Update()
    {
        int s;
        Vector3 position;
        sy = 0;

        string cell;
        GameObject copied;
        v = transform.position;

        if (Input.GetMouseButtonUp(0))
        {

            position = Input.mousePosition;
            var pos = Camera.main.ScreenToViewportPoint(position);
            v.x = (int)((pos.x) * 8);
            v.y = (int)((pos.y) * 15 + 1.5f);
            sy = (int)v.y;
            sx = (int)v.x;
            if (detY != 0) { sy = detY; v.y = detY; }
            cell = "Prefabs/" + pice[board[(int)v.x, (int)v.y - 5] + 9] + sr;

            copied = Instantiate(Resources.Load(cell), new Vector3(v.x - 3.5f, v.y - 8.5f, 0), Quaternion.identity) as GameObject;
            copied.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
            v = copied.transform.position;
            copied.transform.SetSiblingIndex(1);
            copied.transform.position = v;



            //  init();
            // cursor(sx, sy);

            //sy = sy - 5;

            if (!isNotSel(sy - 5, Com))
            {
                playerDestroy();
                manCell(sx);

                scoreMan += board[sx, sy - 5];
                StartCoroutine(checkMan(sx, sy));

                isManScore.text = "YOU: " + scoreMan.ToString();
                turn = Man;

                StartCoroutine(turnCom());
            }


        }

    }
    void comCell(int sy)
    {
        int x;
        string cell;

        GameObject copied;
        for (x = 0; x <= 7; x++)
        {
            cell = "Prefabs/" + pice[board[x, sy - 5] + 9] + sg;

            copied = Instantiate(Resources.Load(cell), new Vector3(x - 3.5f, sy - 8.5f, 0), Quaternion.identity) as GameObject;
            copied.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
            v = copied.transform.position;
            //transform.SetSiblingIndex(z);
            copied.transform.position = v;
            //turn = 0;
        }
        // comDestroy();
    }



    void manCell(int sx)
    {

        string cell;
        GameObject copied;
        int y;

        for (y = 5; y <= 12; y++)
        {

            cell = "Prefabs/" + pice[board[(int)sx, (int)y - 5] + 9] + sr;

            copied = Instantiate(Resources.Load(cell), new Vector3(sx - 3.5f, y - 8.5f, 0), Quaternion.identity) as GameObject;
            copied.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f); v = copied.transform.position;
            //this.transform.SetSiblingIndex(z);
            copied.transform.position = v;
            //turn = 1;

        }
        // playerDestroy();
    }
    IEnumerator check(int x, int y)
    {

        yield return new WaitForSeconds(2f);
        string cell;
        GameObject copied;
        int point;
        cell = "Prefabs/19";
        copied = Instantiate(Resources.Load("Prefabs/19"), new Vector3(x - 3.5f, y - 8.5f, 0), Quaternion.identity) as GameObject;
        v = copied.transform.position;
        copied.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); copied.transform.position = v;
        point = board[x, y - 5];
        board[x, y - 5] = SEL;
        if (!isOver(x, y - 5))
            gameOver();
    }
    IEnumerator checkMan(int x, int y)
    {
        yield return new WaitForSeconds(1.5f);
        string cell;
        GameObject copied;
        int point;
        cell = "Prefabs/19";
        copied = Instantiate(Resources.Load("Prefabs/19"), new Vector3(x - 3.5f, y - 8.5f, 0), Quaternion.identity) as GameObject;
        v = copied.transform.position;
        copied.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); copied.transform.position = v;
        point = board[x, y - 5];
        board[x, y - 5] = SEL;
        if (!isOver(x, y - 5))
            gameOver();
    }
    void cursor(int x, int y)
    {
        // yield return new WaitForSeconds(3.5f);

        string cell;
        GameObject copied;
        cell = "Prefabs/cursorGreen";
        copied = Instantiate(Resources.Load("Prefabs/cursorGreen"), new Vector3(x - 3.5f, y - 8.5f, 0), Quaternion.identity) as GameObject;
        v = copied.transform.position;
        copied.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); copied.transform.position = v;
    }
    void cursorMan(int x, int y)
    {
        // yield return new WaitForSeconds(3.5f);

        string cell;
        GameObject copied;
        cell = "Prefabs/cursorGreen";
        copied = Instantiate(Resources.Load("Prefabs/cursor"), new Vector3(x - 3.5f, y - 8.5f, 0), Quaternion.identity) as GameObject;
        v = copied.transform.position;
        copied.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); copied.transform.position = v;
    }
    void init(int z)
    {
        int x, y, s;
        Vector3 position;
        y = 0;
        string cell;
        GameObject copied;
        v = transform.position;

        this.transform.SetSiblingIndex(z);
        for (x = 0; x <= 7; x++)
            for (y = 0; y <= 7; y++)
            {
                cell = "Prefabs/" + pice[board[x, y] + 9];

                copied = Instantiate(Resources.Load(cell), new Vector3(x - 3.5f, y - 3.5f, 0), Quaternion.identity) as GameObject;
                copied.transform.localScale = new Vector3(1.1f, 1.1f, 1.0f);
                v = copied.transform.position;
                copied.transform.position = v;
            }
    }
    void playerDestroy()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obs in obstacles)
        {
            Destroy(obs);
        }
    }
    void comDestroy()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Com");
        foreach (GameObject obs in obstacles)
        {
            Destroy(obs);
        }
    }
    int minMax(int MaxDepth)
    {
        int x, y, s, ss, nn, by, bx;
        int comSel;
        ss = -100000;
        s = -10;
        comSel = 0;
        nn = 0;
        bx = sx;
        int[,] ban = new int[8, 8];
        for (x = 0; x < 8; x++)
            for (y = 0; y < 8; y++)
                ban[x, y] = board[x, y];
        for (by = 0; by < 8; by++)
        {



            Debug.Log(sx);
            if (!isNotSel2(bx, by))
            {
                s = State(bx, by);
                s -= minMode(MaxDepth, by);

                if (s > ss)
                {
                    ss = s;
                    comSel = by;
                    nn = by;
                }
                for (x = 0; x < 8; x++)
                    for (y = 0; y < 8; y++)
                        board[x, y] = ban[x, y];
            }
        }
        return nn;
    }
    int minMode(int dep, int by)
    {
        int x, s, ss, y, bx;
        ss = -100000;
        s = 0;
        if (--dep == 0)
            return (0);
        int[,] ban = new int[8, 8];
        for (x = 0; x < 8; x++)
            for (y = 0; y < 8; y++)
                ban[x, y] = board[x, y];
        for (bx = 0; bx <= 7; bx++)
        {
            if (!isNotSel2(bx, by))
            {
                s = State(bx, by);
                if (!isOver(bx, by))
                {
                    for (x = 0; x < 8; x++)
                        for (y = 0; y < 8; y++)
                            board[x, y] = ban[x, y];
                    return s;
                }
                    s -= maxMode(dep, bx);
                if (s > ss)
                    ss = s;

                for (x = 0; x < 8; x++)
                    for (y = 0; y < 8; y++)
                        board[x, y] = ban[x, y];
            }
        }
        return ss;
    }
    int maxMode(int dep, int bx)
    {
        int x, s, ss, y, by;
        ss = -100000;
        s = 0;
        if (--dep == 0)
            return (0);
        int[,] ban = new int[8, 8];
        for (x = 0; x < 8; x++)
            for (y = 0; y < 8; y++)
                ban[x, y] = board[x, y];
        for (by = 0; by <= 7; by++)
        {
            if (!isNotSel2(bx, by))
            {
                s = State(bx, by);
                if (!isOver(bx, by))
                {
                    for (x = 0; x < 8; x++)
                        for (y = 0; y < 8; y++)
                            board[x, y] = ban[x, y];
                    return s;
                }
                s -= minMode(dep, by);
                if (s > ss)
                    ss = s;
            }
            for (x = 0; x < 8; x++)
                for (y = 0; y < 8; y++)
                    board[x, y] = ban[x, y];


        }
        return ss;
    }
    bool isNotSel(int n, int turn)
    {
        if (turn == Com)
            if (board[sx, n] == SEL)
                return true;
            else return false;

        else
                if (board[n, sy] == SEL)
            return true;
        else return false;

    }
    IEnumerator turnCom()
    {
        cursorMan(sx, sy);
        yield return new WaitForSeconds(3.5f);
        turn = Com;
        //comDestroy();
        sy = minMax(Drop.level);
        detY = sy + 5;
        cursorDestroy();
        cursorMan(sx, detY);

        // cursor(sx, sy + 5);
        StartCoroutine(check(sx, sy + 5));
        if (!isOver(sx, sy))
        {




            gameOver();

        }
        comDestroy();
        comCell(sy + 5);
        scoreCom += board[sx, sy];
        isScore.text = "CPU: " + scoreCom.ToString();
        // GameObject go = GameObject.Find("cpuPt");


        //manCell(sx);
    }
    int State(int bx, int by)
    {
        int ret;


        ret = board[bx, by];
        board[bx, by] = SEL;
        return ret;

    }
    bool isNotSel2(int x, int y)
    {

        if (board[x, y] == SEL)
            return true;
        else return false;


    }
    bool isOver(int x, int y)
    {
        int neslx, nesly;
        int i;
        neslx = 0;
        nesly = 0;
        for (i = 0; i <= 7; i++)
            if (board[x, i] == SEL)
                ++nesly;
        for (i = 0; i <= 7; i++)
            if (board[i, y] == SEL)
                ++neslx;
        if (neslx == 8 || nesly == 8)
            return false;
        else return true;
    }
    void gameOver()
    {

        //isResult;
        if (scoreMan == scoreCom)
            isResult.text = "DRAW!";
        if (scoreMan > scoreCom)
            isResult.text = "YOU WIN!";
        if (scoreMan < scoreCom)
            isResult.text = "CPU WIN!";
        StartCoroutine(title());

    }
    void cursorDestroy()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("cursor");
        foreach (GameObject obs in obstacles)
        {
            Destroy(obs);
        }
    }
    IEnumerator title()
    {


        yield return new WaitForSeconds(1.0f);
        st = "BRAIN TRAINING PUZZLE!!   CPU " + scoreCom + "pt vs YOU " + scoreMan + "pt Level is " + isLevel.text + "  " + isResult.text;
        SocialConnector.Share(st);
        Social.ReportScore(scoreCom - scoreMan, "CgkIhNOd2YYLEAIQAA", (bool success) =>
        {
            if (success)
            {
                //登録成功時の処理
            }
            else
            {
                //登録失敗時の処理
            }
        });
        Social.ShowLeaderboardUI();


        ((PlayGamesPlatform)Social.Active).IncrementAchievement(
                " CgkIhNOd2YYLEAIQAw", 1, (bool success) =>
                {
                    if (success)
                    {
                        //成功時の処理
                    }
                    else
                    {
                        //失敗時の処理
                    }
                });

        SceneManager.LoadScene("select");
    }
}
