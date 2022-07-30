using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter, IDamageable
{
    // TARGET
    public Transform target;

    // STATE MACHINE SETUP
    public EnemyStateMachine stateMachine { get; protected set; }

    public StateEnemyIdle stateIdle { get; protected set; }
    public StateEnemyMove stateMove { get; protected set; }

    // ENEMY PARAMETERS
    public float chargeDist;
    
    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // PLAYER DATA
        data = new EntityData();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();

        // CREATE STATE MACHINE
        stateMachine = new EnemyStateMachine();
        stateIdle = new StateEnemyIdle(this, stateMachine, "idle");
        stateMove = new StateEnemyMove(this, stateMachine, "move");

        // CREATE WEAPON
        weapon.SetWeaponParams(new WeaponParams(true, 2f, 0.3f, 1, 1.5f, 0, 0, 0, 0.5f, 1));

        // GET TARGET
        if (target == null)
            target = FindTarget();
    }

    // START
    protected override void Start()
    {
        stateMachine.Iinitialize(stateIdle);
    }

    // UPDATE
    protected override void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    // FIXED UPDATE
    protected override void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    // RECEIVE DAMAGE
    public void ReceiveDamage(DoDamage damage, Vector2 originDir)
    {
        rb.AddForce(originDir * damage.force, ForceMode2D.Impulse);
        stats.TakeDamage(damage.damage);
        CheckDeath(stats);
        hitflash.Flash();
    }

    // CHECK DAMAGE
    public void CheckDeath(EntityStats stats)
    {
        if (stats.HPcur <= 0f)
        {
            Destroy(gameObject);
        }
    }

    // FIND NEW TARGET
    public Transform FindTarget()
    {
        return GameManager.instance.playerManager.playerList[Random.Range(0,GameManager.instance.playerManager.playerList.Count)].transform;
    }
}
