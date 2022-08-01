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
    public StateEnemyCharge stateCharge { get; protected set; }
    public StateEnemyAttack stateAttack { get; protected set; }

    // ENEMY PARAMETERS
    public float chargeDist;
    
    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // PLAYER DATA
        data = new EntityData();
        //stats = GetComponent<EntityStats>();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();

        // CREATE WEAPON
        if (weapon == null)
            weapon = GetComponent<IWeapon>();
        wpnStats = new WeaponParams(true, 2f, 0.3f, 1, 1.5f, 1, 0, 0, 0.5f, 1);
        weapon.SetWeaponParams(wpnStats);

        chargeDist = 8f;

        // GET TARGET
        if (target == null)
            target = FindTarget();

        // CREATE STATE MACHINE
        stateMachine = new EnemyStateMachine();
        stateIdle = new StateEnemyIdle(this, stateMachine, "idle");
        stateMove = new StateEnemyMove(this, stateMachine, "move");
        stateCharge = new StateEnemyCharge(this, stateMachine, "charge");
        stateAttack = new StateEnemyAttack(this, stateMachine, "attack");
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
