﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationsItens : MonoBehaviour {

    public int TotalMoney;
    public int TotalMoneyOfCompany;
    public int NumberOfCompany;
    public int NumberOfUpgrades;

    public int MoneyPerUpgrade;
    public int MoneyPerCompany;

    public int CompanyValue;
    public int UpgradeValue;

    public bool company;
    public bool upgrade;

    public int afectArmy;
    public int afectWater;
    public int afectFood;
    public int afectPopulation;
    public int afectEnergy;
    public int afectNature;
    private MoneyCollect moneyCollect;
    private AllPoints allPoints;

    private void Start()
    {
        allPoints = GameObject.Find("PointsController").GetComponent<AllPoints>();
        if (allPoints == null)
        {
            Debug.Log("The script technology dont fint the Game Object 'TimeController'");
        }
        moneyCollect = GameObject.Find("PointsController").GetComponent<MoneyCollect>();
        if (moneyCollect == null)
        {
            Debug.Log("The script technology dont fint the Game Object 'TimeController'");
        }
    }

    void Update()
    {
        TotalMoneyOfCompany = MoneyPerCompany + MoneyPerUpgrade * NumberOfUpgrades;
        TotalMoney = TotalMoneyOfCompany * NumberOfCompany;

        if (company)
        {
            BuyCompany(1);
            company = !company;
        }
        if (upgrade)
        {
            if (NumberOfCompany > 0)
                BuyUpgrade(1);
            upgrade = !upgrade;
        }

        moneyCollect.ReciveMoney(5, TotalMoney);
    }

    public void BuyCompany(int number)
    {
        NumberOfCompany += number;
        CompanyValue += CompanyValue / 2;
        allPoints.AddArmy(afectArmy);
        allPoints.AddWater(afectWater);
        allPoints.Addfood(afectFood);
        allPoints.AddPopulation(afectPopulation);
        allPoints.AddPower(afectEnergy);
        allPoints.AddNature(afectNature);
    }

    public void BuyUpgrade(int number)
    {
        NumberOfUpgrades += number;
        UpgradeValue += UpgradeValue / 2;
        allPoints.AddArmy(afectArmy / 2);
        allPoints.AddWater(afectWater / 2);
        allPoints.Addfood(afectFood / 2);
        allPoints.AddPopulation(afectPopulation / 2);
        allPoints.AddPower(afectEnergy / 2);
        allPoints.AddNature(afectNature / 2);
    }
}