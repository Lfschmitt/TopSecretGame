﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItens : MonoBehaviour {

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
    public int afectPopulation;
    public int afectEnergy;
    public int afectNature;
    private MoneyCollect moneyCollect;
    private AllPoints allPoints;
    public InstatiateErrorMessage errorMessage;
    private FoodRequirement requirement;

    private MusicController musicController;
    private ShopManager shopManager;

    private void Start()
    {
        allPoints = GameObject.Find("PointsController").GetComponent<AllPoints>();
        if (allPoints == null)
        {
            Debug.Log("The script food dont find the Game Object 'PointsController'");
        }
        moneyCollect = GameObject.Find("PointsController").GetComponent<MoneyCollect>();
        if (moneyCollect == null)
        {
            Debug.Log("The script food dont find the Game Object 'PointsController'");
        }
        requirement = GameObject.Find("ItensRequirementManager").GetComponent<FoodRequirement>();
        if (requirement == null)
        {
            Debug.Log("The script food dont find the Game Object 'ItensRequirementManager'");
        }
        musicController = GameObject.Find("LevelManager").GetComponent<MusicController>();
        shopManager = GameObject.Find("LevelManager").GetComponent<ShopManager>();
        SetCompanyValue = CompanyValue;
        SetUpgradeValue = UpgradeValue;
    }

    void Update()
    {
        TotalMoneyOfCompany = MoneyPerCompany + MoneyPerUpgrade * NumberOfUpgrades;
        TotalMoney = TotalMoneyOfCompany * NumberOfCompany;

        moneyCollect.ReciveMoney(3, TotalMoney);

        if (shopManager.count == 3)
        {
            if (requirement.requirement == "")
            {
                if (allPoints.money >= CompanyValue)
                    shopManager.ColorButton("Company", "White");
                else
                    shopManager.ColorButton("Company", "Red");

                if (allPoints.money >= UpgradeValue && NumberOfCompany > 0)
                    shopManager.ColorButton("Upgrade", "White");
                else
                    shopManager.ColorButton("Upgrade", "Red");
            }
            else
            {
                shopManager.ColorButton("Company", "Red");
                shopManager.ColorButton("Upgrade", "Red");
            }
        }
    }

    public void BuyCompany(int number)
    {
        if (requirement.requirement == "")
        {
            musicController.CoinSound();
            allPoints.money -= CompanyValue;
            NumberOfCompany += number;
            CompanyValue += SetCompanyValue;
            allPoints.AddNature(afectNature);
            allPoints.Addfood(afectFood);
            allPoints.AddPower(afectEnergy);
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
            if (NumberOfCompany > 0)
            {
                musicController.CoinSound();
                allPoints.money -= UpgradeValue;
                NumberOfUpgrades += number;
                UpgradeValue += SetUpgradeValue;
                allPoints.AddNature(afectNature / 2);
                allPoints.Addfood(afectFood / 2);
                allPoints.AddPower(afectEnergy / 2);
                allPoints.AddPopulation(afectPopulation / 2);
            }
            else
            {
                musicController.ClickSound();
                errorMessage.Instantiate("You Need a Company Before");
            }
        }
        else
        {
            musicController.ClickSound();
            errorMessage.Instantiate(requirement.requirement);
        }
    }
}
