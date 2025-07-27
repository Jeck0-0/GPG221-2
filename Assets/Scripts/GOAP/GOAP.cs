using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Unit))]
public class GOAP : MonoBehaviour
{
    public GoapAction currentRootAction;
    Coroutine behaviourLoop;
    
    private Unit me;

    private void Awake()
    {
        me = GetComponent<Unit>();
    }

    public void SetBehaviour(GoapDataScriptableObject behaviourData)
    {
        if(behaviourLoop != null)
            StopCoroutine(behaviourLoop);
        
        currentRootAction = behaviourData.GetRootAction();
    }

    void Update()
    {
        currentRootAction?.TryEffect(me);
    }

    /*IEnumerator BehaviourLoop()
    {
        while (true)
        {
            var result = currentRootAction.GetEffect();
            if (result.success == true)
            {
                yield return result.nextEffect.DoEffect();
                yield break;
            }

            while (result.nextEffect == null)
            {
                yield return null;
                continue;
            }
            
            yield return result.nextEffect.DoEffect();
        }
    }*/
    
}