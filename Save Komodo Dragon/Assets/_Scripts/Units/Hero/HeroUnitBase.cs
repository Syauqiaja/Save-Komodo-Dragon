using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroUnitBase : UnitBase
{
    public Image healthBar;
    public Gradient healthGradient;
    public WeaponStats Weapon {get;private set;}
    public void SetWeapon(WeaponStats stats) => Weapon = stats;

    protected Rigidbody2D _r;
    protected float _timeElapsed = 0f;
    protected SpriteRenderer spriteRenderer;
    [HideInInspector] public Vector3 facingDirection;
    [HideInInspector] public List<Transform> enemyFront = new List<Transform>();

    // Enemy hit detector
    private EnemyUnitBase enemiesTouched = null;
    

    protected virtual void Awake() {
        _r = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingDirection = transform.right;
    }
    private void Start() {
        GameManager.OnBeforeStateChanged += BeforeState;
    }
    public void BeforeState(GameState gameState){
        // if(gameState == GameState.LevelUp){
        //     LevelUp();
        // }else if(gameState == GameState.Starting){
        //     Starting();
        // }
    }
    private void Update() {
        MoveUnit();
        if(_timeElapsed < Time.time){
            _timeElapsed = Time.time + 1f/Weapon.FiringRate;
            Attack(transform.position, facingDirection);
        }
    }
    public void UpdateHealthBar(){
        healthBar.fillAmount = (float) currentHealth / BaseStats.health;
        healthBar.color = healthGradient.Evaluate(healthBar.fillAmount);
    }
    protected virtual void Attack(Vector3 CenterPos, Vector3 direction){
    }
    protected virtual void MoveUnit(){
        Vector2 inputJoystick = Joystick.GetJoystickAxis();
        if(inputJoystick.magnitude > 0.1f) {
            facingDirection = inputJoystick.normalized;
            spriteRenderer.flipX = facingDirection.x > 0?false:true;
        }
        _r.velocity = inputJoystick * BaseStats.travelSpeed;
    }

    public override void Damaged(int hitValue)
    {
        base.Damaged(hitValue);
        UpdateHealthBar();
    }
    public override void Death()
    {
        base.Death();
        GameManager.Instance.ChangeState(GameState.Lose);
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.collider.CompareTag("Enemy")){
    //         Damaged(other.gameObject.GetComponent<EnemyUnitBase>().hitDamage);
    //     }else if(other.collider.CompareTag("BossBound")){
    //         Death();
    //     }
    // }

}