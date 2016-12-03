namespace MapperTest
{
    class Source
    {
        public int FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public double ThirdProperty { get; set; }
        public short FourthProperty { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Source)) return false;
            Source that = (Source) obj;
            return FirstProperty == that.FirstProperty &&
                string.Equals(SecondProperty, that.SecondProperty) &&
                ThirdProperty.Equals(that.ThirdProperty) &&
                FourthProperty == that.FourthProperty;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FirstProperty.GetHashCode();
                hashCode = (hashCode * 397) ^ (SecondProperty != null ? SecondProperty.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ThirdProperty.GetHashCode();
                hashCode = (hashCode * 397) ^ FourthProperty.GetHashCode();
                return hashCode;
            }
        }
    }
}
