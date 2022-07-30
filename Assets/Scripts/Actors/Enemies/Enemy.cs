using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter, IDamageable
{
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
        stateMachine = new StateMachine();
        stateIdle = new StateIdle(this, stateMachine, "idle");
        stateMove = new StateMove(this, stateMachine, "move");

        // CREATE WEAPON
        weapon.SetWeaponParams(new WeaponParams(true, 2f, 0.3f, 1, 1.5f, 0, 0, 0, 0.5f, 1));
    }

    protected override void Start()
    {
        stateMachine.Iinitialize(stateIdle);
    }

    protected override void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    protected override void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public void ReceiveDamage(DoDamage damage, Vector2 originDir)
    {
        rb.AddForce(originDir * damage.force, ForceMode2D.Impulse);
        stats.TakeDamage(damage.damage);
        CheckDeath(stats);
        hitflash.Flash();
    }

    public void CheckDeath(EntityStats stats)
    {
        if (stats.HPcur <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public Transform FindTarget()
    {
        return GameObject.FindWithTag("Player").transform;
    }
}
