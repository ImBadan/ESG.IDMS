using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ESG.Common.Web.Utility.Authorization;

/// <summary>
/// The custom <see cref="AuthorizationPolicy"/> provider.
/// </summary>
/// <remarks>
/// Initializes an instance of <see cref="PermissionPolicyProvider"/>.
/// </remarks>
/// <param name="options"></param>
public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
{
    /// <summary>
    /// The default policy provider.
    /// </summary>
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; } = new DefaultAuthorizationPolicyProvider(options);

    /// <summary>
    /// Gets the default policy.
    /// </summary>
    /// <returns></returns>
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    /// <summary>
    /// Gets <see cref="AuthorizationPolicy"/> for the given <paramref name="policyName"/>.
    /// If <paramref name="policyName"/> starts with Permission, creates a policy with a
    /// <see cref="PermissionRequirement"/>. The policy name must match the permission
    /// that is needed.
    /// </summary>
    /// <param name="policyName">The name of the policy</param>
    /// <returns></returns>
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build())!;
        }

        // Policy is not for permissions, try the default provider.
        return FallbackPolicyProvider.GetPolicyAsync(policyName)!;
    }

    /// <summary>
    /// Gets the fallback authorization policy.
    /// </summary>
    /// <returns></returns>
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync()!;
}