using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControllerOld : MonoBehaviour
{
    // Buttons
    PlayerControls controls;

    // GameObjects
    GameObject aimObject;
    public Image imageCooldown;
    public GameObject fireballPrefab;

    // Components
    //private Rigidbody2D mageBody;

    // Physics
    Vector2 move;
    Vector2 aim;
    float aimAngle;

    // Constants
    float moveSpeed = 1;
    //float turnSpeed = 0.001f;
    float fireballCooldown = 3;

    // gameStuff
    bool fireballReady = true;

    void Awake() {
        // controls
        controls = new PlayerControls();
        controls.Battle.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Battle.Move.canceled += ctx => move = Vector2.zero;
        controls.Battle.Aim.performed += ctx => aim = ctx.ReadValue<Vector2>();
        controls.Battle.Aim.canceled += ctx => aim = Vector2.zero;
        controls.Battle.Spell1.performed += ctx => ThrowFireball();
    }

    void OnEnable() {
        controls.Battle.Enable();
    }

    void OnDisable() {
        controls.Battle.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // GameObjects
        foreach (Transform child in transform) {
            if (child.name == "Aim") {
                aimObject = child.gameObject;
            }
        }

        //mageBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector2 movement = new Vector2(move.x, move.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Aim
        if (aim.x != 0 && aim.y != 0) {
            aimAngle = Mathf.Atan2(-aim.x, aim.y) * Mathf.Rad2Deg;
            // Uncomment next 2 lines and comment 3rd line if u want a turn lag
            //Quaternion aimRotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, aimRotation, turnSpeed * Time.time);
            aimObject.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        }

        // Cooldowns
        if (!fireballReady) {
            imageCooldown.fillAmount -= 1 / fireballCooldown * Time.deltaTime;
            //if (imageCooldown.fillAmount >= 1) {
            //    imageCooldown.fillAmount = 0;
            //}
        }
    }

    // Buttons
    void ThrowFireball() {
        if (fireballReady) {
            Debug.Log("throwing fireball!");
            GameObject fireballObject = Instantiate(fireballPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            fireballObject.GetComponent<FireballController>().aimAngle = aimAngle;
            StartCoroutine(FireballCooldown());
        } else {
            Debug.Log("cooling down...");
        }
    }

    IEnumerator FireballCooldown() {
        fireballReady = false;
        imageCooldown.fillAmount = 1;
        yield return new WaitForSeconds(fireballCooldown);
        fireballReady = true;
    }
}
