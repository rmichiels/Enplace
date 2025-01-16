using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Enplace.Service
{
    public static class AuthoriationOptionsExtensions
    {
        public static AuthorizationOptions ApplyPolicyDefinitions(this AuthorizationOptions options, IConfiguration configuration)
        {
            var section = configuration.GetSection("PolicyDefinitions");
            if (section.Exists())
            {
                var policyDefinitions = section.Get<List<PolicyDefinition>>() ?? [];
                foreach (var policy in policyDefinitions)
                {
                    Console.WriteLine($"Applying Policy: {policy.Label}");
                    options.AddPolicy(policy.Label, p => policy.ClaimDefinitions.ForEach(claim => p.RequireClaim(claim.Type, claim.Values)));
                }
            }
            else
            {
                Console.WriteLine("Tried to apply Policy Definitions, however, the section 'PolicyDefinitions' did not exist in the configuration file.");
            }
            return options;
        }
    }

    public class PolicyDefinition
    {
        public required string Label { get; set; }
        public string? Description { get; set; } = string.Empty;
        public required List<ClaimDefinition> ClaimDefinitions { get; set; }
    }

    public class ClaimDefinition
    {
        public required string Type { get; set; }
        public required List<string> Values { get; set; }
    }
}
