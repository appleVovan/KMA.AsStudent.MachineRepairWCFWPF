using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MacineRepairTool.Models;

namespace MachineRepair
{
    public enum OrderByDirection
    {
        None,
        Asc,
        Desc
    }

    public enum LogicOperator
    {
        And,
        Or
    }

    [DataContract]
    [Obfuscation(Exclude = true, StripAfterObfuscation = true)]
    public class EntitySelectQuery
    {
        [DataMember]
        private int _from = -1;

        [DataMember]
        private readonly WhereExpression _whereExpression;
        [DataMember]
        public int Top { get; set; }

        public int From
        {
            get { return _from; }
            set
            {
                if (value > -1)
                {
                    _from = value - 1;
                }
            }
        }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public string OrderBy { get; set; }
        [DataMember]
        public OrderByDirection OrderDirection { get; set; }

        public static IQueryable<TObject> GetQueryable<TObject>(TObject workingObject, EntitySelectQuery selectQuery,
            DbContext context)
            where TObject : class, IEntityObject, IEntityObject<TObject>
        {
            IQueryable<TObject> dbQuery = context.Set<TObject>();
            if (workingObject.HasAssociation)
            {
                dbQuery = workingObject.GetAssociaton(context.Set<TObject>());
            }
            var param = Expression.Parameter(typeof(TObject), "t");
            var exp = GetExpression<TObject>(selectQuery, param);
            if (exp != null)
            {
                dbQuery = dbQuery.Where(exp);
            }

            if (selectQuery.OrderDirection != OrderByDirection.None)
            {
                MemberExpression member;
                if (selectQuery.OrderBy.Contains("."))
                {
                    int pos = selectQuery.OrderBy.IndexOf(".");
                    member = Expression.Property(param, selectQuery.OrderBy.Substring(0, pos));
                    member = Expression.Property(member, selectQuery.OrderBy.Substring(pos + 1));
                }
                else
                {
                    member = Expression.Property(param, selectQuery.OrderBy);
                }
                //var pi = typeof(TObject).GetProperty(selectQuery.OrderBy);
                switch (selectQuery.OrderDirection)
                {
                    case OrderByDirection.Asc:
                        dbQuery = dbQuery.OrderBy(param, member);
                        break;
                    case OrderByDirection.Desc:
                        dbQuery = dbQuery.OrderByDescending(param, member);
                        break;
                }
            }
            if (selectQuery.From >= 0 && selectQuery.Count > 0)
            {
                return dbQuery.Skip(selectQuery.From).Take(selectQuery.Count);
            }
            if (selectQuery.Top > 0)
            {
                return dbQuery.Take(selectQuery.Top);
            }
            return dbQuery;
        }

        public EntitySelectQuery()
        {
            _whereExpression = new WhereExpression();
            OrderDirection = OrderByDirection.None;
        }

        public WhereExpression GetParentExpression()
        {
            return _whereExpression;
        }

        /// <summary>
        /// Creates new embeded expresion in parent expression
        /// </summary>
        /// <param name="whereExpression">Parent Expression</param>
        /// <returns>new expression</returns>
        public WhereExpression AddNewExpression(WhereExpression whereExpression)
        {
            var newExpression = new WhereExpression();
            whereExpression.Filters.Add(newExpression);
            return newExpression;
        }

        /// <summary>
        /// Creates new embeded filter in parent expression
        /// </summary>
        /// <param name="whereExpression">Parent Expression</param>
        /// <param name="propertyName">name of the property of the object</param>
        /// <param name="signSymbol"></param>
        /// <param name="value"></param>
        /// <returns>new filter</returns>
        public void AddNewFilter(WhereExpression whereExpression, string propertyName, Sign signSymbol, object value)
        {
            var filter = new Filter(propertyName, signSymbol, value);
            whereExpression.Filters.Add(filter);
        }

        /// <summary>
        /// Sets Logic operator wthin given where expression
        /// </summary>
        /// <param name="whereExpression">where Expression</param>
        /// <param name="logicOperator"></param>
        public void SetExpressionConnection(WhereExpression whereExpression, LogicOperator logicOperator)
        {
            whereExpression.LogicOperator = logicOperator;
        }

