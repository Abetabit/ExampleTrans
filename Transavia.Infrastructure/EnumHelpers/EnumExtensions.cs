using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transavia.Core.EnumHelpers;

namespace Transavia.Infrastructure.EnumHelpers
{
    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Description = Enum.GetName(typeof(T), itemType),
                    Id = (int)itemType
                });
            }
            return values;
        }
    }
}
