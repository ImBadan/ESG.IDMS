using ESG.IDMS.Web.Binders;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Attributes
{
    public class DataTablesRequestAttribute : ModelBinderAttribute
    {
        //
        // Summary:
        //     Initialize a new instance of DataTables.AspNetCore.Mvc.Binder.DataTablesRequestAttribute
        public DataTablesRequestAttribute()
            : base(typeof(DataTablesRequestModelBinder))
        {
        }
    }
}
