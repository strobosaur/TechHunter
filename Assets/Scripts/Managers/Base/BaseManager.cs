using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseManager : MonoBehaviour
{
    // WEAPON UPGRADES
    public List<TMP_Text> wpnUpgTexts;
    public List<TMP_Text> wpnUpgFields;
    public TMP_Text upgMessage;

    // PLAYER STATS
    public List<TMP_Text> playerStatTexts;

    public BaseMenuStateMachine stateMachine;
    public BaseMenuStateIdle stateIdle;
    public BaseMenuStateUpgrade stateUpgrade;

    void Awake()
    {
        stateMachine = new BaseMenuStateMachine();
        stateIdle = new BaseMenuStateIdle(this, stateMachine);
        stateUpgrade = new BaseMenuStateUpgrade(this, stateMachine);
    }

    void Start()
    {
        stateMachine.Initialize(stateIdle);
        UpdatePlayerTexts();
    }

    // UPDATE PLAYER STAT TEXTS
    public void UpdatePlayerTexts()
    {
        for (int i = 0; i < playerStatTexts.Count; i++)
        {
            if (i == (int)PlayerStatTexts.hp) playerStatTexts[i].text = PlayerManager.instance.playerStats.HPmax.GetValue().ToString();
            if (i == (int)PlayerStatTexts.armor) playerStatTexts[i].text = PlayerManager.instance.playerStats.armor.GetValue().ToString();
            if (i == (int)PlayerStatTexts.moveSpd) playerStatTexts[i].text = PlayerManager.instance.playerStats.moveSpd.GetValue().ToString();

            if (i == (int)PlayerStatTexts.frate) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.frate.GetValue().ToString();
            if (i == (int)PlayerStatTexts.brate) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.brate.GetValue().ToString();
            if (i == (int)PlayerStatTexts.range) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.range.GetValue().ToString();
            if (i == (int)PlayerStatTexts.spread) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.spr.GetValue().ToString();
            if (i == (int)PlayerStatTexts.knockback) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.knockback.GetValue().ToString();
            if (i == (int)PlayerStatTexts.shots) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.shots.GetValue().ToString();
            if (i == (int)PlayerStatTexts.burst) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.burst.GetValue().ToString();
        }
    }
}

public enum PlayerStatTexts
{
    hp,
    armor,
    moveSpd,
    dmg,
    frate, 
    brate,
    range,
    spread,
    knockback,
    shots,
    burst
}