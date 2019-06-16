using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    struct BulletWay
    {
        public Vector3 lastPos;
        public float distance;
        public int id;
        public int hits;
        private int playerId;

        public BulletWay(Vector3 _pos, int _id, int _playerId)
        {
            lastPos = _pos;
            distance = 0;
            id = _id;
            hits = 0;
            playerId = _playerId;
        }

        public int getPlayerId()
        {
            return playerId;
        }
    }

    struct PlayerStat
    {
        public int playerId;
        public int statId;
        public float points;
    }

    private static GameManager instance;
    private static bool isSwitching = false;

    public static GameManager getInstance()
    {
        return instance;
    }

    private GameObject canvas;

    private List<BulletWay> bulletFlights;
    private int bulletIdentifier = 0;
    private Vector2[] playerPos;

    private PlayerStat longestShotDistance = new PlayerStat();
    //TODO Playerstats setzen
    private PlayerStat bestAccuracy = new PlayerStat();
    private PlayerStat mostHealthDrain = new PlayerStat();
    private PlayerStat mostHealthLeft = new PlayerStat();
    private int teamHealth1 = 10;
    private int teamHealth2 = 10;
    private int teamPasses1 = 0;
    private int teamPasses2 = 0;
    private int teamHits1 = 0;
    private int teamHits2 = 0;

    private CamBehaviour mainCam;
    private bool hasMainCam = true;
    private float camSize = 10;
    private int camMaxSize = 20;
    private int camMinSize = 10;
    private bool isWaitingToZoom = false;
    private bool shouldZoomIn = false;

    private GameObject[] players = new GameObject[4];

    private GameObject timerPrefab;
    private CountDown startTimer;
    private bool gameHasFinished = false;

    void Start()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            bulletFlights = new List<BulletWay>();
            playerPos = new Vector2[4];

            mainCam = Camera.main.GetComponent<CamBehaviour>();
            if (!mainCam)
            {
                hasMainCam = false;
                Debug.LogError("No moveable mainCamera found. Please add camera with CamBehaviour as main");
            }

            GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        if (hasMainCam && mainCam.shouldLookAtInterest())
        {
            shouldZoomIn = false;
            if (camSize < camMaxSize)
            {
                camSize += Time.deltaTime;
                Debug.Log(camSize);
            }
        }
        else
        {
            if (shouldZoomIn)
            {
                if (camSize > camMinSize)
                {
                    camSize -= Time.deltaTime;
                }
            }
            else if (!isWaitingToZoom)
            {
                isWaitingToZoom = true;
                StartCoroutine(ZoomIn());
            }
        }

        if (!gameHasFinished && (teamHealth1 <= 0 || teamHealth2 <= 0))
        {
            gameHasFinished = true;
            finishGame();
        }

        //Presentation-Shortcuts

        if (Input.GetKeyDown(KeyCode.Q) == true)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.W) == true)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            SceneManager.LoadScene(2);
        }
    }

    public bool isTimerActive()
    {
        return startTimer && startTimer.isActiveAndEnabled;
    }

    //---------------------------------------------------------------------------------------------------------------------
    //Camera Position - Methods
    //---------------------------------------------------------------------------------------------------------------------

    public Vector3 getPlayerMid()
    {
        float x = 0, y = 0;
        foreach (Vector2 v in playerPos)
        {
            x += v.x;
            y += v.y;
        }
        return new Vector3(x / playerPos.Length, y / playerPos.Length, -10);
    }

    public void updatePlayerPosById(Vector3 pos, int id)
    {
        playerPos[id] = new Vector2(pos.x, pos.y);
    }

    public float getCameraSize()
    {
        return camSize;
    }

    //---------------------------------------------------------------------------------------------------------------------
    //Bullet Paths - Methods
    //---------------------------------------------------------------------------------------------------------------------

    public int addBulletForId(Vector3 pos, int playerId)
    {
        BulletWay newBullet = new BulletWay(pos, bulletIdentifier++, playerId);
        bulletFlights.Add(newBullet);
        return newBullet.id;
    }

    public void updateBulletPath(Vector3 pos, int id)
    {
        BulletWay bullet = getBulletById(id);
        bullet.distance += Vector3.Distance(bullet.lastPos, pos);
        bullet.lastPos = pos;
        bullet.hits++;
    }

    public void killBullet(int id)
    {
        BulletWay bullet = getBulletById(id);
        if (bullet.distance > longestShotDistance.points)
        {
            longestShotDistance.playerId = bullet.getPlayerId();
            longestShotDistance.points = bullet.distance;
        }
        bulletFlights.Remove(bullet);
    }

    private BulletWay getBulletById(int id)
    {
        foreach (BulletWay bullet in bulletFlights)
        {
            if (bullet.id == id)
            {
                return bullet;
            }
        }
        return new BulletWay();
    }

    //---------------------------------------------------------------------------------------------------------------------
    //Switch Scenes
    //---------------------------------------------------------------------------------------------------------------------

    public static void startGame()
    {
        if (isSwitching)
        {
            return;
        }
        instance.StartCoroutine("SceneSwitchDelay");

        instance.gameHasFinished = false;
        instance.teamHealth1 = 20;
        instance.teamHealth2 = 20;
        instance.teamPasses1 = 0;
        instance.teamPasses2 = 0;
        instance.teamHits1 = 0;
        instance.teamHits2 = 0;
        SceneManager.LoadScene(1);
        instance.canvas = GameObject.Find("mainCanvas");
        instance.startTimer = Instantiate(instance.timerPrefab, instance.canvas.transform).GetComponent<CountDown>();
        instance.startTimer.run = true;
    }

    public static void finishGame()
    {
        if (isSwitching)
        {
            return;
        }
        instance.StartCoroutine("SceneSwitchDelay");
        //TODO
        //Get Playerstats
        //calculate best badgestats
        SceneManager.LoadScene(2);
    }

    public static void goBackToMain()
    {
        if (isSwitching)
        {
            return;
        }
        instance.StartCoroutine("SceneSwitchDelay");
        SceneManager.LoadScene(0);
    }

    //---------------------------------------------------------------------------------------------------------------------
    //Helpers & Getters
    //---------------------------------------------------------------------------------------------------------------------

    public static void reduceTeamHealth(int teamId)
    {
        if (teamId == 0)
        {
            instance.teamHits2++;
            instance.teamHealth1--;
        }
        else
        {
            instance.teamHits1++;
            instance.teamHealth2--;
        }
    }

    public void onPlayerHit(int hittedTeam) // 0-1
    {
        if(hittedTeam == 0)
        {
            teamHits2++;
        }
        else
        {
            teamHits1++;
        }
    }

    public void onPlayerPass(int passingTeam) // 0-1
    {
        if (passingTeam == 0)
        {
            teamPasses1++;
        }
        else
        {
            teamPasses2++;
        }
    }

    public static GameObject getPlayerById(int playerId)
    {
        return instance.players[playerId];
    }

    public static int[] getTeamHealths()
    {
        int[] hps = { instance.teamHealth1, instance.teamHealth2 };
        return hps;
    }

    public static int getWinningTeam() // 1-2
    {
        return instance.teamHealth1 > instance.teamHealth2 ? 1 : 2;
    }

    public static TeamScore getTeamScoreOf(int teamID) // 1-2
    {
        if(teamID == 1)
        {
            return new TeamScore(1, calculateTeamScore(1), instance.teamHits1, instance.teamPasses1);
        }
        else
        {
            return new TeamScore(2, calculateTeamScore(2), instance.teamHits2, instance.teamPasses2);
        }
    }

    private static int calculateTeamScore(int teamId) // 1-2
    {
        if (teamId == 1)
        {
            return getWinningTeam() == 1 ? 15 : instance.teamHits1 + instance.teamPasses1 * 2;
        }
        else
        {
            return getWinningTeam() == 2 ? 15 : instance.teamHits2 + instance.teamPasses2 * 2;
        }
    }

    public static PlayerScore getPlayerScore(int id) // 1-4
    {
        return new PlayerScore(id, id / 2, 0, 0); //TODO
    }

    //---------------------------------------------------------------------------------------------------------------------
    //Coroutines
    //---------------------------------------------------------------------------------------------------------------------

    IEnumerator ZoomIn()
    {
        yield return new WaitForSeconds(2);
        shouldZoomIn = true;
        isWaitingToZoom = false;
    }

    IEnumerator SceneSwitchDelay()
    {
        isSwitching = true;
        yield return new WaitForSeconds(1);
        isSwitching = false;
    }
}