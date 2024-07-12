using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerStats : MonoBehaviour
{
    public float damage, attackSpeed, range, exp, level, expMax;
    public int ID, upgradePoints; //need to set the tower ID manually in the inspector
    private static int towerCount = 0;
    private TowerController controller;
    public TextMeshProUGUI[] thisTowerStats; //tower stats for this tower [damage, fire rate, range]
    public TextMeshProUGUI thisTowerHeader;
    public Button[] thisTowerButtons; //upgrade buttons corresponding to their stats
    private SphereCollider attackCircle;
    private int[] upgradeCount = {1,1,1};
    public AudioSource audio;
    public AudioClip clip;

    private void Start()
    {
        towerCount++;
        damage = 1;
        attackSpeed = 1.25f;
        range = 1.8f;
        exp = 0;
        level = 0;
        expMax = 1;
        upgradePoints = 0;

        controller = GetComponent<TowerController>();
        attackCircle = GetComponentInParent<SphereCollider>();
        attackCircle.radius = range;
        audio.clip = clip;

        thisTowerStats[0].text = string.Format(">Damage: {0:00}", 1);
        thisTowerStats[1].text = string.Format(">Fire Rate: {0:00}", 1);
        thisTowerStats[2].text = string.Format(">Range: {0:00}", 1);

        thisTowerHeader.text = string.Format(">Tower #{0:0} upgrade points: ", ID) + string.Format("{0:00}", upgradePoints);

        StartCoroutine(Upgrade());
    }

    private void OnEnable()
    {
        Enemy.giveExp += LevelUp;
    }

    private void OnDisable()
    {
        Enemy.giveExp -= LevelUp;
    }

    private void LevelUp(float expGain, int towerID)
    {
        //only gives exp to the tower which killed the enemy
        if(towerID == ID)
        {
            exp += expGain;

            if (exp >= expMax)
            {
                level++;
                exp -= expMax;
                expMax *= 1.3f; //raise amount of exp needed to level up
                upgradePoints++;
                thisTowerHeader.text = string.Format(">Tower #{0:0} upgrade points: ", ID) + string.Format("{0:00}", upgradePoints);
                audio.Play();
            }

            controller.targeter.targets.Remove(controller.target);
            controller.target = null;
        }
    }

    //shows upgrade buttons only if there is one available
    private IEnumerator Upgrade()
    {
        int toggleActivate = 0;

        while(true)
        {
            if(upgradePoints <= 0)
            {
                thisTowerButtons[0].gameObject.SetActive(false);
                thisTowerButtons[1].gameObject.SetActive(false);
                thisTowerButtons[2].gameObject.SetActive(false);
                toggleActivate = 0;
            }

            yield return new WaitUntil(() => upgradePoints > 0);

            if(toggleActivate == 0)
            {
                thisTowerButtons[0].gameObject.SetActive(true);
                thisTowerButtons[1].gameObject.SetActive(true);
                thisTowerButtons[2].gameObject.SetActive(true);
                toggleActivate = 1;
            }
        }
    }

    //when an upgrade button is clicked this is called
    public void upgradeStat(int id)
    {
        if(id == 0) //damage increase
        {
            damage *= 1.1f;
            upgradeCount[0]++;
            thisTowerStats[0].text = string.Format(">Damage: {0:00}", upgradeCount[0]);
        }
        else if(id == 1) //attack speed increase
        {
            attackSpeed *= 0.9f;
            upgradeCount[1]++;
            thisTowerStats[1].text = string.Format(">Fire Rate: {0:00}", upgradeCount[1]);
        }
        else if(id == 2) //range increase
        {
            range *= 1.1f;
            attackCircle.radius = range;
            upgradeCount[2]++;
            thisTowerStats[2].text = string.Format(">Range: {0:00}", upgradeCount[2]);
        }

        upgradePoints--;
        thisTowerHeader.text = string.Format(">Tower #{0:0} upgrade points: ", ID) + string.Format("{0:00}", upgradePoints);
    }
}
