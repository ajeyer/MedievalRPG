using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 100;
    //public int[] mpLvlBonus; //Creates an array to manually add the increase of level xp
    public int strength;
    public int defence;
    public int weaponPower;
    public int armorPower;
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;


    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(500);
        }
    }


    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP  -=expToNextLevel[playerLevel];

                playerLevel++;

                //Raise HP with level increase
                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                //Raise MP with level increase
                maxHP = Mathf.FloorToInt(maxMP * 1.08f);
                currentMP = maxMP;

                //maxMP += mpLvlBonus[playerLevel];
                //currentMP = maxMP

            }
        }

        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }

    }




}
