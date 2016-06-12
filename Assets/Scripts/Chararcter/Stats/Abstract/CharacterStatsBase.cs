using UnityEngine;
using System.Collections;

public class CharacterStatsBase : CharatcerStatsAbstract {

	public new Enums.CharacterSides side;
	public new Enums.CharacterTypes type;
	public new float walkSpeed;
	public new float runSpeed;
	public new IRoom basicRoom;
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

	public override Enums.CharacterSides Side { get { return side; } set { side = value; } }
	public override Enums.CharacterTypes Type { get { return type; } set { type = value; } }
	public override float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }
	public override float RunSpeed { get { return runSpeed; } set { runSpeed = value; } }
	public override IRoom BasicRoom { get { return basicRoom; } set { basicRoom = value; }  }
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

	public virtual void Init()
	{
		AttackCoolDown = 0f;
		Broadcaster.Instance.tickEvent += StatsUpdate;
	}
	
	public virtual void StatsUpdate ()
	{
		if (Health >= 10f) Health = Health + HealthRegeneration - HealthReduction;
		Health = Mathf.Clamp(Health, -10f, MaxHealth);
		HealthReduction = Mathf.Clamp(HealthReduction, 0f, MaxHealth);
	}
	
	public override void Purge ()
	{
		base.Purge ();
		attackCoolDown = 0f;
	}
	
}
