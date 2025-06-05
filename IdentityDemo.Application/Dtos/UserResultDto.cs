namespace IdentityDemo.Application.Dtos;

// Representerar resultatet av en operation som t.ex. skapa en användare, logga in etc.
public record UserResultDto(string? ErrorMessage)
{
    public bool Succeeded => ErrorMessage == null;
}
