using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter, IDamageable
{
    // PLAYER DATA
    public PlayerData data;
    public PlayerStats stats2;
    public PlayerMoveBoost moveBoost;
    public PlayerInvincibility invincibility;

    // STATE MACHINE
    public PlayerStateMachine stateMachine { get; private set; }

    public StatePlayerIdle stateIdle { get; private set; }
    public StatePlayerMove stateMove { get; private set; }

    // CROSSHAIR
    public Crosshair crosshair { get; private set; }

    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // PLAYER DATA
        data = new PlayerData();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();
        invincibility = GetComponent<PlayerInvincibility>();

        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();

        // CREATE WEAPON
        weapon = GetComponent<IWeapon>();
        wpnStats = new WeaponParams(false, 1.5f, 0.1f, 1f, 15f, 0.5f, 0.15f, 32f, 1f, 8);
        weapon.SetWeaponParams(wpnStats);
        stats2 = new PlayerStats(2f, 3f, 10f, 1f);
        moveBoost = GetComponent<PlayerMoveBoost>();

        // CREATE STATE MACHINE
        stateMachine = new PlayerStateMachine();
        stateIdle = new StatePlayerIdle(this, stateMachine, "idle");
        stateMove = new StatePlayerMove(this, stateMachine, "move");
    }

    // START
    protected override void Start()
    {
        stateMachine.Iinitialize(stateIdle);
    }

    // UPDATE
    protected override void Update()
    {
        if (crosshair == null) GameObject.Find("Crosshair").GetComponent<Crosshair>();
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
        if (!invincibility.isInvincible)
        {
            rb.AddForce(originDir * damage.force, ForceMode2D.Impulse);
            stats.TakeDamage(damage.damage);
            CheckDeath(stats);
            hitflash.Flash();
            stats2.lastDamage = Time.time;
            invincibility.SetInvincible();
        }
    }

    public void CheckDeath(EntityStats stats)
    {
        if (stats.HPcur <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
