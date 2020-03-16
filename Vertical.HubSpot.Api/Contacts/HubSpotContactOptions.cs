namespace Vertical.HubSpot.Api.Contacts
{
    public class HubSpotContactOptions
    {
        public const bool IgnorePropertiesWithNullValuesDefault = false;

        public bool IgnorePropertiesWithNullValues { get; set; } = IgnorePropertiesWithNullValuesDefault;
    }
}