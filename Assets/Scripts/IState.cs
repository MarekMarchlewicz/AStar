using System.Collections.Generic;

public interface IState
{
    void Initialize(Node node);
    float GetEstimate(IState toState);
    List<Node> GetSuccessors();
    Node GetNode();
}
