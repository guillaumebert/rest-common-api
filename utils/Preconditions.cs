
namespace Neotys.CommonAPI.Utils
{
    public class Preconditions
    {
        /// <summary>
        ///  Throw an exception if the argument is null.
        /// </summary>
        public static T CheckNotNull<T>(T value)
        {
            if (value == null)
            {
                throw new System.ArgumentNullException("Parameter cannot be null");
            }
            return value;
        }
    }
}
