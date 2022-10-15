using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player3DController : MonoBehaviour
{
    // Buttons
    PlayerControls controls;

    // GameObjects
    GameObject aimObject;
    public Image imageCooldown;
    public GameObject fireballPrefab;

    // Physics
    Vector2 move;
    float moveAngle;
    Vector2 aim;
    float aimAngle;

    // Constants
    float moveSpeed = 1;
    float turnSpeed = 0.001f;
    float fireballCooldown = 3;

    // gameStuff
    bool fireballReady = true;
    
    // Start is called before the first frame update
    void Start()
    {
        // GameObjects
        foreach (Transform child in transform) {
            if (child.name == "Aim") {
                aimObject = child.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector2 movement = new Vector2(move.x, move.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
        Quaternion moveRotation = Quaternion.AngleAxis(moveAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, turnSpeed * Time.time);

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

    private void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }
    private void OnAim(InputValue value) {
        aim = value.Get<Vector2>();
    }
    
    private void OnFire() {
        ThrowFireball();
    }
}
