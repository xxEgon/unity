using UnityEngine;
using System.Collections;
 
public class PlayerController : MonoBehaviour {
 
    public static int hp = 200;
    private float xInput, yInput;
    public float speed = 2;
    private Rigidbody2D body;
    private bool isMoving;
    private float niebij= 0;      
    private float niebij_chuju= 1f;
    private GameObject player;
    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        isMoving = false;
        // isAttacking = false;
        player = GameObject.Find("Player");
    }
   
    // Update is called once per frame
    void Update () {
 
        // Get input
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
 
        // If IsMoving, Move with 3d Vector
        isMoving = (xInput != 0 || yInput != 0);
 
        if (isMoving)
        {
            var moveVector = new Vector2(xInput , yInput);  
 
            // BAD MOVEMENT - Circumvents RigidBody2D Physics
            // transform.position += moveVector * speed * Time.deltaTime;
            //
 
            // CORRECT MOVEMENT
            body.MovePosition(new Vector2((transform.position.x + moveVector.x * speed * Time.deltaTime), transform.position.y + moveVector.y * speed * Time.deltaTime));
            Sprite spr;
			SpriteRenderer sprRenderer;
            if(xInput == 1){
                spr = Resources.Load<Sprite>("Sprites/right");
                sprRenderer = (SpriteRenderer) GetComponent<Renderer>();
                sprRenderer.sprite = spr;
            }
            else if(xInput == -1){
                spr = Resources.Load<Sprite>("Sprites/left");
                sprRenderer = (SpriteRenderer) GetComponent<Renderer>();
                sprRenderer.sprite = spr;
            }
            else if(yInput == 1){
                spr = Resources.Load<Sprite>("Sprites/up");
                sprRenderer = (SpriteRenderer) GetComponent<Renderer>();
                sprRenderer.sprite = spr;
            }
            else if(yInput == -1){
                spr = Resources.Load<Sprite>("Sprites/down");
                sprRenderer = (SpriteRenderer) GetComponent<Renderer>();
                sprRenderer.sprite = spr;
            }
        }

        if(hp<0){
            Debug.Log("NO I CHUJ NO I CZEŚĆ");
            GameObject.Find("DeathCanvas").GetComponent<GameOver>().Death("YOU DIED");
            //Destroy(this.gameObject);
        }
 
    	if (niebij <=niebij_chuju) {
			niebij += Time.deltaTime;
		}

    }
void OnTriggerStay2D (Collider2D col)
    { 
		if(col.name=="Enemy1(Clone)" && niebij > niebij_chuju)
		{
			Debug.Log("zajebalem ci");
			hp-=5;
            niebij=0;
			
		}

        if(col.name=="Enemy3(Clone)" && niebij > niebij_chuju)
		{
			Debug.Log("zajebalem ci");
			hp-=5;
			niebij=0;
		}
    }

    
}