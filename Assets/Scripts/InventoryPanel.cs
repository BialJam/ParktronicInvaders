using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {

	enum typeOfClick
	{
		takeControl,
		giveBomb,
		givePoison,
		giveHammer,
		giveMatch,
		giveShit
	};
	typeOfClick LeftMouseButtonClick = typeOfClick.takeControl; //jeszcze w starcie jest przypisana wartosc

	void SetItemQuantity(int controls, int Bombs, int poisons, int hammers, int matches, int shits)
	{

	}

	int controlsLeft, bombsLeft, poisonsLeft, hammersLeft, matchesLeft, shitsLeft;

	void OnGUI()
	{
		for(int i=0; i<5; i++)
		{
			int przedmiot = 0;
									//napisy na buttonie
			string buttonNapis = "Bomba";
			if (i == 1)
				buttonNapis = "Trucizna";
			else if(i == 2)
				buttonNapis = "Mlotek";
			else if (i == 3)
				buttonNapis = "Zapalka";
			else if (i == 4)
				buttonNapis = "Kupa";
									//koniec napisow na buttonie

			if (GUI.Button(new Rect(Screen.width * 0.9f, Screen.height * i * 0.2f, Screen.width * 0.1f, Screen.height * 0.2f), buttonNapis))
			{
				if(buttonNapis == "Bomba")
				{
					LeftMouseButtonClick = typeOfClick.giveBomb;
				}
				else if (buttonNapis == "Trucizna")
				{
					LeftMouseButtonClick = typeOfClick.givePoison;
				}
				else if (buttonNapis == "Zapalka")
				{
					LeftMouseButtonClick = typeOfClick.giveMatch;
				}

				GUI.TextField(new Rect(Screen.width * 0.4f, 0f, Screen.width * 0.2f, Screen.height * 1.5f), "Wybrano cos");
			}
		}

	}
	
	void Start () {
		LeftMouseButtonClick = typeOfClick.takeControl; //jeszcze po enumie jest przypisana wartosc (i deklaracja)
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				GameObject human = hit.transform.gameObject;
				if(LeftMouseButtonClick == typeOfClick.takeControl)
				{
					//human.DotStatistics.TakeControl();         przypomniec adasiowi o zmiennej static
				}
				else if(LeftMouseButtonClick == typeOfClick.giveBomb)
				{
					//human.DotStatistics.BlowUp();
					LeftMouseButtonClick = typeOfClick.takeControl;
				}
				else if (LeftMouseButtonClick == typeOfClick.givePoison)
				{
					Debug.Log("Jeszcze nie zakodzone");
					LeftMouseButtonClick = typeOfClick.takeControl;
				}
				else if (LeftMouseButtonClick == typeOfClick.giveHammer)
				{
					Debug.Log("Jeszcze nie zakodzone");
					LeftMouseButtonClick = typeOfClick.takeControl;
				}
				else if (LeftMouseButtonClick == typeOfClick.giveMatch)
				{
					//human.DotStatistics.SetOnFire();
					LeftMouseButtonClick = typeOfClick.takeControl;
				}
				else if (LeftMouseButtonClick == typeOfClick.giveShit)
				{
					human.GetComponent<DotStatistics>().Death();
					LeftMouseButtonClick = typeOfClick.takeControl;
				}
			}
		}
	}
}
