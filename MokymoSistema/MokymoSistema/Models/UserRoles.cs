namespace MokymoSistema.Models;

public static class UserRoles
{
    public const string Admin = nameof(Admin);
    public const string Lecturer = nameof(Lecturer);

    public static readonly IReadOnlyCollection<string> All = new[] { Admin, Lecturer };
}