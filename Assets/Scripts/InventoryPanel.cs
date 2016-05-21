using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
    
    public GameObject hoverImage;
    public DotController dotController;
    GameObject lasthito;
    private bool doneSomethingBad = false;

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

    int controlsLeft, bombsLeft, poisonsLeft, hammersLeft, matchesLeft, shitsLeft;

	public void SetItemQuantity(int controls, int Bombs, int poisons, int hammers, int matches, int shits)
	{
        controlsLeft = controls;
        bombsLeft = Bombs;
        poisonsLeft = poisons;
        hammersLeft = hammers;
        matchesLeft = matches;
        shitsLeft = shits;
	}

	void OnGUI()
	{
		for(int i=0; i<5; i++)
		{
			//int przedmiot = 0;
									//napisy na buttonie
			string buttonNapis = "Bomba" + ", Left: " + bombsLeft;
			if (i == 1)
				buttonNapis = "Trucizna" + ", Left: " + poisonsLeft;
			else if(i == 2)
				buttonNapis = "Mlotek" + ", Left: " + hammersLeft;
			else if (i == 3)
				buttonNapis = "Zapalka" + ", Left: " + matchesLeft;
			else if (i == 4)
				buttonNapis = "Kupa" + ", Left: " + shitsLeft;
									//koniec napisow na buttonie

			if (GUI.Button(new Rect(Screen.width * 0.9f, Screen.height * i * 0.2f, Screen.width * 0.1f, Screen.height * 0.2f), 
                buttonNapis))          //only bombs for everything
			{
				if(buttonNapis == "Bomba" + ", Left: " + bombsLeft)
				{
					LeftMouseButtonClick = typeOfClick.giveBomb;
				}
				else if (buttonNapis == "Trucizna" + ", Left: " + poisonsLeft)
				{
					LeftMouseButtonClick = typeOfClick.givePoison;
				}
				else if (buttonNapis == "Zapalka" + ", Left: " + matchesLeft)
				{
					LeftMouseButtonClick = typeOfClick.giveMatch;
				}
                else if (buttonNapis == "Kupa" + ", Left: " + shitsLeft)
                {
                    LeftMouseButtonClick = typeOfClick.giveShit;
                }
                else if (buttonNapis == "Mlotek" + ", Left: " + hammersLeft)
                {
                    LeftMouseButtonClick = typeOfClick.giveHammer;
                }
                GUI.TextField(new Rect(Screen.width * 0.4f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.2f), "Wybrano cos");
			}
		}
        if(LeftMouseButtonClick != typeOfClick.takeControl)
        {
            if(GUI.Button(new Rect(Screen.width * 0.8f, Screen.height * 0.8f, Screen.width * 0.1f, Screen.height * 0.2f), "Cancel this ability"))
            {
                LeftMouseButtonClick = typeOfClick.takeControl;
            }
        }
	}
	
	void Start () {
		LeftMouseButtonClick = typeOfClick.takeControl; //jeszcze po enumie jest przypisana wartosc (i deklaracja)
        SetItemQuantity(1, 2, 2, 1, 1, 1);
        lasthito = null;
        doneSomethingBad = false;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform != null && hit.transform.gameObject.layer == 10)
			{
				GameObject human = hit.transform.gameObject;
                Debug.Log(LeftMouseButtonClick);
                if (LeftMouseButtonClick == typeOfClick.takeControl && controlsLeft > 0)
                {

                    if (dotController.takingControl(human))
                    {
                        controlsLeft--;
                    }
                }
                else if (dotController.isInstantiated && dotController.DotInControl != null && Vector3.Distance(dotController.DotInControl.transform.position, hit.transform.position) <= 15)
                {
                    if (LeftMouseButtonClick == typeOfClick.giveBomb && bombsLeft > 0)
                    {
                        human.GetComponent<DotStatistics>().Explode();
                        LeftMouseButtonClick = typeOfClick.takeControl;
                        bombsLeft--;
                        doneSomethingBad = true;
                    }
                    else if (LeftMouseButtonClick == typeOfClick.givePoison && poisonsLeft > 0)
                    {
                        human.GetComponent<DotStatistics>().StartPoison();
                        LeftMouseButtonClick = typeOfClick.takeControl;
                        poisonsLeft--;
                        doneSomethingBad = true;
                    }
                    else if (LeftMouseButtonClick == typeOfClick.giveHammer && hammersLeft > 0)
                    {
                        human.GetComponent<DotStatistics>().HammerTime();
                        LeftMouseButtonClick = typeOfClick.takeControl;
                        hammersLeft--;
                        doneSomethingBad = true;
                    }
                    else if (LeftMouseButtonClick == typeOfClick.giveMatch && matchesLeft > 0)
                    {
                        human.GetComponent<DotStatistics>().StartFire();
                        LeftMouseButtonClick = typeOfClick.takeControl;
                        matchesLeft--;
                        doneSomethingBad = true;
                    }
                    else if (LeftMouseButtonClick == typeOfClick.giveShit && shitsLeft > 0)
                    {
                        human.GetComponent<DotStatistics>().Death();
                        shitsLeft--;
                        doneSomethingBad = true;
                        LeftMouseButtonClick = typeOfClick.takeControl;
                    }
                }
			}
            if (hit.collider != null && hit.transform.gameObject.layer == 8)
            {
                GameObject house = hit.transform.gameObject;
                if (dotController.isInstantiated && dotController.DotInControl != null && Vector3.Distance(dotController.DotInControl.transform.position, hit.transform.position) <= 15)
                {
                    if (LeftMouseButtonClick == typeOfClick.giveMatch && matchesLeft > 0)
                    {
                        house.GetComponent<FiredBuilding>().isGoingToFire = true;
                        LeftMouseButtonClick = typeOfClick.takeControl;
                        matchesLeft--;
                        doneSomethingBad = true;
                    }
                }
            }
            if(hit.transform != null && hit.transform.gameObject.layer == 4)
            {
                GameObject lake = hit.transform.gameObject;
                if (dotController.isInstantiated && dotController.DotInControl != null && Vector3.Distance(dotController.DotInControl.transform.position, lake.transform.position) <= 20)
                {
                    if (LeftMouseButtonClick == typeOfClick.givePoison && poisonsLeft > 0)
                    {
                        lake.GetComponent<PoisonedLake>().isPoisoned = true;
                        LeftMouseButtonClick = typeOfClick.takeControl;
                        poisonsLeft--;
                        doneSomethingBad = true;
                    }
                }
            }
            if(doneSomethingBad)
            {
                doneSomethingBad = false;
                dotController.DotInControl.GetComponent<StatusController>().TagEnemies();
            }
        }

        RaycastHit2D hito = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hito.collider != null && hito.transform.gameObject.layer == 10)
        {
            if (lasthito != null)
            {
                lasthito.transform.SetParent(null);
                Destroy(lasthito);
            }
            GameObject hover = (GameObject)Instantiate(hoverImage, Camera.main.ScreenToWorldPoint(Input.mousePosition), new Quaternion(0,0,0,0));

            hover.transform.SetParent(hito.transform);
            hover.transform.localPosition = new Vector3(0, 0, 0);

            lasthito = hover;
        }
        else if(lasthito != null)
        {
            lasthito.transform.SetParent(null);
            Destroy(lasthito);
        }
    }
}
