using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    private Camera movedCam;
    private ICamInterest[] interestingObjects = new ICamInterest[1];

    // Start is called before the first frame update
    void Start()
    {
        movedCam = GetComponent<Camera>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Interesting");
        interestingObjects = new ICamInterest[objects.Length];
        for(int i = 0; i < objects.Length; i++)
        {
            interestingObjects[i] = objects[i].GetComponent<ICamInterest>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Something?
        GameManager.getInstance().updatePlayerPosById(interestingObjects[0].getPosition(), 0);
        GameManager.getInstance().updatePlayerPosById(interestingObjects[1].getPosition(), 1);
        GameManager.getInstance().updatePlayerPosById(interestingObjects[2].getPosition(), 2);
        GameManager.getInstance().updatePlayerPosById(interestingObjects[3].getPosition(), 3);
    }

    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, GameManager.getInstance().getPlayerMid(), Time.deltaTime * 5);
        movedCam.orthographicSize = GameManager.getInstance().getCameraSize();
    }

    public bool shouldLookAtInterest()
    {
        foreach (ICamInterest interest in interestingObjects)
        {
            if (Vector3.Distance(interest.getPosition(), transform.position) >= GameManager.getInstance().getCameraSize())
            {
                return true;
            }
        }
        return false;
    }
}