using UnityEngine;

public class EnemyAI : Unit
{
    public GoapDataScriptableObject defaultBehaviour;

    private GOAP goap;
    
    protected override void ManagedInitialize()
    {
        goap = GetComponent<GOAP>();
        goap.SetBehaviour(defaultBehaviour);
        base.ManagedInitialize();
    }
    



}
