using GraphQL.Next.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Next
{
    public interface IReferenceReplacer
    {
        void Replace(object target, object type);
    }

    public interface IReferenceReplacer<T, K> : IReferenceReplacer
    {
        void Replace(T target, K type);
    }

    public class ReferenceReplacer<T, K> : IReferenceReplacer<T, K>
    {
        private readonly Action<T, K> _func;

        public ReferenceReplacer(Action<T, K> func)
        {
            _func = func;
        }

        public void Replace(T target, K type)
        {
            _func(target, type);
        }

        void IReferenceReplacer.Replace(object target, object type)
        {
            Replace((T)target, (K)type);
        }
    }

    public class ReferenceTarget
    {
        public GraphQLTypeReference Ref { get; set; }
        public object Target { get; set; }
        public IReferenceReplacer Replacer { get; set; }
    }
}
