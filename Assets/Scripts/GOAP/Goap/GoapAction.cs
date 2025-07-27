using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class GoapAction
{
    [SerializeReference, ShowInInspector] private List<Prerequisite> prerequisites;
    public List<Prerequisite> Prerequisites
    {
        get => prerequisites;
        protected set => prerequisites = value;
    }
    
    [SerializeReference, ShowInInspector] private Effect effect;
    public Effect Effect 
    {
        get => effect; 
        protected set => effect = value;
    }
    
    public GoapAction() { Prerequisites = new List<Prerequisite>(); }
    public GoapAction(Effect effect) : this() { this.Effect = effect; }

    
    public (bool playedThisAction, bool playedAnyAction) TryEffect(Unit me)
    {
        bool hasPrerequisites = true;
        foreach (var prerequisite in Prerequisites)
        {
            var result = prerequisite.CheckPrerequisite(me);
            
            if (result.didAction)
                return (false, true);
            
            if (!result.met)
                hasPrerequisites = false;
        }
        
        if (!hasPrerequisites) 
            return (false, false);
        
        effect.DoEffect(me);
        return (true, true);
    }

    
    public void SetupNode(GoapNode node)
    {
        Effect.SetupNode(node);
    }
}

[Serializable]
public class Prerequisite
{
    [SerializeReference]
    public Condition condition;
    
    [SerializeReference]
    public List<GoapAction> fallbackActions;
    
    
    public Prerequisite() { fallbackActions = new List<GoapAction>(); }
    public Prerequisite(Condition condition) : this() { this.condition = condition; }


    public (bool met, bool didAction) CheckPrerequisite(Unit me)
    {
        if (!condition.CheckCondition(me))
        {
            foreach (var b in fallbackActions)
            {
                var result = b.TryEffect(me);
                if (result.playedAnyAction)
                    return (false, true);
            }
            return (false, false);
        }
        return (true, false);
    }

    public void SetupNode(GoapNode node)
    {
        condition.SetupNode(node);
    }
}

[Serializable]
public abstract class Effect
{
    public abstract void DoEffect(Unit me);
    public abstract void SetupNode(GoapNode node);
}

[Serializable]
public abstract class Condition
{
    public virtual bool CheckCondition(Unit me)
    {
        return false;
    }
    public abstract void SetupNode(GoapNode node);
}
