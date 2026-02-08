using Microsoft.Extensions.Primitives;

namespace EFCore.API.Tenants;

public interface ITenantService
{
    string? GetTenantId();
}

public class TenantService : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TenantService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private string? _tenantId = null;
    
    public string? GetTenantId()
    {
        if (_tenantId is not null)
            return _tenantId;

        KeyValuePair<string, StringValues>? header = _httpContextAccessor.HttpContext?.Request.Headers
            .FirstOrDefault(header => header.Key.Equals("X-Tenant", StringComparison.InvariantCultureIgnoreCase));

        if (header is null) return _tenantId;

        if (header.Value.Value.Count != 0)
        {
            _tenantId = header.Value.Value.First();
        }
        return _tenantId;
    }
}