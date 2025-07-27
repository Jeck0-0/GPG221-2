using UnityEngine;

public class StartNode : GoapNode
{
    protected override string outputPortName => "First Action";
    protected override Color portColor => new Color(.6f, .1f, .9f, 1);
    
    public StartNode()
    {
        title = "Start Node";

        data.type = GoapNodeType.Start;
        
        Color color = new Color(.54f, .19f, .9f, 1);
        style.borderBottomColor = color;
        style.borderTopColor = color;
        style.borderLeftColor = color;
        style.borderRightColor = color;
    }
}