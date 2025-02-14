using Backend.Domains.Common.Domain.Types;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Domain.Entity;

public class PermissionNameEntity
{
    private PermissionNameEntity(Id id, Name name, Locale locale, Id permissionId)
    {
        Id = id;
        Name = name;
        Locale = locale;
        PermissionId = permissionId;
    }

    public Id Id { get; }
    public Name Name { get; }
    public Locale Locale { get; }

    public Id PermissionId { get; }

    public static PermissionNameEntity Create(Name name, Locale locale, Id permissionId)
    {
        return new PermissionNameEntity(Id.From(Guid.NewGuid()), name, locale, permissionId);
    }
}
