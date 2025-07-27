using UnityEngine;

public class ChasePlayerEffect : Effect
{
    public float slowWhenNearTarget;
    public float chaseSpeed;
    public override void DoEffect(Unit me)
    {
        me.MoveTowards(Player.instance.transform.position, chaseSpeed, slowWhenNearTarget);
        me.LookAt(Player.instance.transform.position);
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Chase Player";
        node.AddTextField("Move speed", x =>
        {
            if (int.TryParse(x.newValue, out int newAmount))
                chaseSpeed = newAmount;
        }).value = chaseSpeed.ToString();
        node.AddTextField("Slow when near target", x =>
        {
            if (int.TryParse(x.newValue, out int newAmount))
                slowWhenNearTarget = newAmount;
        }).value = slowWhenNearTarget.ToString();
    }
}


public class AttackEffect : Effect
{
    public int amount;

    public override void DoEffect(Unit me)
    {
        me.LookAt(Player.instance.transform.position);
        me.TryAttacking();
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Attack";
    }
}

public class IdleEffect : Effect
{
    public int rotationSpeed;

    public override void DoEffect(Unit me)
    {
        me.transform.rotation = Quaternion.Euler(me.transform.rotation.eulerAngles 
            + Vector3.forward * (Time.deltaTime * rotationSpeed));
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Idle (rotate)";
        node.AddTextField("Rotation speed", x =>
        {
            if (int.TryParse(x.newValue, out int newAmount))
                rotationSpeed = newAmount;
        }).value = rotationSpeed.ToString();
    }
}

public class RunAwayEffect : Effect
{
    public int speed;

    public override void DoEffect(Unit me)
    {
        me.MoveAwayFrom(Player.instance.transform.position, speed);
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Run away";
        node.AddTextField("Move speed", x =>
        {
            if (int.TryParse(x.newValue, out int newAmount))
                speed = newAmount;
        }).value = speed.ToString();
    }
}

