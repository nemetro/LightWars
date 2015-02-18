using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public Camera cam;
	public Gun gun;
	public int player;
	public float sensitivity;
	public Light rivalLight;
	public Light myLight;
	private float shottimer = -100.0f;
	private int bCount;
	private KeyCode [] keys = new KeyCode[6];
	public int health;
	private bool gameover = false;
	public float itemTime = 15f;
	private bool hider = false, finder = false;
	private float hiderTimer = 0, finderTimer = 0;
	public bool controlsActive = false;
	public GameObject deadPre;

	public float nRange = 30f, nIntensity = 0.5f;
	public float hRange = 3f, hIntensity = 0.05f;
	public float fRange = 90f, fIntensity = 4.5f;

	void Start(){
		//keys [0] = move
		//keys [1] = back
		//keys [2] = cntclock
		//keys [3] = clock
		//keys [4] = shoot1
		//keys [5] = shoot2
		if(player == 1){
			keys[0] = KeyCode.W;
			keys[1] = KeyCode.S;
			keys[2] = KeyCode.A;
			keys[3] = KeyCode.D;
			keys[4] = KeyCode.LeftShift;
			keys[5] = KeyCode.C;
		}
		if(player == 2){
			keys[0] = KeyCode.UpArrow;
			keys[1] = KeyCode.DownArrow;
			keys[2] = KeyCode.LeftArrow;
			keys[3] = KeyCode.RightArrow;
			keys[4] = KeyCode.RightShift;
			keys[5] = KeyCode.Period;
		}
		health = 5;
		gameover = false;
	}

	void FixedUpdate () 
	{
		if(finder || hider)
			updateItems();

		if(controlsActive)
			updateTrans();

		//keys [4] || keys [5] = shoot
		if((Input.GetKeyUp(keys[4]) || Input.GetKeyUp(keys[5])) && shottimer <= 0 && bCount < 2){
			bCount++;
			if(bCount == 2){
				shottimer = 0.1f;
				bCount = 0;
			}
			gun.shoot();
		}
		else if(shottimer > 0){
			shottimer -= Time.deltaTime;
		}

		if(gameover && Input.GetKey (KeyCode.R))
			Application.LoadLevel("_LightWars");
	}

	void updateTrans(){
		Vector3 newpos = new Vector3(this.transform.position.x, 
		                             this.transform.position.y, 
		                             this.transform.position.z);
		//Forward
		if (Input.GetKey (keys[0])) 
		{
			newpos.x += 0.1f*Mathf.Cos(this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
			newpos.y += 0.1f*Mathf.Sin(this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
		} 
		//keys [1] = back
		if (Input.GetKey (keys[1])) 
		{
			newpos.x -= 0.1f* Mathf.Cos(this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
			newpos.y -= 0.1f* Mathf.Sin(this.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
		}
		
		//keys [2] = cntclock
		if(Input.GetKey(keys[2])){
			this.transform.Rotate(new Vector3(0, 0, sensitivity));
		}
		
		//keys [3] = clock
		if(Input.GetKey(keys[3])){
			this.transform.Rotate(new Vector3(0, 0, -1f*sensitivity));
		}
		
		this.transform.position = newpos;
		newpos.z = cam.transform.position.z;
		cam.transform.position = newpos;
	}

	void updateItems(){
		if(finder){
			if(finderTimer < itemTime){
				finderTimer += Time.deltaTime;
			}
			else{
				rivalLight.range = nRange;
				rivalLight.intensity = nIntensity;
				finder = false;
			}
		}
		if(hider){
			if(hiderTimer < itemTime){
				hiderTimer += Time.deltaTime;
			}
			else{
				myLight.range = nRange;
				myLight.intensity = nIntensity;
				hider = false;
			}
		}
	}

	void Update(){
		if(health <= 0 && !gameover){
			gameover = true;
			Instantiate(deadPre, this.transform.position, Quaternion.Euler(0,0,0));
			this.renderer.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Bullet"){
			Destroy(other.gameObject);
			health--;
		}

		if(other.name == "Finder(Clone)"){
			rivalLight.range = fRange;
			rivalLight.intensity = fIntensity;
			finderTimer = 0f;
			finder = true;
			Destroy (other.gameObject);
		}

		if(other.name == "Hider(Clone)"){
			myLight.range = hRange;
			myLight.intensity = hIntensity;
			hider = true;
			hiderTimer = 0f;
			Destroy (other.gameObject);
		}
	}
}
