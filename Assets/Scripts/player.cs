using System.Collections;
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
    public GameObject selectedWeapon;
    public GameObject selectedShield;
    public GameObject weapon;
    public GameObject shield;
    Vector3 itemPosition;
    int controllerID;
    bool armed;
    public GameObject bullet;
    public GameObject specialBullet;
    bool onCooldown;
    bool onCooldown2;
    public GameObject slowAnim;
    [SerializeField]
    private HealthBar teamHealthBar;
    public GameObject teamPartner;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movable = true;
        rb = GetComponent<Rigidbody>();
        itemPosition = new Vector3(1.0f, 0.0f, 0.0f);
        weapon.transform.position = transform.position + itemPosition;
        shield.transform.position = transform.position + itemPosition;
        weapon.SetActive(false);
        armed = false;
        selectedWeapon.SetActive(false);
        selectedShield.SetActive(true);
        if(playerID == 0 || playerID == 2)
        {
            shield.SetActive(false);
            weapon.SetActive(true);
            armed = true;
            selectedWeapon.SetActive(true);
            selectedShield.SetActive(false);
        }
        else if (playerID == 1 || playerID == 3)
        {
            shield.SetActive(true);
            weapon.SetActive(false);
            armed = false;
            selectedWeapon.SetActive(false);
            selectedShield.SetActive(true);
        }

        /*for(int i = 0; i < 4; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]);
        }*/
        //Suche nach dem manuell einzustellenden Controller, falls Probleme mit dem Input Manager auftreten
        if (playerID == 3)
        {
            int controllerNum = Input.GetJoystickNames().Length;
            for (int i = 0; i < controllerNum; i++)
            {
                Debug.Log(Input.GetJoystickNames()[i] + i);
                if (Input.GetJoystickNames()[i].Equals("Wireless Controller"))
                {
                    Debug.Log("Joystick found");
                    Debug.Log(i);
                    controllerID = i + 1;
                }
            }
        }
        //Setzen des Layers je nach Teamnummer
        if (teamNumber == 1)
        {
            gameObject.layer = 8;
        }
        else if (teamNumber == 2)
        {
            gameObject.layer = 9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Change Item
        if (playerID == 3)
        {
            if (Input.GetButtonDown("Change" + controllerID))
            {
                changeItem();
                teamPartner.GetComponent<player>().changeItem();
            }
        }
        else
        {
            if (InputManager.Instance.getButtonDown(playerID, InputManager.ButtonType.Y))
            {
                changeItem();
                teamPartner.GetComponent<player>().changeItem();
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
            amtToMove = Input.GetAxis("Horizontal" + controllerID) * playerSpeed / 2 * Time.deltaTime;
            amtToMove2 = Input.GetAxis("Vertical" + controllerID) * playerSpeed / 2 * Time.deltaTime;
            /*Debug.Log(Input.GetAxis("Horizontal" + controllerID));
            Debug.Log(Input.GetAxis("Vertical" + controllerID));
            Debug.Log("2: " + Input.GetAxis("Horizontal2"));
            Debug.Log("3: " + Input.GetAxis("Horizontal3"));
            Debug.Log("4: " + Input.GetAxis("Horizontal4"));*/
        }
        else
        {
            amtToMove = InputManager.Instance.getLeftStick(playerID).x * playerSpeed / 2 * Time.deltaTime;
            amtToMove2 = InputManager.Instance.getLeftStick(playerID).y * playerSpeed / 2 * Time.deltaTime;
        }
        if (movable)
        {
            if (amtToMove > 0 && amtToMove2 > 0) //Das Mathf.Abs könnte evtl für Analog-Sticks wichtig sein
            {
                if (armed == true)
                {
                    anim.SetInteger("State", 7);
                    lastKey = 7;
                }
                else
                {
                    anim.SetInteger("State", 3);
                    lastKey = 3;
                }
            }
            if (amtToMove < 0 && amtToMove2 > 0)
            {
                if (armed == true)
                {
                    anim.SetInteger("State", 6);
                    lastKey = 6;
                }
                else
                {
                    anim.SetInteger("State", 2);
                    lastKey = 2;
                }
            }
            if (amtToMove < 0 && amtToMove2 < 0)
            {
                if (armed == true)
                {
                    anim.SetInteger("State", 5);
                    lastKey = 5;
                }
                else
                {
                    anim.SetInteger("State", 1);
                    lastKey = 1;
                }
            }
            if (amtToMove > 0 && amtToMove2 > 0)
            {
                if (armed == true)
                {
                    anim.SetInteger("State", 8);
                    lastKey = 8;
                }
                else
                {
                    anim.SetInteger("State", 4);
                    lastKey = 4;
                }
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
        if (playerID == 3)
        {
            if (Mathf.Abs(Input.GetAxis("HorizontalRight" + controllerID)) + Mathf.Abs(Input.GetAxis("VerticalRight" + controllerID)) >= 0.1f)
            {
                itemPosition = new Vector3(Input.GetAxis("HorizontalRight" + controllerID), Input.GetAxis("VerticalRight" + controllerID), 0.0f);
                itemPosition = Vector3.Normalize(itemPosition);
                weapon.transform.position = transform.position + itemPosition;
                shield.transform.position = transform.position + itemPosition;
            }
        }
        else
        {
            if (Mathf.Abs(InputManager.Instance.getRightStick(playerID).x) + Mathf.Abs(InputManager.Instance.getRightStick(playerID).y) >= 0.1f)
            {
                itemPosition = new Vector3(InputManager.Instance.getRightStick(playerID).x, InputManager.Instance.getRightStick(playerID).y, 0.0f);
                itemPosition = Vector3.Normalize(itemPosition);
                weapon.transform.position = transform.position + itemPosition;
                shield.transform.position = transform.position + itemPosition;
            }
        }

        if (armed && onCooldown == false)//Überprüfung ob Waffe ausgerüstet ist und geschossen werden kann
        {
            if (playerID == 3)
            {
                if (Input.GetButtonDown("Fire" + controllerID))
                {
                    GameObject obj = (GameObject)Instantiate(bullet, weapon.transform.position, Quaternion.identity);
                    if (teamNumber == 1)
                    {
                        obj.layer = 10;
                    }
                    else if (teamNumber == 2)
                    {
                        obj.layer = 11;
                    }
                    Projectile projectile = obj.GetComponent<Projectile>();
                    projectile.setDirection(itemPosition);
                    projectile.sourceId = playerID;
                    onCooldown = true;
                    StartCoroutine(Cooldown());
                }
                if (Input.GetButtonDown("FireHard" + controllerID) && onCooldown2 == false)
                {
                    GameObject obj = (GameObject)Instantiate(specialBullet, weapon.transform.position, Quaternion.identity);
                    if (teamNumber == 1)
                    {
                        obj.layer = 10;
                    }
                    else if (teamNumber == 2)
                    {
                        obj.layer = 11;
                    }
                    Projectile projectile = obj.GetComponent<Projectile>();
                    projectile.setDirection(itemPosition);
                    projectile.sourceId = playerID;
                    onCooldown = true;
                    onCooldown2 = true;
                    StartCoroutine(Cooldown());
                    StartCoroutine(SpecialCooldown());
                }
            }
            else
            {
                if (InputManager.Instance.getButtonDown(playerID, InputManager.ButtonType.RightShoulder))
                {
                    GameObject obj = (GameObject)Instantiate(bullet, weapon.transform.position, Quaternion.identity);
                    if (teamNumber == 1)
                    {
                        obj.layer = 10;
                    }
                    else if (teamNumber == 2)
                    {
                        obj.layer = 11;
                    }
                    Projectile projectile = obj.GetComponent<Projectile>();
                    projectile.setDirection(itemPosition);
                    projectile.sourceId = playerID;
                    onCooldown = true;
                    StartCoroutine(Cooldown());
                }
                if (InputManager.Instance.getButtonDown(playerID, InputManager.ButtonType.LeftShoulder) && onCooldown2 == false)
                {
                    GameObject obj = (GameObject)Instantiate(specialBullet, weapon.transform.position, Quaternion.identity);
                    if (teamNumber == 1)
                    {
                        obj.layer = 10;
                    }
                    else if (teamNumber == 2)
                    {
                        obj.layer = 11;
                    }
                    Projectile projectile = obj.GetComponent<Projectile>();
                    projectile.setDirection(itemPosition);
                    projectile.sourceId = playerID;
                    onCooldown = true;
                    onCooldown2 = true;
                    StartCoroutine(Cooldown());
                    StartCoroutine(SpecialCooldown());
                }
            }
        }

        if (amtToMove == 0 && amtToMove2 == 0)
        {
            anim.SetInteger("State", -1 * lastKey);
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

        if (armed == false)
        {
            shield.gameObject.transform.LookAt(gameObject.transform, Vector3.right);
        }
        else
        {
            weapon.gameObject.transform.LookAt(gameObject.transform, Vector3.right);
        }
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

    public void slowEnemies()
    {
        if (teamNumber == 1)
        {
            GameObject.Find("player3").gameObject.GetComponent<player>().setSlow();
            GameObject.Find("player4").gameObject.GetComponent<player>().setSlow();
        }
        else
        {
            GameObject.Find("player1").gameObject.GetComponent<player>().setSlow();
            GameObject.Find("player2").gameObject.GetComponent<player>().setSlow();
        }
    }

    public void setSlow()
    {
        playerSpeed = 4;
        slowAnim.SetActive(true);
        StartCoroutine(Slow());
    }

    public void revertSlow()
    {
        playerSpeed = 12;
        slowAnim.SetActive(false);
    }

    //TODO: DAMAGE ?
    public void reduceLife(int team)
    {
        Debug.Log("Should get damaged");
        GameManager.reduceTeamHealth(team);
        teamHealthBar.updateHealthbar();
    }

    public void changeItem()
    {
        if (weapon.activeSelf)
        {
            weapon.SetActive(false);
            armed = false;
            selectedWeapon.SetActive(false);
            selectedShield.SetActive(true);
            shield.SetActive(true);
        }
        else if (shield.activeSelf)
        {
            shield.SetActive(false);
            armed = true;
            selectedWeapon.SetActive(true);
            selectedShield.SetActive(false);
            weapon.SetActive(true);
        }
    }

    //------------------------------------------------------------------------------------------------------------------
    //Coroutines
    //------------------------------------------------------------------------------------------------------------------

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        onCooldown = false;
    }

    IEnumerator SpecialCooldown()
    {
        yield return new WaitForSeconds(10f);
        onCooldown2 = false;
    }

    IEnumerator Slow()
    {
        yield return new WaitForSeconds(6f);
        revertSlow();
    }
}