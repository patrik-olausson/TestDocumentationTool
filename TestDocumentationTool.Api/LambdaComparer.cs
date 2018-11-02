using System;
using System.Collections.Generic;

namespace TestDocumentationTool.Api
{
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _lambdaComparer;
        private readonly Func<T, int> _lambdaHash;

        public LambdaComparer(Func<T, T, bool> lambdaComparer) :
            this(lambdaComparer, o => 0)
        {

        }

        public static LambdaComparer<T> CreateForProperty<TProperty>(Func<T, TProperty> propertyValueGetter)
        {
            return new LambdaComparer<T>((x, y) => propertyValueGetter(x).Equals(propertyValueGetter(y)));
        }

        public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
        {
            if (lambdaComparer == null) throw new ArgumentNullException(nameof(lambdaComparer));
            if (lambdaHash == null) throw new ArgumentNullException(nameof(lambdaHash));

            _lambdaComparer = lambdaComparer;
            _lambdaHash = lambdaHash;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return _lambdaComparer(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return _lambdaHash(obj);
        }
    }
}