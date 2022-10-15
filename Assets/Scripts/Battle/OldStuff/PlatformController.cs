using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // ScriptableObjects
    public GameConstants gameConstants;
    public KnockbackArr playersKnockback;

    // Game state
    bool[] onPlatform = {true, true, true, true};
    IEnumerator[] damageCoroutines = {null, null, null, null};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++) {
            if (!onPlatform[i]) {
                // Start damage coroutine, if not started
                if (damageCoroutines[i] == null) {
                    damageCoroutines[i] = Damage(i);
                    StartCoroutine(damageCoroutines[i]);
                }
            } else {
                // Stop damage coroutine, if running
                if (damageCoroutines[i] != null) {
                    StopCoroutine(damageCoroutines[i]);
                    damageCoroutines[i] = null;
                }
            }
        }
    }

    void  OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            onPlatform[other.gameObject.GetComponent<BattleController>().playerID] = true;
            other.gameObject.GetComponent<BattleController>().onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            onPlatform[other.gameObject.GetComponent<BattleController>().playerID] = false;
            other.gameObject.GetComponent<BattleController>().onPlatform = false;
        }
    }

    public IEnumerator Damage(int playerID) {
        while (true) {
            playersKnockback.SetValue(playerID, playersKnockback.GetValue(playerID) + gameConstants.lavaDamage);
            yield return new WaitForSeconds(gameConstants.lavaDamageInverval);
        }
    }
}
