using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Node
{
    [SerializeField] private int xPosition;
    [SerializeField] private int yPosition;

    public IState State { get; private set; }
        
    public Node(IState state)
    {
        this.State = state;
    }
    
    public Node Parent { get; set; }
        
    public float GCost { get; set; }
    public float HCost { get; set; }

    public float FCost { get { return GCost + HCost; } }

    public void GetSuccessors(ref List<Node> successors)
    {
        successors.AddRange(State.GetSuccessors());
    }
}
