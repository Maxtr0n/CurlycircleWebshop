using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class EntityCreatedViewModel
    {
        public int Id { get; set; }

        public EntityCreatedViewModel()
        {
        }

        public EntityCreatedViewModel(int id)
        {
            Id = id;
        }
    }
}
