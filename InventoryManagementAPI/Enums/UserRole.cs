using System.ComponentModel;

namespace InventoryManagementAPI.Enums
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin,

        [Description("Manager")]
        Manager,

        [Description("Clerk")]
        Clerk
    }
}
