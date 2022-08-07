using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseManager : MonoBehaviour
{
    public TMP_Text[] wpnUpgTexts;
    public TMP_Text[] wpnUpgFields;
    public TMP_Text upgMessage;

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
    }
}
