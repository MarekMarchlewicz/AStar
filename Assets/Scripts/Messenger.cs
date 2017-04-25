using UnityEngine;

public class Messenger : MonoBehaviour 
{
    /// <summary>
    /// Target node was selected
    /// </summary>
    public static event System.Action<Node> OnSelectedTarget;

    public static void SelectedTarget(Node node)
    {
        if (OnSelectedTarget != null)
        {
            OnSelectedTarget(node);
        }
    }

    /// <summary>
    /// Event triggered when you hover above the node
    /// </summary>
    public static event System.Action<Node> OnCheckNode;

    public static void CheckNode(Node node)
    {
        if (OnCheckNode != null)
        {
            OnCheckNode(node);
        }
    }
}
