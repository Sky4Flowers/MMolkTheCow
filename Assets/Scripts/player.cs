﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float playerSpeed;
    int lastKey;
    bool movable;
    public int playerID;
    public int teamNumber;
    Rigidbody rb;
    Animator anim;
    public GameObject weapon;
    public GameObject shield;
    Vector3 itemPosition;
    int controllerID;
    bool armed;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        movable = true;
        rb = GetComponent<Rigidbody>();
        itemPosition = new Vector3(1.0f, 0.0f, 0.0f);
        weapon.transform.position = transform.position + itemPosition;
        shield.transform.position = transform.position + itemPosition;
        weapon.SetActive(false);
        armed = weapon.activeSelf;
        /*for(int i = 0; i < 4; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]);
        }*/
        //Suche nach dem manuell einzustellenden Controller, falls Probleme mit dem Input Manager auftreten
        if(playerID == 3)
        {
            int controllerNum = Input.GetJoystickNames().Length;
            for (int i = 0; i < controllerNum; i++)
            {
                if(Input.GetJoystickNames()[i].Equals("USB Joystick     "))
                {
                    Debug.Log("Joystick found");
                    controllerID = i+1;
                }
            }
        }
        //Setzen des Layers je nach Teamnummer
        if(teamNumber == 1)
        {
            gameObject.layer = 8;
        }else if (teamNumber == 2)
        {
            gameObject.layer = 9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Change Item
        if (InputManager.Instance.getButtonDown(playerID, InputManager.ButtonType.LeftShoulder))
        {
            if (weapon.activeSelf)
            {
                weapon.SetActive(false);
                armed = false;
                shield.SetActive(true);
            }
            else if (shield.activeSelf)
            {
                shield.SetActive(false);
                armed = true;
                weapon.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        /*float amtToMove = Input.GetAxis("Horizontal") * playerSpeed * 1000 * Time.deltaTime;
        float amtToMove2 = Input.GetAxis("Vertical") * playerSpeed * 1000 * Time.deltaTime;*/
        float amtToMove = 0.0f;
        float amtToMove2 = 0.0f;
        //Ggf manuelle Anpassung an einen Controller, der nicht mit dem Input Manager arbeitet
        if (playerID == 3)
        {
            amtToMove = Input.GetAxis("Horizontal" + controllerID) * playerSpeed * 100 * Time.deltaTime;
            amtToMove2 = Input.GetAxis("Vertical" + controllerID) * playerSpeed * 100 * Time.deltaTime;
            Debug.Log("0: " + Input.GetAxis("Horizontal"));
            Debug.Log("1: " + Input.GetAxis("Horizontal1"));
            Debug.Log("2: " + Input.GetAxis("Horizontal2"));
            Debug.Log("3: " + Input.GetAxis("Horizontal3"));
            Debug.Log("4: " + Input.GetAxis("Horizontal4"));
        }
        else
        {
            amtToMove = InputManager.Instance.getLeftStick(playerID).x * playerSpeed / 2 * Time.deltaTime;
            amtToMove2 = InputManager.Instance.getLeftStick(playerID).y * playerSpeed / 2 * Time.deltaTime;
        }

        if (movable)
        {
            if (amtToMove > 0 && Mathf.Abs(amtToMove) > Mathf.Abs(amtToMove2)) //Das Mathf.Abs könnte evtl für Analog-Sticks wichtig sein
            {
                //anim.SetInteger("State", 4);
                lastKey = 4;

            }

            if (amtToMove < 0 && Mathf.Abs(amtToMove) > Mathf.Abs(amtToMove2))
            {
                //anim.SetInteger("State", 2);
                lastKey = 2;

            }
            if (amtToMove2 < 0 && Mathf.Abs(amtToMove2) > Mathf.Abs(amtToMove))
            {
                //anim.SetInteger("State", 1);
                lastKey = 1;

            }
            if (amtToMove2 > 0 && Mathf.Abs(amtToMove2) > Mathf.Abs(amtToMove))
            {
                //anim.SetInteger("State", 3);
                lastKey = 3;

            }

            //transform.Translate(Vector2.right * amtToMove);
            //transform.Translate(Vector2.up * amtToMove2);

            Vector3 movement = new Vector3(amtToMove, amtToMove2, 0.0f);
            /*if (playerID == 3)
            {
                rb.AddForce(movement);
                rb.velocity = Vector3.zero;
            }
            else
            {*/
                rb.MovePosition(rb.position + movement);
            //}

        }
        //Positionieren des Items, abhängig von der Ausrichtung des rechten Sticks
        if (Mathf.Abs(InputManager.Instance.getRightStick(playerID).x) + Mathf.Abs(InputManager.Instance.getRightStick(playerID).y) >= 0.1f)
        {
            itemPosition = new Vector3(InputManager.Instance.getRightStick(playerID).x, InputManager.Instance.getRightStick(playerID).y, 0.0f);
            itemPosition = Vector3.Normalize(itemPosition);
            weapon.transform.position = transform.position + itemPosition;
            shield.transform.position = transform.position + itemPosition;
        }

        if (armed)//Überprüfung ob Waffe ausgerüstet ist und geschossen werden kann
        {
            if (InputManager.Instance.getButtonDown(playerID, InputManager.ButtonType.RightShoulder))
            {
                GameObject projectile = (GameObject)Instantiate(bullet, weapon.transform.position, Quaternion.identity);
                projectile.layer = gameObject.layer;
                projectile.GetComponent<Projectile>().SetDirection(itemPosition);
            }
        }

        if (amtToMove == 0 && amtToMove2 == 0)
        {
            //anim.SetInteger("State", -1 * lastKey);
        }

        if (movable)
        {
          /*  if (Input.GetKeyDown(KeyCode.E)) //Überprüft auf Objekte mit denen Interagiert werden kann
            {
                //Hier ne Anfrage, ob man interagieren kann mit etwas, z.B. einem NPC
                //Die Richtung des Blicks ergibt sich über den State:
                //-1 oder 1 : -y
                //-2 oder 2 : -x
                //-3 oder 3 : y
                //-4 oder 4 : x
                //Der Raycast soll dann bei dem anderen Objekt die Methode "Action" auslösen

                RaycastHit hit;
                Vector2 raydirect = new Vector2(0, 0);
                switch (lastKey)
                {
                    case 1:
                        raydirect = Vector2.down;
                        break;
                    case 2:
                        raydirect = Vector2.left;
                        break;
                    case 3:
                        raydirect = Vector2.up;
                        break;
                    case 4:
                        raydirect = Vector2.right;
                        break;
                    case -1:
                        raydirect = Vector2.down;
                        break;
                    case -2:
                        raydirect = Vector2.left;
                        break;
                    case -3:
                        raydirect = Vector2.up;
                        break;
                    case -4:
                        raydirect = Vector2.right;
                        break;
                }

                if (Physics.Raycast(transform.position, raydirect, out hit, 1.7f))
                {
                    if (hit.collider.gameObject.tag == "World")
                    {
                        hit.collider.gameObject.GetComponent<Interaction>().Action(this.gameObject);

                    }

                }

                if (Physics.Raycast(transform.position, raydirect, out hit, 1.7f))
                {
                    if (hit.collider.gameObject.tag == "Dead" && alive == false)
                    {
                        hit.collider.gameObject.GetComponent<Interaction>().Action(this.gameObject);

                    }

                }

            }
            if (Input.GetKeyDown(KeyCode.T) )
            {
                
            }*/
			
        }
        rb.velocity = Vector3.zero;

    }

    public void setMovable(bool set)
    {
        movable = set;
        if (!movable)
        {
            anim.SetInteger("State", -1 * lastKey);
            rb.velocity = Vector3.zero;
        }
    }

    public Vector3 getPlayPos()
    {
        return transform.position;
    }

    public void setPlayPos(Vector3 newpos)
    {
        transform.position = newpos;
    }
}