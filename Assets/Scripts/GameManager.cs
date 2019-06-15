using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static GameManager instance;

    public static GameManager getInstance()
    {
        return instance;
    }

    private List<BulletWay> bulletFlights;
    private int bulletIdentifier = 0;
    private Vector2[] playerPos;

    private CamBehaviour mainCam;
    private float camSize = 10;
    private int camMaxSize = 20;
    private int camMinSize = 10;
    private bool isWaitingToZoom = false;
    private bool shouldZoomIn = false;

    private float maxDistance = 0;

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
        Debug.Log(mainCam.shouldLookAtInterest() + " " + shouldZoomIn);
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
        if (bullet.distance > maxDistance)
        {
            maxDistance = bullet.distance;
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
    //Coroutines
    //---------------------------------------------------------------------------------------------------------------------

    IEnumerator ZoomIn()
    {
        yield return new WaitForSeconds(2);
        shouldZoomIn = true;
        isWaitingToZoom = false;
    }
}