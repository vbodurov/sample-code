using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MiscCodeTests.Extensions
{
    public static class FieldInfoExtensions
    {
        public static Action<object, object> CreateSetter(this FieldInfo fi)
        {
            if (fi.IsInitOnly)
            {
                return null;
            }

            var instance = Expression.Parameter(typeof(object), "i");
            var value = Expression.Parameter(typeof(object));
            var convertedParam = Expression.Convert(instance, fi.DeclaringType);
            var fieldExp = Expression.Field(convertedParam, fi.Name);
            var assignExp = Expression.Assign(fieldExp, Expression.Convert(value, fi.FieldType));
            return Expression.Lambda<Action<object, object>>(assignExp, instance, value).Compile();
        }
        public static Func<object, object> CreateGetter(this FieldInfo fi)
        {
            var instance = Expression.Parameter(typeof(object), "i");
            var convertP = Expression.TypeAs(instance, fi.DeclaringType);
            var field = Expression.Field(convertP, fi);
            var convert = Expression.TypeAs(field, typeof(object));
            return (Func<object, object>)Expression.Lambda(convert, instance).Compile();

        }
    }
}