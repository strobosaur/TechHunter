using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquad : MonoBehaviour
{
    public Transform moveTarget;
    public IMoveInput moveInput;

    public List<Enemy> enemyList = new List<Enemy>();

    void Awake()
    {
        moveInput = GetComponent<IMoveInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
