namespace Records.ItemRecords;

public record Item(Guid id, string name) {
    public Item() : this(Guid.NewGuid(), "TEST") { }
    public Item(string name) : this(Guid.NewGuid(), name) { }
}
