﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyItens : MonoBehaviour {

    public int TotalMoney;
    public int TotalMoneyOfCompany;
    public int NumberOfCompany;
    public int NumberOfUpgrades;

    public int MoneyPerUpgrade;
    public int MoneyPerCompany;

    public int SetCompanyValue;
    public int SetUpgradeValue;
    public int CompanyValue;
    public int UpgradeValue;

    public int afectFood;
    public int afectWater;
    public int afectPopulation;
    public int afectArmy;
    private MoneyCollect moneyCollect;
    private AllPoints allPoints;
    public InstatiateErrorMessage errorMessage;
    private ArmyRequirement requirement;

    private MusicController musicController;

    private void Start()
    {
        allPoints = GameObject.Find("PointsController").GetComponent<AllPoints>();
        if (allPoints == null)
        {
            Debug.Log("The script army dont find the Game Object 'PointsController'");
        }
        moneyCollect = GameObject.Find("PointsController").GetComponent<MoneyCollect>();
        if (moneyCollect == null)
        {
            Debug.Log("The script army dont find the Game Object 'PointsController'");
        }
        requirement = GameObject.Find("ItensRequirementManager").GetComponent<ArmyRequirement>();
        if (requirement == null)
        {
            Debug.Log("The script army dont find the Game Object 'ItensRequirementManager'");
        }
        musicController = GameObject.Find("LevelManager").GetComponent<MusicController>();
        SetCompanyValue = CompanyValue;
        SetUpgradeValue = UpgradeValue;
    }

    void Update()
    {
        TotalMoneyOfCompany = MoneyPerCompany + MoneyPerUpgrade * NumberOfUpgrades;
        TotalMoney = TotalMoneyOfCompany * NumberOfCompany;

        moneyCollect.ReciveMoney(2, TotalMoney);
    }

    public void BuyCompany(int number)
    {
        if (requirement.requirement == "")
        {
            musicController.CoinSound();
            allPoints.money -= CompanyValue;
            NumberOfCompany += number;
            CompanyValue += SetCompanyValue;
            allPoints.AddArmy(afectArmy);
            allPoints.Addfood(afectFood);
            allPoints.AddWater(afectWater);
            allPoints.AddPopulation(afectPopulation);
        }
        else
        {
            musicController.ClickSound();
            errorMessage.Instantiate(requirement.requirement);
        }
    }

    public void BuyUpgrade(int number)
    {
        if (requirement.requirement == "")
        {
            musicController.CoinSound();
            allPoints.money -= UpgradeValue;
            NumberOfUpgrades += number;
            UpgradeValue += SetUpgradeValue;
            allPoints.AddArmy(afectArmy / 2);
            allPoints.Addfood(afectFood / 2);
            allPoints.AddWater(afectWater / 2);
            allPoints.AddPopulation(afectPopulation / 2);
        }
        else
        {
            musicController.ClickSound();
            errorMessage.Instantiate(requirement.requirement);
        }
    }
}
