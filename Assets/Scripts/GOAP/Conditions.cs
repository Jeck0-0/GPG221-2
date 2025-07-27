using UnityEngine;

public class PlayerWithinRangeCondition : Condition
{
    public float range;
    public override bool CheckCondition(Unit me)
    {
        return Vector3.Distance(Player.instance.transform.position, me.transform.position) <= range;
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Player within range";
        node.AddTextField("Range", x=> {
            if (int.TryParse(x.newValue, out int newAmount))
                range = newAmount;
        }).value = range.ToString();
    }
}

public class PlayerWithinWeaponRangeCondition : Condition
{
    public override bool CheckCondition(Unit me)
    {
        return Vector3.Distance(Player.instance.transform.position, me.transform.position) <= me.weapon.attackDistance;
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Player within weapon range";
    }
}


public class HasWeaponCondition : Condition
{
    public override bool CheckCondition(Unit me)
    {
        return me.weapon;
    }

    public override void SetupNode(GoapNode node)
    {
        node.title = "Has weapon";
    }
}