using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public float gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.1f;                                      // Number in seconds which controls how often the player can fire
    public float attackSpeed = 10;
	public float weaponRange = 10f;                                     // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    private float corection = 0.7f;
	public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float startFire= 0;            
	private bool right=false,left=false,up=false,down=false;                                 // Float to store the time the player will be allowed to fire again, after firing

 public GameObject bulletPrefab;
    public Transform bulletSpawn;
	private GameObject player;
    void Start () 
    {
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();
		player = GameObject.Find("player");

    }
    

    void Update () 
    {
		if (Input.GetKeyDown("right")){
				right=true;
		}
		if (Input.GetKeyDown("left")){
				left=true;
		}
		if (Input.GetKeyDown("up")){
				up=true;
		}
		if (Input.GetKeyDown("down")){
				down=true;
		}


		if (Input.GetKeyUp("right")){
				right=false;
		}
		if (Input.GetKeyUp("left")){
				left=false;
		}
		if (Input.GetKeyUp("up")){
				up=false;
		}
		if (Input.GetKeyUp("down")){
				down=false;
		}

		if(right && !up && !down && startFire > fireRate){
			Fire(1);
		} else
		if(left && !up && !down && startFire > fireRate){
			Fire(2);
		} else
		if(up && !left && !right && startFire > fireRate){
			Fire(3);
		} else
		if(down && !left && !right && startFire > fireRate){
			Fire(4);
		} else
		if(up && right && startFire > fireRate){
			Fire(5);
		} else
		if(up && left && startFire > fireRate){
			Fire(6);
		} else
		if(down && right && startFire > fireRate){
			Fire(7);
		} else
		if(down && left && startFire > fireRate){
			Fire(8);
		}

		// if(!up && !down && !left && !right){
		// 	startFire=0;
		// }

		if (startFire <=fireRate) {
			startFire += Time.deltaTime;
		}

	
		
	
	
    }
void Fire(int direction)
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
        // Add velocity to the bullet
		//bullet.transform.forward = player.transform.position - transform.position;
		switch(direction){
			case 1:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*attackSpeed,ForceMode2D.Impulse);
				break;
			case 2:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*(-attackSpeed),ForceMode2D.Impulse);
				break;
			case 3:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up*attackSpeed,ForceMode2D.Impulse);
				break;
			case 4:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up*(-attackSpeed),ForceMode2D.Impulse);
				break;
			case 5:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up*attackSpeed*corection,ForceMode2D.Impulse);
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*attackSpeed*corection,ForceMode2D.Impulse);
				break;
			case 6:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up*attackSpeed*corection,ForceMode2D.Impulse);
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*(-attackSpeed*corection),ForceMode2D.Impulse);
				break;
			case 7:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up*(-attackSpeed*corection),ForceMode2D.Impulse);
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*attackSpeed*corection,ForceMode2D.Impulse);
				break;
			case 8:
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up*(-attackSpeed*corection),ForceMode2D.Impulse);
				bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*(-attackSpeed*corection),ForceMode2D.Impulse);
				break;
			
		}

		startFire = 0;        
    }
}
