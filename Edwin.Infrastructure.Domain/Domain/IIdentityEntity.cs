using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Edwin.Infrastructure.Domain.Domain
{
    public interface IIdentityEntity : IEntity<int>
    {
    }
}
