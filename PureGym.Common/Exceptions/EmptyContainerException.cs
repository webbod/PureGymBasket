using System;

namespace PureGym.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a container is unexpectedly empty
    /// </summary>
    public class EmptyContainerException : InvalidOperationException
    {
        public EmptyContainerException(string message) : base(message)
        {

        }
    }
}
