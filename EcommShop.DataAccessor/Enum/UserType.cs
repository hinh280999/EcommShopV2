using System.ComponentModel;

namespace EcommShop.DataAccessor.Entities
{
    public enum UserType
    {
        [Description("Admin")]
        ADMIN = 1,
        [Description("Normal User")]
        USER = 0
    }
}
