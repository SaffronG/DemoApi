namespace Records.ItemRecords;

public record Item(Guid Id, string Name) {
    public Item() : this(Guid.NewGuid(), "TEST") { }
    public Item(string name) : this(Guid.NewGuid(), name) { }
}
