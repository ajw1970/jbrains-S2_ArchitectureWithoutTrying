namespace Tests
{
    public class Barcode
    {
        public Barcode(string value)
        {
            Value = value;
        }

        protected bool Equals(Barcode other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Barcode) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Barcode left, Barcode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Barcode left, Barcode right)
        {
            return !Equals(left, right);
        }

        public string Value { get; }
    }
}