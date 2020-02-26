using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        //public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
        //    where TAttribute : Attribute
        //{
        //    return enumValue.GetType()
        //        .GetMember(enumValue.ToString())
        //        .First()
        //        .GetCustomAttribute<TAttribute>();
        //}
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }
    }
}
