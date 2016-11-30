using System.Reflection;

namespace Mapper
{
    internal sealed class MappingProperty
    {
        public MappingProperty(PropertyInfo source, PropertyInfo destination)
        {
            Destination = destination;
            Source = source;
        }

        internal PropertyInfo Source { get; }
        internal PropertyInfo Destination { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            MappingProperty other = (MappingProperty) obj;
            return Source == other.Source &&
                Destination == other.Destination;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) 
                    ^ (Destination != null ? Destination.GetHashCode() : 0);
            }
        }
    }
}