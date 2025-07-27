using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : Targetable
{
    public Weapon weapon;
    public Targetable target;

    public event Action WeaponDropped;

    protected Rigidbody2D m_rigidbody;

    protected override void ManagedInitialize()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        if(!weapon)
            weapon = GetComponentInChildren<Weapon>();

        base.ManagedInitialize();
    }
    public override void ManagedUpdate()
    {
        if (target)
            LookAt(target.transform.position);
    }

    public void MoveAwayFrom(Vector3 targetPosition, float speed)
    {
        var direction = (transform.position - targetPosition).normalized;
        MoveBy(direction * (speed * Time.deltaTime));
    }
    public void MoveTowards(Vector3 targetPosition, float speed, float slowWhenNear = 0)
    {
        var direction = (targetPosition - transform.position).normalized;
        var distance = Vector2.Distance(targetPosition, transform.position);
        var s = speed * Mathf.Clamp01(distance/slowWhenNear);
        var velocity = direction * s;
        MoveBy(velocity * Time.deltaTime);
    }
    
    public void MoveBy(Vector2 movement)
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        m_rigidbody.MovePosition(currentPos + movement);
    }

    public virtual void TryAttacking()
    {
        weapon?.TryAttacking();
    }

    //rotate towards a target
    public void LookAt(Vector2 target)
    {
        Vector2 lookDirection = target - (Vector2)transform.position;
        transform.right = lookDirection;
    }

    public void PickupWeapon(Weapon newWeapon)
    {
        if(weapon == newWeapon) 
            return;
        
        if(weapon != null)
            DropWeapon();

        Debug.Log("Picked up " +  newWeapon.name);
        newWeapon.SetEquipped(this);
        weapon = newWeapon;
    }

    public void DropWeapon()
    {
        WeaponDropped?.Invoke();
        weapon = null;
    }
}
