using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MeshRenderer))]
public class NodeVisual : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMesh fCost, gCost, hCost;

    private Node node;
    private PathState state;

    private Material material;

    private Color color = Color.white; 

    private bool isMouseOver;

    public void Initialize(Node node)
    {
        this.node = node;

        this.state = node.State as PathState;

        material = GetComponent<MeshRenderer>().material;

        UpdateVisual();
    }
    
    public void UpdateVisual()
    {
        SetColor(Tiles.GetColor(state.TerrainType));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    private void Update()
    {
        if (isMouseOver)
        {
            Messenger.CheckNode(node);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Messenger.SelectedTarget(node);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            state.TerrainType = (TerrainType)(((int)(state).TerrainType + 1) % (System.Enum.GetNames(typeof(TerrainType)).Length));

            state.Walkable = Tiles.IsWalkable(state.TerrainType);

            SetColor(Tiles.GetColor(state.TerrainType));
        }
    }

    public void ScaleDown()
    {
        transform.localScale = Vector3.one * 0.7f;
    }

    public void SetColor(Color newColor)
    {
        color = newColor;

        SetTint(color);
    }

    public void SetTint(Color newColor)
    {
        material.color = newColor;
    }

    public void Reset()
    {
        transform.localScale = Vector3.one;

        SetTint(color);
    }

    public void EnableCostVisuals(bool enable)
    {
        fCost.GetComponent<Renderer>().enabled = enable;
        gCost.GetComponent<Renderer>().enabled = enable;
        hCost.GetComponent<Renderer>().enabled = enable;

        fCost.text = node.FCost.ToString();
        gCost.text = node.GCost.ToString();
        hCost.text = node.HCost.ToString();
    }
}
