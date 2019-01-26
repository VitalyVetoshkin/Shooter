public struct PlayerData
{
    public string Name;
    public float HP;
    public bool IsVisible;

    public override string ToString() => $"Name: {Name} HP: {HP} IsVisible: {IsVisible}";
}
