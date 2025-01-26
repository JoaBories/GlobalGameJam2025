using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static PlayerUpgrade Instance;

    public int nbOfDamageUpgrade = 0;
    public int nbOfSpeedUpgrade = 0;
    public int nbOfKnockbackUpgrade = 0;
    public int nbOfFirerateUpgrade = 0;

    public float DamageUpgradeAmount;
    public float SpeedUpgradeAmount;
    public float KnockbackUpgradeAmount;
    public float FirerateUpgradeAmount;
    public float HealUpgradeAmount;

    private void Awake()
    {
        Instance = this;
    }

    public void GetUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                nbOfKnockbackUpgrade++;

                break;

            case 1:
                nbOfFirerateUpgrade++;

                break;

            case 2:
                PlayerLife.Instance.Heal(HealUpgradeAmount);

                break;

            case 3:
                nbOfDamageUpgrade++;

                break;

            case 4:
                nbOfSpeedUpgrade++;

                break;

        }
    }
}
