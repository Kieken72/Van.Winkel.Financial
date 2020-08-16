using System.Collections.Generic;
using System.Text;

namespace Van.Winkel.Financial.Infrastructure.Configuration
{
    public interface IConfigurationHelper
    {
        string ConnectionString { get; set; }
    }
}