        private static Expression<Func<T, bool>> GetExpression<T>(EntitySelectQuery expressionBuilder, ParameterExpression param) where T : class, IEntityObject, IEntityObject<T>
        {
            var exp = GetExpression<T>(expressionBuilder._whereExpression, param);
            if (exp == null)
            {
                return null;
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(WhereExpression whereExpression, ParameterExpression param) where T : class, IEntityObject, IEntityObject<T>
        {
            if (whereExpression == null || whereExpression.Filters == null || whereExpression.Filters.Count == 0)
                return null;

            Expression exp;
            if (whereExpression.Filters[0] is Filter)
            {
                exp = GetExpression(param, whereExpression.Filters[0] as Filter);
            }
            else
            {
                exp = GetExpression<T>(whereExpression.Filters[0], param);
            }
            for (int i = 1; i < whereExpression.Filters.Count; i++)
            {
                var f1 = whereExpression.Filters[i];
                Expression tempExpression;
                if (f1 is Filter)
                {
                    tempExpression = GetExpression(param, f1 as Filter);
                }
                else
                {
                    tempExpression = GetExpression<T>(f1, param);
                }
                if (tempExpression != null)
                    switch (whereExpression.LogicOperator)
                    {
                        case LogicOperator.And:
                            exp = Expression.And(exp, tempExpression);
                            break;
                        case LogicOperator.Or:
                            exp = Expression.Or(exp, tempExpression);
                            break;
                    }
            }
            return exp;
        }

        private static Expression GetExpression(ParameterExpression param, Filter filter)
        {
            MemberExpression member;
            if (filter.PropertyName.Contains("."))
            {
                int pos = filter.PropertyName.IndexOf(".");
                member = Expression.Property(param, filter.PropertyName.Substring(0, pos));
                member = Expression.Property(member, filter.PropertyName.Substring(pos + 1));
            }
            else
            {
                member = Expression.Property(param, filter.PropertyName);
            }
            ConstantExpression constant = Expression.Constant(filter.Value);
            var Exp = Expression.Convert(constant, member.Type);
            switch (filter.SignSymbol)
            {
                case Sign.Equals:

                    return Expression.Equal(member, Exp);
                case Sign.GreaterThan:
                    return Expression.GreaterThan(member, Exp);
                case Sign.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, Exp);
                case Sign.LessThan:
                    return Expression.LessThan(member, Exp);
                case Sign.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, Exp);
                case Sign.NotEqual:
                    return Expression.NotEqual(member, Exp);
            }
            return null;
        }

    }

    public static class OrderHelper
    {
        public static IQueryable<TObject> OrderBy<TObject>(this IQueryable<TObject> query, ParameterExpression param, MemberExpression member)
        {
            if (member.Type == typeof(Int32))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, Int32>>(member, param));
            }
            if (member.Type == typeof(Int16))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, Int16>>(member, param));
            }
            if (member.Type == typeof(Int64))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, Int64>>(member, param));
            }
            if (member.Type == typeof(Guid))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, Guid>>(member, param));
            }
            if (member.Type == typeof(DateTime))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, DateTime>>(member, param));
            }
            if (member.Type == typeof(string))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, string>>(member, param));
            }
            if (member.Type == typeof(bool))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, bool>>(member, param));
            }
            if (member.Type == typeof(byte))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, byte>>(member, param));
            }
            if (member.Type == typeof(char))
            {
                return query.OrderBy(Expression.Lambda<Func<TObject, char>>(member, param));
            }
            return query;
        }

        public static IQueryable<TObject> OrderByDescending<TObject>(this IQueryable<TObject> query, ParameterExpression param, MemberExpression member)
        {
            if (member.Type == typeof(Int32))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, Int32>>(member, param));
            }
            if (member.Type == typeof(Int16))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, Int16>>(member, param));
            }
            if (member.Type == typeof(Int64))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, Int64>>(member, param));
            }
            if (member.Type == typeof(Guid))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, Guid>>(member, param));
            }
            if (member.Type == typeof(DateTime))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, DateTime>>(member, param));
            }
            if (member.Type == typeof(string))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, string>>(member, param));
            }
            if (member.Type == typeof(bool))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, bool>>(member, param));
            }
            if (member.Type == typeof(byte))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, byte>>(member, param));
            }
            if (member.Type == typeof(char))
            {
                return query.OrderByDescending(Expression.Lambda<Func<TObject, char>>(member, param));
            }
            return query;
        }
    }

    public enum Sign
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        NotEqual
    }

    [DataContract]
    [Obfuscation(Exclude = true, StripAfterObfuscation = true)]
    public class Filter : WhereExpression
    {
        [DataMember]
        internal string PropertyName { get; set; }
        [DataMember]
        internal Sign SignSymbol { get; set; }
        [DataMember]
        internal object Value { get; set; }

        internal Filter(string propertyName, Sign signSymbol, object value)
        {
            PropertyName = propertyName;
            SignSymbol = signSymbol;
            Value = value;
        }
        private Filter()
        {

        }
    }

    [DataContract]
    [KnownTypeAttribute(typeof(Filter))]
    [Obfuscation(Exclude = true, StripAfterObfuscation = true)]
    public class WhereExpression
    {
        [DataMember]
        internal List<WhereExpression> Filters;
        [DataMember]
        internal LogicOperator LogicOperator;

        internal WhereExpression()
        {
            Filters = new List<WhereExpression>();
            LogicOperator = LogicOperator.And;
        }
    }

}

