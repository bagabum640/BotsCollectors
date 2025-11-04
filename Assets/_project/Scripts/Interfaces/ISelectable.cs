public interface ISelectable
{
    public SelectionIndicator selectionIndicator { get; }
    public bool IsSelected { get; set; }

    public void Select();
    public void Deselect();
}