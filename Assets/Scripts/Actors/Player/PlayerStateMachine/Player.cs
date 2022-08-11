using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter, IDamageable
{
    // PLAYER DATA
    public PlayerMoveBoost moveBoost;
    public PlayerInvincibility invincibility;
    public PlayerInteraction interaction;

    // STATS
    public float animSpd = 0.35f;
    public Vector2 facingDir;
    public Vector2 aimTarget;
    public float lastDamage;

    // STATE MACHINE
    public PlayerStateMachine stateMachine { get; private set; }

    public StatePlayerIdle stateIdle { get; private set; }
    public StatePlayerMove stateMove { get; private set; }
    public StatePlayerDisabled stateDisabled { get; private set; }

    // CROSSHAIR
    public Crosshair crosshair { get; private set; }

    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();
        invincibility = GetComponent<PlayerInvincibility>();
        interaction = GetComponent<PlayerInteraction>();

        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();

        //StatsInit(statsBlueprint, wpnStats);
        //stats = PlayerManager.instance.playerStats;

        // CREATE WEAPON
        weapon = GetComponent<IWeapon>();
        //wpnStats = new WeaponParams(false, 1.5f, 0.1f, 1f, 15f, 0.5f, 0.15f, 32f, 1f, 1, 1, 5);
        //weapon.SetWeaponParams(wpnStats);
        moveBoost = GetComponent<PlayerMoveBoost>();

        // CREATE STATE MACHINE
        stateMachine = new PlayerStateMachine();
        stateIdle = new StatePlayerIdle(this, stateMachine);
        stateMove = new StatePlayerMove(this, stateMachine);
        stateDisabled = new StatePlayerDisabled(this, stateMachine);
    }

    // START
    protected override void Start()
    {
        stats = PlayerManager.instance.playerStats;
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

    // ON COLLISION ENTER
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if ((invincibility.isInvincible) && (coll.gameObject.tag == "Enemy"))
        {
            Physics2D.IgnoreCollision(coll.collider, coll.otherCollider);
        }
    }

    // RECEIVE DAMAGE
    public void ReceiveDamage(DoDamage damage, Vector2 originDir)
    {
        if (!invincibility.isInvincible)
        {
            // CREATE BLOOD
            Instantiate(EffectsManager.instance.SpawnBlood01(transform.position));

            // TAKE DAMAGE
            rb.AddForce(originDir * damage.force, ForceMode2D.Impulse);
            stats.TakeDamage(damage.damage);
            hitflash.Flash();
            lastDamage = Time.time;
            invincibility.SetInvincible();

            // CHECK DEATH
            CheckDeath(stats);

            // UPDATE HUD
            HUDlevel.instance.onHPchanged?.Invoke();
        }
    }

    // CHECK PLAYER DEATH
    public void CheckDeath(StatsObject stats)
    {
        if (stats.HPcur <= 0f)
        {
            // CREATE BLOOD
            Instantiate(EffectsManager.instance.SpawnBlood02(transform.position));

            // DESTROY OBJECT
            PlayerManager.instance.onGameOver?.Invoke();
            Destroy(gameObject);
        }
    }
}
