namespace Modals.HomeModals;

public record Example(string Name, int Age);
public record Message(DateOnly Date, String User, List<String>? MessageBody);
public record AllLogs(List<Message> log_messages);
public record Uptime(DateTime StartTime, DateTime ServerTime, TimeSpan CurrentUptime);
