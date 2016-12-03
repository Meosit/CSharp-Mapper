using System;

namespace MapperTest
{
    class Destination
    {
        public long FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public float ThirdProperty { get; set; }
        public DateTime FourthProperty { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Destination)) return false;
            Destination that = (Destination)obj;
            return FirstProperty == that.FirstProperty &&
                string.Equals(SecondProperty, that.SecondProperty) &&
                ThirdProperty.Equals(that.ThirdProperty) &&
                FourthProperty.Equals(that.FourthProperty);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FirstProperty.GetHashCode();
                hashCode = (hashCode * 397) ^ (SecondProperty != null ? SecondProperty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ThirdProperty.GetHashCode();
                hashCode = (hashCode * 397) ^ (FourthProperty != null ? FourthProperty.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
