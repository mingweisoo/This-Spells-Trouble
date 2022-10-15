using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineThrowController : MonoBehaviour, SpellController
{
    // ScriptableObjects
    public GameConstants gameConstants;

    // Components
    private Rigidbody2D mineThrowBody;
    public GameObject mineGround;

    // Physics
    public float aimAngle;
    public Vector2 movement;
    public bool isShrinking = true;
    public bool isDone = false;
    private IEnumerator createMinesCoroutine;
    private float xPos;
    private float yPos;
    private float xPos_original;

    // Game state
    public int srcPlayerID { get; set; }
    public int spellLevel;
    int rows;
    int columns;

    // Sound Events
    [Header("Sound Events")]
    public GameEvent onMineCastPlaySound;
    // public GameEvent onMineLandPlaySound;

    void Start()
    {
        // Get components
        mineThrowBody = GetComponent<Rigidbody2D>();

        // Get constants
        switch (spellLevel) {
            case 2:
                rows = gameConstants.mineThrowRowsL2L3;
                columns = gameConstants.mineThrowColumnsL2L3;
                break;
            case 3:
                rows = gameConstants.mineThrowRowsL2L3;
                columns = gameConstants.mineThrowColumnsL2L3;
                break;
            default:
                rows = gameConstants.mineThrowRowsL1;
                columns = gameConstants.mineThrowColumnsL1;
                break;
        }

        // mineThrow movement
        movement = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle));
        mineThrowBody.AddForce(movement * gameConstants.mineThrowSpeed, ForceMode2D.Impulse);
        transform.Rotate(0f,0f,aimAngle);
        onMineCastPlaySound.Raise();
    }
    void Update(){
        if (isShrinking){
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, new Vector3(0.3f,0.3f,1f), 0.2f/gameConstants.mineThrowTime/2*Time.deltaTime);
            if (this.transform.localScale.x <= 0.3f){
                isShrinking = false;
            }
        }
        if (!isShrinking && !isDone){
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, new Vector3(0.5f,0.5f,1f), 0.2f/gameConstants.mineThrowTime/2*Time.deltaTime);
            if (this.transform.localScale.x >= 0.5f){
                mineThrowBody.velocity = new Vector2(0f,0f);
                isDone = true;
                createMinesCoroutine = createMines();
                StartCoroutine(createMinesCoroutine);
            }
        }
    }
    public IEnumerator createMines()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        // Debug.Log(transform.position.x + "," + transform.position.y);
        float topRightX = transform.position.x + ((float) rows - 1) / 2 ;
        float topRightY = transform.position.y + ((float) columns - 1) / 2;
        float xPos = topRightX;
        float yPos = topRightY;
        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                mineGround = Instantiate(mineGround, new Vector3(xPos, yPos,this.transform.position.z), Quaternion.identity);
                mineGround.GetComponent<MineGroundController>().srcPlayerID = srcPlayerID;
                mineGround.GetComponent<MineGroundController>().aimAngle = aimAngle;
                mineGround.GetComponent<MineGroundController>().movement = movement;
                mineGround.GetComponent<MineGroundController>().spellLevel = spellLevel;
                xPos -= 1;
            }
            yPos -= 1;
            xPos = topRightX;
        }

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
