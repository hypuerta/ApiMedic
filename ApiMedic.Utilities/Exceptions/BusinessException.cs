// -------------------------------------------------------------------------------
// <copyright file="BusinessException.cs" company="ApiMedic.Api">
// ApiMedic.Api
// </copyright>
// <author>Herley Puerta</author>
// <email>hypuerta@hotmail.com</email>
// <date>13/02/2018</date>
// <summary>Base Class BusinessException.</summary>
// -------------------------------------------------------------------------------
namespace ApiMedic.Utilities.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base Class BusinessException.
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BusinessException class.
        /// </summary>
        public BusinessException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessException class.
        /// </summary>
        /// <param name="message">Message exception.</param>
        public BusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessException class.
        /// </summary>
        /// <param name="message">Message exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessException class.
        /// </summary>
        /// <param name="info">Info exception.</param>
        /// <param name="context">Context exception.</param>
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}