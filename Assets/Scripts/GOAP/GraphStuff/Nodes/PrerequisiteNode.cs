using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PrerequisiteNode : GoapNode
{
    protected override string outputPortName => "Fallback Action";
    protected override Color portColor => new Color(1, .8f, 0, 1);
    
    public Prerequisite prerequisite;

    public PrerequisiteNode()
    {
        data.type = GoapNodeType.Prerequisite;
        
        CreateAddPortButtons();
        AddInputPort();
        
        Color color = new Color(1, .6f, 0, 1);
        style.borderBottomColor = color;
        style.borderTopColor = color;
        style.borderLeftColor = color;
        style.borderRightColor = color;
        
        RefreshExpandedState();
    }

    public override GoapNodeData SaveState()
    {
        data.prerequisite = prerequisite;
        
        foreach (var portElement in outputContainer.Children())
        {
            if (portElement is not Port port) continue;
            var connected = port.connections.FirstOrDefault();
            if (connected?.input.node is not ActionNode node) continue;
            
            prerequisite.fallbackActions.Add(node.action);
        }
        
        return base.SaveState();
    }
}
