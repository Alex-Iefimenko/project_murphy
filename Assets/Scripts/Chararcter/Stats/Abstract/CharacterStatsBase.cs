using UnityEngine;
using System.Collections;

public class CharacterStatsBase : CharatcerStatsAbstract {

	public new float walkSpeed;
	public new float runSpeed;
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
	
	public override float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }
	public override float RunSpeed { get { return runSpeed; } set { runSpeed = value; } }
	public override Room BasicRoom { get { return basicRoom; } set { basicRoom = value; }  }
	// Health
	public override float MaxHealth { get { return maxHealth; } set { maxHealth = value; }  }
	public override float Health { get { return health; } set { health = value; } }
	public override float HealthIncrease { get { return healthIncrease; } set { healthIncrease = value; }  }
	public override float HealthRegeneration { get { return healthRegeneration; } set { healthRegeneration = value; }  }
	public override float HealthReduction { get { return healthReduction; } set { healthReduction = value; } }
	public override float HealthThreshold { get { return healthThreshold; } set { healthThreshold = value; }  }
	// Fighting
	public override float Damage { get { return damage; } set { damage = value; }  }
	public override float AttackRate { get { return attackRate; } set { attackRate = value; }  }
	public override float AttackCoolDown { get { return attackCoolDown; } set { attackCoolDown = value; }  }
	public override bool AbbleDistantAttack { get { return abbleDistantAttack; } set { abbleDistantAttack = value; }  }
	public delegate void AttackDelegate();
	public event AttackDelegate attackReady;

	public virtual void Init(CharacterMain character)
	{
		RelatedCharacter = character;
		AttackCoolDown = 0f;
	}
	
	public virtual void StatsUpdate()
	{
		if (Health >= 10f) Health = Health + HealthRegeneration - HealthReduction;
		Health = Mathf.Clamp(Health, -10f, MaxHealth);
	}
	
	public virtual void Purge ()
	{
		attackReady = null;
		attackCoolDown = 0f;
	}

	public void Update()
	{
		if (AttackCoolDown > 0f) 
		{
			AttackCoolDown -= Time.deltaTime;
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
	
	public bool IsActive ()
	{
		return !(IsDead() || IsUnconscious());

	}
}
