using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MacineRepairTool.Models
{
    [Serializable]
    [DataContract]
    public class EList<TObject>:List<TObject>, IEntityObject where TObject : class, IEntityObject<TObject>
    {
        public ObjectType Type => ObjectType.User;
    }
}
