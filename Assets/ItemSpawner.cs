using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {
	
	public GameObject hiderPref;
	public GameObject finderPref;
	public float timer = 0;
	public float maxFreq = 20f;
	public float minFreq = 5f;
	public float spawnTime;

	private GameObject hider = null;
	private float htimer = 0f;
	private GameObject finder = null;
	private float ftimer = 0f;

	void Start(){
		spawnTime = 15f;
	}

	void FixedUpdate(){

		if(hider != null){
			if(htimer < 20f)
				htimer += Time.deltaTime;
			else{
				Destroy(hider);
				htimer = 0f;
			}
		}
		if(finder != null){
			if(ftimer < 20f)
				ftimer += Time.deltaTime;
			else{
				Destroy(finder);
				ftimer = 0f;
			}
		}

		if(timer > spawnTime){
			int rnd = Random.Range(0,3);
			if(rnd == 1 && hider == null){
				hider = Instantiate(hiderPref,
				                    new Vector3(Random.Range(0.5f,29.5f),
				                                           Random.Range(0.5f,19.5f), 0),
				                    Quaternion.Euler(0,0,0)) as GameObject;
				htimer = 0f;
			}
			else if(rnd == 2 && finder == null){
				finder = Instantiate(finderPref,
				            new Vector3(Random.Range(0.5f,29.5f), 
				            						Random.Range(0.5f,19.5f), 0), 
				            Quaternion.Euler(0,0,0)) as GameObject;
				ftimer = 0f;
			}
			timer = 0;
			spawnTime = Random.Range(minFreq, maxFreq);
		}
		else{
			timer += Time.deltaTime;
		}
	}

}
