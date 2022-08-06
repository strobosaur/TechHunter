using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter, IDamageable
{
    // TARGET
    public Transform target;

    public float animSpd = 0.35f;
    public Vector2 facingDir;
    public Vector2 aimTarget;

    // ENEMY STATS
    public EntityStats statsBlueprint;

    // STATE MACHINE SETUP
    public EnemyStateMachine stateMachine { get; protected set; }

    public StateEnemyIdle stateIdle { get; protected set; }
    public StateEnemyMove stateMove { get; protected set; }
    public StateEnemyCharge stateCharge { get; protected set; }
    public StateEnemyAttack stateAttack { get; protected set; }

    public float[] timerArr;

    // ENEMY PARAMETERS
    public float chargeDist;
    
    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // TIMERS
        timerArr = new float[(int)EnemyTimers.end];

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();

        // INIT STATS
        StatsInit(statsBlueprint, wpnStats);

        // CREATE WEAPON
        if (weapon == null)
            weapon = GetComponent<IWeapon>();

        chargeDist = 8f;

        // GET TARGET
        if (target == null)
            target = FindTarget();

        // CREATE STATE MACHINE
        stateMachine = new EnemyStateMachine();
        stateIdle = new StateEnemyIdle(this, stateMachine);
        stateMove = new StateEnemyMove(this, stateMachine);
        stateCharge = new StateEnemyCharge(this, stateMachine);
        stateAttack = new StateEnemyAttack(this, stateMachine);
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
        if (weapon == null)
            weapon = GetComponent<IWeapon>();
    }

    // FIXED UPDATE
    protected override void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    // RECEIVE DAMAGE
    public void ReceiveDamage(DoDamage damage, Vector2 originDir)
    {
        // CREATE BLOOD
        Instantiate(EffectsManager.instance.SpawnBlood01(transform.position));
        
        rb.AddForce(originDir * damage.force, ForceMode2D.Impulse);
        
        stats.TakeDamage(damage.damage);
        CheckDeath(stats);
        
        hitflash.Flash();
    }

    // CHECK DAMAGE
    public void CheckDeath(StatsObject stats)
    {
        if (stats.HPcur <= 0f)
        {
            // CREATE BLOOD
            Instantiate(EffectsManager.instance.SpawnBlood02(transform.position));

            EnemyManager.instance.enemyList.Remove(this);
            Inventory.instance.kills++;
            if (Random.value < 0.05) Inventory.instance.scraps += 1;
            Destroy(gameObject);
        }
    }

    // FIND NEW TARGET
    public Transform FindTarget()
    {
        return GameManager.instance.playerManager.playerList[Random.Range(0,GameManager.instance.playerManager.playerList.Count)].transform;
    }
}

// ENEMY TIMERS
public enum EnemyTimers
{
    chargeState,
    end
}