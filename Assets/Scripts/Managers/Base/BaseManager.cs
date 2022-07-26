using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseManager : MonoBehaviour
{
    // RESOURCE TEXTS
    public TMP_Text scrapsText, techText, HPmax, HPcur;

    // CANVAS GROUPS
    public CanvasGroup upgradeGroup;
    public CanvasGroup playerGroup;

    // MISSION COMPONENT
    public BaseMissionManager missionManager;

    // WEAPON UPGRADES
    public UpgradeManager upgManager;
    public List<TMP_Text> wpnUpgTexts;
    public List<TMP_Text> wpnUpgFieldsScraps;
    public List<TMP_Text> wpnUpgFieldsTech;
    public TMP_Text upgMessage;

    // PLAYER STATS
    public List<TMP_Text> playerStatTexts;

    public BaseMenuStateMachine stateMachine;
    public BaseMenuStateEnter stateEnter;
    public BaseMenuStateIdle stateIdle;
    public BaseMenuStateUpgrade stateUpgrade;
    public BaseMenuStateMission stateMission;
    public BaseMenuStateMissionExit stateMissionExit;

    void Awake()
    {
        // GET COMPONENT REFERENCES
        upgManager = GameManager.instance.upgManager;
        missionManager = GetComponent<BaseMissionManager>();
        upgradeGroup = GameObject.Find("UpgradeMenuContainer").GetComponent<CanvasGroup>();
        playerGroup = GameObject.Find("PlayerMenuContainer").GetComponent<CanvasGroup>();

        // INITIALIZE STATE MACHINE
        stateMachine = new BaseMenuStateMachine();
        stateEnter = new BaseMenuStateEnter(this, stateMachine);
        stateIdle = new BaseMenuStateIdle(this, stateMachine);
        stateUpgrade = new BaseMenuStateUpgrade(this, stateMachine);
        stateMission = new BaseMenuStateMission(this, stateMachine);
        stateMissionExit = new BaseMenuStateMissionExit(this, stateMachine);
    }

    void OnEnable()
    {
        UpdateResourceTexts();
        PlayerManager.instance.onHPchanged += UpdateHP;
    }

    void OnDisable()
    {
        PlayerManager.instance.onHPchanged -= UpdateHP;
    }

    void Start()
    {
        // INIT STATE MACHINE
        stateMachine.Initialize(stateEnter);
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public void UpdateHP()
    {
        HPmax.text = Mathf.RoundToInt(PlayerManager.instance.playerStats.HPmax.GetValue()).ToString();
        HPcur.text = Mathf.RoundToInt(PlayerManager.instance.playerStats.HPcur).ToString();
    }
    
    public void UpdateResourceTexts()
    {
        scrapsText.text = Inventory.instance.scraps.ToString();
        techText.text = Inventory.instance.techUnits.ToString();
    }

    // UPDATE PLAYER STAT TEXTS
    public void UpdatePlayerTexts()
    {
        for (int i = 0; i < playerStatTexts.Count; i++)
        {
            if (i == (int)PlayerStatTexts.hp) playerStatTexts[i].text = PlayerManager.instance.playerStats.HPmax.GetValue().ToString();
            if (i == (int)PlayerStatTexts.armor) playerStatTexts[i].text = PlayerManager.instance.playerStats.armor.GetValue().ToString();
            if (i == (int)PlayerStatTexts.moveSpd) playerStatTexts[i].text = PlayerManager.instance.playerStats.moveSpd.GetValue().ToString();

            if (i == (int)PlayerStatTexts.dmg) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.dmg.GetValue().ToString();
            if (i == (int)PlayerStatTexts.frate) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.frate.GetValue().ToString();
            if (i == (int)PlayerStatTexts.brate) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.brate.GetValue().ToString();
            if (i == (int)PlayerStatTexts.range) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.range.GetValue().ToString();
            if (i == (int)PlayerStatTexts.spread) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.spr.GetValue().ToString();
            if (i == (int)PlayerStatTexts.knockback) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.knockback.GetValue().ToString();
            if (i == (int)PlayerStatTexts.shots) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.shots.GetValue().ToString();
            if (i == (int)PlayerStatTexts.burst) playerStatTexts[i].text = PlayerManager.instance.playerStats.wpnStats.burst.GetValue().ToString();
        }
    }

    public void TogglePlayerMenu(bool show = true)
    {
        if (show) {
            playerGroup.alpha = 1f;
            playerGroup.interactable = true;
        } else {
            playerGroup.alpha = 0f;
            playerGroup.interactable = false;
        }
    }

    public void ToggleUpgradeMenu(bool show = true)
    {
        if (show) {
            upgradeGroup.alpha = 1f;
            upgradeGroup.interactable = true;
        } else {
            upgradeGroup.alpha = 0f;
            upgradeGroup.interactable = false;
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