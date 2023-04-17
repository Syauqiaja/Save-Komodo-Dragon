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
    [HideInInspector] public List<Transform> enemyNear = new List<Transform>();

    protected virtual void Awake() {
        _r = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingDirection = transform.right;
    }
    private void Update() {
        MoveUnit();
    }
    public void UpdateHealthBar(){
        maxHealth = Mathf.FloorToInt(BaseStats.health * ClothHandler.Instance.maxHealthMultiplier);
        healthBar.fillAmount = (float) currentHealth / maxHealth;
        healthBar.color = healthGradient.Evaluate(healthBar.fillAmount);
    }
    public void SetSprite(Sprite sprite){
        spriteRenderer.sprite = sprite;
    }
    protected virtual void MoveUnit(){
        Vector2 inputJoystick = Joystick.GetJoystickAxis();
        if(inputJoystick.magnitude > 0.1f) {
            facingDirection = inputJoystick.normalized;
            spriteRenderer.flipX = facingDirection.x > 0?false:true;
        }
        _r.velocity = inputJoystick * BaseStats.travelSpeed * ClothHandler.Instance.speedMultiplier;
    }

    public override void Damaged(float hitValue)
    {   
        // Damaging every 0.2s
        base.Damaged(Mathf.CeilToInt(hitValue * ClothHandler.Instance.damageReduce) * Time.deltaTime * 10f);
        UpdateHealthBar();
    }
    public void Heal(int healAmount){
        currentHealth += healAmount;
        UpdateHealthBar();
    }
    public void Heal(float healAmount){
        currentHealth += Mathf.FloorToInt(healAmount * maxHealth);
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