﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EndGame : MonoBehaviour {

    private AlertButton alertButton;
    private AllPoints allPoints;
    private LevelManager levelManager;
    private TimeController timeController;
    private MoneyCollect moneyCollect;
    private FinishGame finishGame;
    private int tec;
    private int sci;
    private int army;
    private int food;
    private int water;
    private int pop;
    private int power;
    private int nat;

    void Start () {
        allPoints = GetComponent<AllPoints>();
        finishGame = GetComponent<FinishGame>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if(levelManager == null)
        {
            Debug.Log("The game object -LevelManager- not find in scene");
        }
        timeController = GameObject.Find("TimeController").GetComponent<TimeController>();
        if (timeController == null)
        {
            Debug.Log("The game object -TimeController- not find in scene");
        }
        moneyCollect = GetComponent<MoneyCollect>();
        if (moneyCollect == null)
        {
            Debug.Log("The game object -PoitsController- not find in scene");
        }
        alertButton = GameObject.Find("LevelManager").GetComponent<AlertButton>();
        if (alertButton == null)
        {
            Debug.Log("The object -LevelManager- Dont find in scene");
        }

    }

    void Update () {
        ReadPoints();
        //Ganhou
        if (tec > 700 && sci > 700 && pop > 700 && nat > 600 && water > 600)
            FinishGame("Your planet is work", timeController.totalDays);

        //Ganhou
        if (tec > 950 && sci > 950 && nat > 800 && water > 700)
            FinishGame("You got a perfect Planet", timeController.totalDays);

        //Destruição por poluição
        if (tec>500 && pop>300 && nat<300)
            Destruction("Destruição por poluição (Muitos Prédios)");

        //Greve Da Raça humana
        if (tec<500 && sci<500 && food<(pop-200) && water<(pop-200) && pop>400 && nat<300)
            Destruction("Greve Da Raça humana(População muito infeliz)");

        //Falta de população
        if(tec - pop > 200 || sci - pop > 200)
            Destruction("Falta de população (Pouca estrutura para ter pessoas)");

        //Falta de comida
        if ((tec>250 || sci>250) && food<(pop-200) &&  nat<500)
            Destruction("Falta de comida");

        //Falta de água
        if ((tec>300 || sci>350) && water<(pop-200) && nat<400)
            Destruction("Falta de água");

        //Apocalipse
        if (sci>500 && army<500 && pop>300)
            Destruction("Apocalipse");

        //Tecnolipse
        if (tec>500 && army<500 && pop>300)
            Destruction("Tecnolipse");

        //Destruição por raça alienígena
        if ((tec<400 && sci<400 && army<400 && water>800 && pop>500) || timeController.totalDays == 1000)
            Destruction("Destruição por raça alienígena");

        //Destruição por outra espécie
        if (army<300 && food>300 && nat>600)
            Destruction("Destruição por outra espécie");

        //Ditadura  
        if (tec<(army-300) && sci<(army-300) && pop<(army-300))
            Destruction("Ditadura");

        //Planeta morreu
        if (nat < 10 || water < 10)
            Destruction("Your planet is dead");

        //A populaçao morreu
        if (pop == 0)
            Destruction("You dont Have peoples for your planets");

        //Energia abaixo dos Necessario
        if (tec > power + 150 || sci > power + 150 || pop > power + 200 || food > power + 200)
        {
            moneyCollect.FreezeEconomy(true);
            alertButton.SetButton(true);
        }
        else
        {
            moneyCollect.FreezeEconomy(false);
            alertButton.SetButton(false);
        }
    }

    void ReadPoints()
    {
        /*/ TECNOOLOGY /*/
        tec = allPoints.technology;
        /*/  SCIENCE  /*/
        sci = allPoints.science;
        /*/ ARMY /*/
        army = allPoints.army;
        /*/ FOOD /*/
        food = allPoints.food;
        /*/ WATER /*/
        water = allPoints.water;
        /*/  POPULATION  /*/
        pop = allPoints.population;
        /*/ POWER /*/
        power = allPoints.power;
        /*/ NATURE /*/
        nat = allPoints.nature;
    }

    void Destruction(string name)
    {
        //CallPanel(name, true);
        finishGame.LoseGame(name);
    }

    void FinishGame(string name, int days)
    {
        finishGame.WinGame(days, name);
    }
}
