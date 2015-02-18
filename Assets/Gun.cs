using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject player;
	public GameObject bulletPref;
	public int bulCount;
	public float bulSpeed;
	public float resetTimer;
	public float curTimer;

	void FixedUpdate(){

//		if(curTimer < resetTimer){
//			curTimer += Time.deltaTime;
//		}
//		else{
//			bulCount = 0;
//		}
	}

	public void shoot(){
		//if(bulCount < 4){
			//bulCount++;

			Vector3 bPos = new Vector3(Mathf.Cos(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) + player.transform.position.x,
			                           Mathf.Sin(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) + player.transform.position.y, -0.25f);
			Quaternion bRot =  Quaternion.Euler(0, 0, player.transform.rotation.eulerAngles.z + 90f);
			GameObject bul = Instantiate(bulletPref, bPos, bRot) as GameObject;
			Vector3 bVel = new Vector3(Mathf.Cos(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 
			                           Mathf.Sin(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad),0);

			bul.rigidbody.velocity =  bVel * bulSpeed;
//			curTimer = 0.0f;
//		}
//		else if(curTimer > resetTimer){
//			curTimer = 0.0f;
//		}

	}
}
