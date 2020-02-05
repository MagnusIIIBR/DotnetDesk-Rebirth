using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Mvc.Enum
{
	public enum CustomerType
    {
        Other = 0,
        [Display(Name = "Small Business")]
        SmallBusiness = 1,
        Enterprise = 2,
        Government = 3,
        NGO = 4,
        Internal = 5
    }
}
