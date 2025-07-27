using UnityEngine;

public class Player : Unit
{
    public static Player instance;

    protected override void ManagedInitialize()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        base.ManagedInitialize();
    }


    //I don't destroy the Player script to prevent some bugs from happening
    public override void Die()
    {
        Debug.Log("DEAD");
        gameObject.SetActive(false);
    }
}
