using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBar : MonoBehaviour 
{
	private Text health1;
	private Text health2;
	private Text end;
	private Text start;
	public Player player1;
	public Player player2;
	private int startInt = 0;
	private float nI, hI, fI;

	void Start () {
		nI = player1.nIntensity;
		hI = player1.hIntensity;
		fI = player1.fIntensity;

		health1 = transform.Find("health1").gameObject.GetComponent<Text>(); 
		health1.text= "Health: 10";
		
		health2 = transform.Find("health2").gameObject.GetComponent<Text>(); 
		health2.text= "";
		
		end = transform.Find("EndGame").gameObject.GetComponent<Text>(); 
		end.text= "";

		start = transform.Find("StartGame").gameObject.GetComponent<Text>(); 
		start.text= "";
	}
	
	void Update () {

		switch(startInt){
		case 0:
			player1.myLight.intensity = 0f;
			player2.myLight.intensity = nI;
			start.text = "Player 1:\nRotate: A/D\nMove: W/S\nFire: LShift/C\nPress Return";
			start.color = Color.blue;
			break;
		case 1:
			player1.myLight.intensity = nI;
			player2.myLight.intensity = 0f;
			start.text = "Player 2:\nRotate: LArrow/RArrow\nMove: UArrow/DArrow\nFire: Period/RShiftC\nPress Return";
			start.color = Color.red;
			break;
		case 2:
			player1.myLight.intensity = hI;
			player2.myLight.intensity = hI;
			start.text = "Green Lights:\nDim your light to make it harder for the other player to see you\nPress Return";
			start.color = Color.green;
			break;
		case 3:
			player1.myLight.intensity = fI;
			player2.myLight.intensity = fI;
			start.text = "White Lights:\nBrighten the other players light to make it easy for you to find them\nPress Return";
			start.color = Color.white;
			break;
		case 4:
			start.text = "Press Return to Start!";
			start.color = Color.white;
			break;
		case 5:
			start.text = "";
			player1.controlsActive = true;
			player2.controlsActive = true;
			player1.myLight.intensity = nI;
			player2.myLight.intensity = nI;
			startInt++;
			break;
		default: break;
		}

		if(!player1.controlsActive || !player2.controlsActive){
			if(Input.GetKeyUp(KeyCode.Return)){
				startInt++;
			}
		}

		//life
		string newLife1 = "Health: ";

		newLife1 += player1.health.ToString();
		health1.text = newLife1;

		string newLife2 = "Health: ";

		newLife2 += player2.health.ToString();
		health2.text = newLife2;

		if(player1.health == 0){
			end.text = "Player 2 Wins!\n(Press 'R' to restart)";
			end.color = Color.red;
		}
		if(player2.health == 0){
			end.text = "Player 1 Wins!\n(Press 'R' to restart)";
			end.color = Color.blue;
		}
	}
}