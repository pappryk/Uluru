using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.Positions
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<IEnumerable<Position>> GetAllOfGroupAsync(int groupId);
    }
}
