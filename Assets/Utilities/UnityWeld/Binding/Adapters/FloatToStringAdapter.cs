namespace UnityWeld.Binding.Adapters
{
    /// <summary>
    /// Adapter that converts a float to a string.
    /// </summary>
    [Adapter(typeof(float), typeof(string), typeof(FloatToStringAdapterOptions))]
    public class FloatToStringAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            if(options != null)
            {
                var format = ((FloatToStringAdapterOptions)options).Format;
                return ((float)valueIn).ToString(format);
            }
            return valueIn.ToString();
        }
    }
}
