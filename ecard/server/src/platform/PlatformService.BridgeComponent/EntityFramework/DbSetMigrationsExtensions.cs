﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.EntityFramework
{
    public static class DbSetMigrationsExtensions
    {
        public static T AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }

        /// <summary>
        /// Adiciona uma entidade se ela não existe ainda
        /// Assinatura semelhante ao AddOrUpdate
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="set">Set onde serão adicionadas as entidades</param>
        /// <param name="identifierExpression">Campos usados na comparação</param>
        /// <param name="entities">Entidades para adicionar</param>
        public static void AddIfNotExists<TEntity>(this IDbSet<TEntity> set, Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities) where TEntity : class
        {

            var identifyingProperties = GetProperties<TEntity>(identifierExpression).ToList();
            var parameter = Expression.Parameter(typeof(TEntity));
            foreach (var entity in entities)
            {
                var matches = identifyingProperties.Select(pi => Expression.Equal(Expression.Property(parameter, pi.Name), Expression.Constant(pi.GetValue(entity, null))));
                var matchExpression = matches.Aggregate<BinaryExpression, Expression>(null, (agg, v) => (agg == null) ? v : Expression.AndAlso(agg, v));

                var predicate = Expression.Lambda<Func<TEntity, bool>>(matchExpression, new[] { parameter });
                if (!set.Any(predicate))
                {
                    set.Add(entity);
                }
            }
        }

        private static IEnumerable<PropertyInfo> GetProperties<T>(Expression<Func<T, object>> exp) where T : class
        {
            Debug.Assert(exp != null);
            Debug.Assert(exp.Body != null);
            Debug.Assert(exp.Parameters.Count == 1);

            var type = typeof(T);
            var properties = new List<PropertyInfo>();

            if (exp.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memExp = exp.Body as MemberExpression;
                if (memExp != null && memExp.Member != null)
                    properties.Add(type.GetProperty(memExp.Member.Name));
            }
            else if (exp.Body.NodeType == ExpressionType.Convert)
            {
                var unaryExp = exp.Body as UnaryExpression;
                if (unaryExp != null)
                {
                    var propExp = unaryExp.Operand as MemberExpression;
                    if (propExp != null && propExp.Member != null)
                        properties.Add(type.GetProperty(propExp.Member.Name));
                }
            }
            else if (exp.Body.NodeType == ExpressionType.New)
            {
                var newExp = exp.Body as NewExpression;
                if (newExp != null)
                    properties.AddRange(newExp.Members.Select(x => type.GetProperty(x.Name)));
            }

            return properties.OfType<PropertyInfo>();
        }

        /// <summary>
        /// Faz um set.Any(predicate)
        /// Se não existe nada no set então adiciona
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set">Set onde será adicionada a entidade</param>
        /// <param name="predicate">Condição (exemplo: dbUser => dbUser.Nome == "Gustavo")</param>
        /// <param name="entity">Entidade para adicionar</param>
        /// <returns></returns>
        public static T AddIfNotExists<T>(this IDbSet<T> set, Expression<Func<T, bool>> predicate, T entity) where T : class, new()
        {
            return !set.Any(predicate) ? set.Add(entity) : null;
        }
    }
}
