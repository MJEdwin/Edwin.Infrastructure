using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Edwin.Infrastructure.Query
{
    public class BaseTypeQueryFilter<TEntity> : QueryFilter<TEntity>
    {
        public BaseTypeQueryFilter(string propName, Operator @operator, object value)
        {
            if (!value.GetType().IsValueType && !(value.GetType() == typeof(string)))
                throw new ArgumentException(nameof(value));
            Body = Expression.MakeBinary(GetExpressionType(@operator), GetProp(propName), Expression.Constant(value));
        }

        public BaseTypeQueryFilter(Expression<Func<TEntity, object>> prop, Operator @operator, object value)
        {
            if (!value.GetType().IsValueType && !(value.GetType() == typeof(string)))
                throw new ArgumentException(nameof(value));
            Body = Expression.MakeBinary(GetExpressionType(@operator), GetProp(prop), Expression.Constant(value));
        }

        private ExpressionType GetExpressionType(Operator @operator)
        {
            ExpressionType type = ExpressionType.Default;
            switch (@operator)
            {
                case Operator.NotEqual:
                    type = ExpressionType.NotEqual;
                    break;
                case Operator.Equal:
                    type = ExpressionType.Equal;
                    break;
                case Operator.LessThan:
                    type = ExpressionType.LessThan;
                    break;
                case Operator.LessThanEqual:
                    type = ExpressionType.LessThanOrEqual;
                    break;
                case Operator.MoreThan:
                    type = ExpressionType.GreaterThan;
                    break;
                case Operator.MoreThanEqual:
                    type = ExpressionType.GreaterThanOrEqual;
                    break;
                default:
                    break;
            }
            return type;
        }

        private Expression GetProp(string propName)
        {
            var splitPath = propName.Split('.');
            Expression prop = Argument;
            foreach (var path in splitPath)
            {
                prop = Expression.PropertyOrField(prop, path);
            }
            return prop;
        }

        private Expression GetProp(Expression<Func<TEntity, object>> prop)
        {
            return prop.Body;
        }
    }

    public enum Operator
    {
        NotEqual = 0x00,
        Equal = 0x01,
        LessThan = 0x02,
        LessThanEqual = 0x03,
        MoreThan = 0x04,
        MoreThanEqual = 0x05,
    }
}
