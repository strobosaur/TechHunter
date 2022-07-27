using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerIdle : PlayerState
{
    public StatePlayerIdle(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
