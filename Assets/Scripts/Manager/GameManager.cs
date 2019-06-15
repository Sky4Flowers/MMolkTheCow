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

    public static GameManager getInstance()
    {
        return instance;
    }

    private List<BulletWay> bulletFlights;
    private int bulletIdentifier = 0;
    private Vector2[] playerPos;

    private PlayerStat longestShotDistance = new PlayerStat();
    //TODO Playerstats setzen
    private PlayerStat bestAccuracy = new PlayerStat();
    private PlayerStat mostHealthDrain = new PlayerStat();
    private PlayerStat mostHealthLeft = new PlayerStat();
    private int teamHealth1 = 20;
    private int teamHealth2 = 20;

    private CamBehaviour mainCam;
    private float camSize = 10;
    private int camMaxSize = 20;
    private int camMinSize = 10;
    private bool isWaitingToZoom = false;
    private bool shouldZoomIn = false;

    private GameObject[] players = new GameObject[4];

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
                Debug.LogError("No moveable mainCamera found. Please add camera with CamBehaviour as main");
            }
        }
    }

    void Update()
    {
        if (mainCam.shouldLookAtInterest())
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
                if(camSize > camMinSize)
                {
                    camSize -= Time.deltaTime;
                }
            }
            else if(!isWaitingToZoom)
            {
                isWaitingToZoom = true;
                StartCoroutine(ZoomIn());
            }
        }

        if(teamHealth1 <= 0 || teamHealth2 <= 0)
        {
            finishGame();
        }
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
        //SpawnBehaviour.spawnPlayers(instance.players.Length);
        instance.teamHealth1 = 20;
        instance.teamHealth2 = 20;
        SceneManager.LoadScene(1);
        foreach(HealthBar g in Resources.FindObjectsOfTypeAll(typeof(HealthBar)) as HealthBar[])
        {
            g.enabled = true;
        }
    }

    public static GameObject getPlayerById(int playerId)
    {
        return instance.players[playerId];
    }

    public static int[] getTeamHealths()
    {
        int[] hps = new int[2];
        hps[0] = instance.teamHealth1;
        hps[1] = instance.teamHealth2;
        return hps;
    }

    public static void finishGame()
    {
        //TODO
        //Get Playerstats
        //calculate best badgestats
        SceneManager.LoadScene(2);
    }

    public static void goBackToMain()
    {
        SceneManager.LoadScene(0);
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
}