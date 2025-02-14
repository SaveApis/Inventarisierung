using Backend.Domains.Common.Domain.Types;
using Backend.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Domain.Entity;

public class PermissionDescriptionEntity
{
    private PermissionDescriptionEntity(Id id, Description description, Locale locale, Id permissionId)
    {
        Id = id;
        Description = description;
        Locale = locale;
        PermissionId = permissionId;
    }

    public Id Id { get; }
    public Description Description { get; }
    public Locale Locale { get; }

    public Id PermissionId { get; }

    public static PermissionDescriptionEntity Create(Description description, Locale locale, Id permissionId)
    {
        return new PermissionDescriptionEntity(Id.From(Guid.NewGuid()), description, locale, permissionId);
    }
}
