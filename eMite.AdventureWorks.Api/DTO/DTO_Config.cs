using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cfg = eMite.Framework.Bcl.Configurations;
using eMite.Framework.Bcl.Configurations.ATTRIBUTE;

namespace eMite.AdventureWorks.Api.DTO
{
    public class DTO_Config : cfg.BASE.DTO.DTO_ConfigurationBase
    {

        public string AdventureWorksConnection = @"Server=MSAWIN10;Database=AdventureWorks2012;Trusted_Connection=True;";
        public int QueryInterval = 1440;

        public DTO_Config()
        {

        }
    }
}
