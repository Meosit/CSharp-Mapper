using System;

namespace Mapper
{
    public class MappingTypeAssociation
    {
        public MappingTypeAssociation(Type destination, Type source)
        {
            Destination = destination;
            Source = source;
        }

        public Type Source { get; }
        public Type Destination { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            MappingTypeAssociation other = (MappingTypeAssociation) obj;
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