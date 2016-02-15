using UnityEngine;
using System.Collections;

public class CharacterStatsBase : CharatcerStatsAbstract {

	public new float speed;
	public new Room basicRoom;
	// Health
	public new float maxHealth;
	public new float health;
	public new float healthIncrease;
	public new float healthRegeneration;
	public new float healthReduction;
	public new float healthThreshold;
	// Fighting
	public new float damage;
	public new float attackRate;
	public new float attackCoolDown;
	public new bool abbleDistantAttack;

	public override float Speed { get { return speed; } }
	public override Room BasicRoom { get { return basicRoom; } }
	// Health
	public override float MaxHealth { get { return maxHealth; } }
	public override float Health { get { return health; } set { health = value; } }
	public override float HealthIncrease { get { return healthIncrease; } }
	public override float HealthRegeneration { get { return healthRegeneration; } }
	public override float HealthReduction { get { return healthReduction; } }
	public override float HealthThreshold { get { return healthThreshold; } }
	// Fighting
	public override float Damage { get { return damage; } }
	public override float AttackRate { get { return attackRate; } }
	public override float AttackCoolDown { get { return attackCoolDown; } set { attackCoolDown = value; }  }
	public override bool AbbleDistantAttack { get { return abbleDistantAttack; } }
	public delegate void AttackDelegate();
	public event AttackDelegate attackReady;

	public virtual void Init(CharacterStatsConstructor constructor)
	{
		speed = constructor.Speed;
		basicRoom = constructor.BasicRoom;
		maxHealth = constructor.MaxHealth;
		health = constructor.Health;
		healthIncrease = constructor.HealthIncrease;
		healthRegeneration = constructor.HealthRegeneration;
		healthReduction = constructor.HealthReduction;
		healthThreshold = constructor.HealthThreshold;
		damage = constructor.Damage;
		attackRate = constructor.AttackRate;
		attackCoolDown = 0f;
		abbleDistantAttack = constructor.AbbleDistantAttack;
	}

	public virtual void StatsUpdate()
	{
		if (health >= 10f) health = health + healthRegeneration - healthReduction;
		health = Mathf.Clamp(Health, -10f, MaxHealth);
	}

	public virtual void Purge ()
	{
		attackReady = null;
	}

	public void Update()
	{
		if (attackCoolDown > 0f) 
		{
			attackCoolDown -= Time.deltaTime;
		}
		else 
		{
			if (attackReady != null) attackReady();
		}
	}

	public bool IsDead ()
	{
		return Health <= 0f;
	}

	public bool IsUnconscious ()
	{
		return (Health <= 10f && !IsDead());
	}

	public bool IsWounded ()
	{
		return (Health < MaxHealth && !IsDead() && !IsUnconscious ());
	}

}
