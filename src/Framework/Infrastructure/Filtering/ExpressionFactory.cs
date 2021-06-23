using LinqKit;
using Ngx.Monorepo.Framework.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Ngx.Monorepo.Framework.Infrastructure.Filtering
{
    public class ExpressionFactory
    {
        public static Expression<Func<T, bool>> BuildExpression<T>(IEnumerable<FilterRule> filterRule)
        {
            var predicate = PredicateBuilder.New<T>();

            //Loop through each filter rule in the group.
            foreach (var rule in filterRule)
            {
                //Retrieve The Expression Behavior
                var operatorInfo = rule.Operator;
                //Retrieve the property from the Type T (i.e. UnifiedView, CspView, etc)
                var propertyInfo = typeof(T).GetProperty(rule.Field);

                //Creates the left side of the expression (i.e. duedate => duedate.propertyInfo)
                var leftParamExpression = Expression.Parameter(typeof(T));
                Expression leftExpression = Expression.Property(leftParamExpression, propertyInfo);

                Expression rightExpression = Expression.Constant(TypeDescriptor.GetConverter(propertyInfo.PropertyType).ConvertFromInvariantString(rule.Value));

                //Check if the field we are filtering on is nullable.  If it is nullable we need to create an inner predicate that will null check the property and cast to its non nullable type.
                if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null)
                {
                    //Create expression that will check if the newly created left expression (i.e dueDate => duedate.propertyInfo) is not null. (dueDate => duedate.propertyInfo != null)
                    var newExpression = Expression.MakeBinary(ExpressionType.NotEqual, leftExpression, Expression.Constant(null, propertyInfo.PropertyType));
                    predicate = predicate.And(Expression.Lambda<Func<T, bool>>(newExpression, leftParamExpression));
                    leftExpression = Expression.Convert(leftExpression, Nullable.GetUnderlyingType(propertyInfo.PropertyType));
                }

                //Checks if the operator uses a method (Contains, StartsWith, etc, instead of == >= etc)
                if (operatorInfo.UseMethod)
                {
                    //If it is a method alters left expression to use the method. (i.e. duedate => duedate.METHOD({rightExpression}))
                    leftExpression = Expression.Call(leftExpression, operatorInfo.Method, null, rightExpression);
                }

                //Adds the operator to the expression if it is binary (i.e. duedate => duedate {OPERATOR} {right expression})
                //If the expression behavior uses a mehod it will take the operator and apply it the result of the method call using the MethodReturnCompareBool as the result (i.e. {leftparamExpression} => {leftParamExpression}.{Method}({RIGHT EXPRESSION}) {ExpressionType} {MethodReturnCompareBool}, or clientCode => clientCode.contains('str') == false)   
                Expression func = Expression.MakeBinary(operatorInfo.ExpressionType, leftExpression, operatorInfo.IsBinary ? rightExpression : Expression.Constant(operatorInfo.MethodReturnCompareBool));
                var expressionLambda = Expression.Lambda<Func<T, bool>>(func, leftParamExpression);

                //Take the newly build lambda and AND it to the inner expression (i.e. duedate => duedate != null AND duedate >= '1/1/1900')
                predicate = rule.Logic == ExpressionType.And ? predicate.And(expressionLambda) : predicate.Or(expressionLambda);
            }
            return predicate;
        }
    }
}
