namespace Infrastructure.Data;

public record InfrastructureOptions
{
    public string DataDirectory { get; set; } = null!;
}