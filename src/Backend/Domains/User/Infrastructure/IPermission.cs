using Backend.Domains.Common.Domain.Types;
using Backend.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Infrastructure;

public interface IPermission
{
    Key Key { get; }
    IReadOnlyCollection<Tuple<Locale, Name>> LocalizedNames { get; }
    IReadOnlyCollection<Tuple<Locale, Description>> LocalizedDescriptions { get; }
}
