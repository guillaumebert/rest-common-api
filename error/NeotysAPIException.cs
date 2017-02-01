using System;
using System.Collections.Generic;
using System.Text;
using Neotys.CommonAPI.Utils;

/*
 * Copyright (c) 2016, Neotys
 * All rights reserved.
 */
namespace Neotys.CommonAPI.Error
{

    /// <summary>
    /// Exception that can occur while interacting with Neotys API server.
    /// 
    /// @author srichert
    /// 
    /// </summary>
    public class NeotysAPIException : Exception
    {
        public sealed class ErrorType
        {

            // APIs
            public static readonly ErrorType NL_API_ERROR = new ErrorType("NL_API_ERROR", InnerEnum.NL_API_ERROR);
            public static readonly ErrorType NL_API_KEY_NOT_ALLOWED = new ErrorType("NL_API_KEY_NOT_ALLOWED", InnerEnum.NL_API_KEY_NOT_ALLOWED);
            public static readonly ErrorType NL_API_ILLEGAL_SESSION = new ErrorType("NL_API_ILLEGAL_SESSION", InnerEnum.NL_API_ILLEGAL_SESSION);
            public static readonly ErrorType NL_API_INVALID_ARGUMENT = new ErrorType("NL_API_INVALID_ARGUMENT", InnerEnum.NL_API_INVALID_ARGUMENT);

            public static readonly ErrorType NL_RECORDING_NOT_LICENSED = new ErrorType("NL_RECORDING_NOT_LICENSED", InnerEnum.NL_RECORDING_NOT_LICENSED);
            // DESIGN
            public static readonly ErrorType NL_DESIGN_ILLEGAL_STATE_FOR_OPERATION = new ErrorType("NL_DESIGN_ILLEGAL_STATE_FOR_OPERATION", InnerEnum.NL_DESIGN_ILLEGAL_STATE_FOR_OPERATION);
            public static readonly ErrorType NL_DESIGN_CANNOT_GET_RECORDER_SETTINGS = new ErrorType("NL_DESIGN_CANNOT_GET_RECORDER_SETTINGS", InnerEnum.NL_DESIGN_CANNOT_GET_RECORDER_SETTINGS);
            public static readonly ErrorType NL_DESIGN_CANNOT_GET_RECORDING_STATUS = new ErrorType("NL_DESIGN_CANNOT_GET_RECORDING_STATUS", InnerEnum.NL_DESIGN_CANNOT_GET_RECORDING_STATUS);
            public static readonly ErrorType NL_DESIGN_UNKNOWN_USER_PATH = new ErrorType("NL_DESIGN_UNKNOWN_USER_PATH", InnerEnum.NL_DESIGN_UNKNOWN_USER_PATH);
            public static readonly ErrorType NL_DESIGN_CANNOT_SAVE_PROJECT = new ErrorType("NL_DESIGN_CANNOT_SAVE_PROJECT", InnerEnum.NL_DESIGN_CANNOT_SAVE_PROJECT);
            public static readonly ErrorType NL_DESIGN_CANNOT_SAVE_AS_PROJECT = new ErrorType("NL_DESIGN_CANNOT_SAVE_AS_PROJECT", InnerEnum.NL_DESIGN_CANNOT_SAVE_AS_PROJECT);
            public static readonly ErrorType NL_DESIGN_CANNOT_CLOSE_PROJECT = new ErrorType("NL_DESIGN_CANNOT_CLOSE_PROJECT", InnerEnum.NL_DESIGN_CANNOT_CLOSE_PROJECT);
            public static readonly ErrorType NL_DESIGN_CANNOT_OPEN_PROJECT = new ErrorType("NL_DESIGN_CANNOT_OPEN_PROJECT", InnerEnum.NL_DESIGN_CANNOT_OPEN_PROJECT);
            public static readonly ErrorType NL_DESIGN_CANNOT_CREATE_PROJECT = new ErrorType("NL_DESIGN_CANNOT_CREATE_PROJECT", InnerEnum.NL_DESIGN_CANNOT_CREATE_PROJECT);
            public static readonly ErrorType NL_DESIGN_CANNOT_GET_STATUS = new ErrorType("NL_DESIGN_CANNOT_GET_STATUS", InnerEnum.NL_DESIGN_CANNOT_GET_STATUS);
            public static readonly ErrorType NL_DESIGN_CANNOT_GET_CONTAINS_USER_PATH = new ErrorType("NL_DESIGN_CANNOT_GET_CONTAINS_USER_PATH", InnerEnum.NL_DESIGN_CANNOT_GET_CONTAINS_USER_PATH);
			public static readonly ErrorType NL_DESIGN_CANNOT_GET_IS_PROJECT_OPEN = new ErrorType("NL_DESIGN_CANNOT_GET_IS_PROJECT_OPEN", InnerEnum.NL_DESIGN_CANNOT_GET_IS_PROJECT_OPEN);


            // RUNTIME
            public static readonly ErrorType NL_RUNTIME_ILLEGAL_STATE_FOR_OPERATION = new ErrorType("NL_RUNTIME_ILLEGAL_STATE_FOR_OPERATION", InnerEnum.NL_RUNTIME_ILLEGAL_STATE_FOR_OPERATION);
            public static readonly ErrorType NL_RUNTIME_CANNOT_GET_STATUS = new ErrorType("NL_RUNTIME_CANNOT_GET_STATUS", InnerEnum.NL_RUNTIME_CANNOT_GET_STATUS);
            public static readonly ErrorType NL_RUNTIME_CANNOT_GET_ADDED_VIRTUAL_USER = new ErrorType("NL_RUNTIME_CANNOT_GET_ADDED_VIRTUAL_USER", InnerEnum.NL_RUNTIME_CANNOT_GET_ADDED_VIRTUAL_USER);
            public static readonly ErrorType NL_RUNTIME_CANNOT_GET_STOPPED_VIRTUAL_USER = new ErrorType("NL_RUNTIME_CANNOT_GET_STOPPED_VIRTUAL_USER", InnerEnum.NL_RUNTIME_CANNOT_GET_STOPPED_VIRTUAL_USER);
            public static readonly ErrorType NL_RUNTIME_SANITY_CHECK_ERROR = new ErrorType("NL_RUNTIME_SANITY_CHECK_ERROR", InnerEnum.NL_RUNTIME_SANITY_CHECK_ERROR);


            private static readonly IList<ErrorType> valueList = new List<ErrorType>();

            static ErrorType()
            {
                valueList.Add(NL_API_ERROR);
                valueList.Add(NL_API_KEY_NOT_ALLOWED);
                valueList.Add(NL_API_ILLEGAL_SESSION);
                valueList.Add(NL_API_INVALID_ARGUMENT);

                valueList.Add(NL_RECORDING_NOT_LICENSED);
                valueList.Add(NL_DESIGN_ILLEGAL_STATE_FOR_OPERATION);
                valueList.Add(NL_DESIGN_CANNOT_GET_RECORDER_SETTINGS);
                valueList.Add(NL_DESIGN_CANNOT_GET_RECORDING_STATUS);
                valueList.Add(NL_DESIGN_UNKNOWN_USER_PATH);
                valueList.Add(NL_DESIGN_CANNOT_SAVE_PROJECT);
                valueList.Add(NL_DESIGN_CANNOT_SAVE_AS_PROJECT);
                valueList.Add(NL_DESIGN_CANNOT_CLOSE_PROJECT);
                valueList.Add(NL_DESIGN_CANNOT_OPEN_PROJECT);
                valueList.Add(NL_DESIGN_CANNOT_CREATE_PROJECT);
                valueList.Add(NL_DESIGN_CANNOT_GET_STATUS);
                valueList.Add(NL_DESIGN_CANNOT_GET_CONTAINS_USER_PATH);
				valueList.Add(NL_DESIGN_CANNOT_GET_IS_PROJECT_OPEN);

                valueList.Add(NL_RUNTIME_ILLEGAL_STATE_FOR_OPERATION);
                valueList.Add(NL_RUNTIME_CANNOT_GET_STATUS);
                valueList.Add(NL_RUNTIME_CANNOT_GET_ADDED_VIRTUAL_USER);
                valueList.Add(NL_RUNTIME_CANNOT_GET_STOPPED_VIRTUAL_USER);
                valueList.Add(NL_RUNTIME_SANITY_CHECK_ERROR);
            }

            public enum InnerEnum
            {
                NL_API_ERROR,
                NL_API_KEY_NOT_ALLOWED,
                NL_API_ILLEGAL_SESSION,
                NL_API_INVALID_ARGUMENT,

                NL_RECORDING_NOT_LICENSED,
                NL_DESIGN_ILLEGAL_STATE_FOR_OPERATION,
                NL_DESIGN_CANNOT_GET_RECORDER_SETTINGS,
                NL_DESIGN_CANNOT_GET_RECORDING_STATUS,
                NL_DESIGN_UNKNOWN_USER_PATH,
                NL_DESIGN_CANNOT_SAVE_PROJECT,
                NL_DESIGN_CANNOT_SAVE_AS_PROJECT,
                NL_DESIGN_CANNOT_CLOSE_PROJECT,
                NL_DESIGN_CANNOT_OPEN_PROJECT,
                NL_DESIGN_CANNOT_CREATE_PROJECT,
                NL_DESIGN_CANNOT_GET_STATUS,
                NL_DESIGN_CANNOT_GET_CONTAINS_USER_PATH,
				NL_DESIGN_CANNOT_GET_IS_PROJECT_OPEN,

                NL_RUNTIME_ILLEGAL_STATE_FOR_OPERATION,
                NL_RUNTIME_CANNOT_GET_STATUS,
                NL_RUNTIME_CANNOT_GET_ADDED_VIRTUAL_USER,
                NL_RUNTIME_CANNOT_GET_STOPPED_VIRTUAL_USER,
                NL_RUNTIME_SANITY_CHECK_ERROR
            }

            private readonly string nameValue;
            private readonly int ordinalValue;
            private readonly InnerEnum innerEnumValue;
            private static int nextOrdinal = 0;

            internal readonly string message;

            internal ErrorType(string name, InnerEnum innerEnum)
            {
                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;

                this.message = this.nameValue.Replace("_", "-");
            }

            public override string ToString()
            {
                return this.message;
            }

            internal static ErrorType fromString(string text)
            {
                if (text != null)
                {
                    // keep compatibility with 5.0.X errors messages
                    text = text.Replace("_", "-");

                    foreach (ErrorType errorType in ErrorType.values())
                    {
                        if (text.Equals(errorType.ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            return errorType;
                        }
                    }
                }
                return NL_API_ERROR;
            }

            public static IList<ErrorType> values()
            {
                return valueList;
            }

            public InnerEnum InnerEnumValue()
            {
                return innerEnumValue;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public static ErrorType valueOf(string name)
            {
                foreach (ErrorType enumInstance in ErrorType.values())
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new System.ArgumentException(name);
            }
        }

        private readonly ErrorType errorType;
        private readonly string details;
        private readonly Exception wrappedException;

        private const string BEGIN_DETAILS = "(";
        private const string END_DETAILS = ")";
        private const long serialVersionUID = 4303724564433950649L;

        /// <summary>
        /// Create a new NeotysAPIException based on an ErrorType, a details message, and an exception. </summary>
        /// <param name="errorType"> </param>
        /// <param name="details"> </param>
        /// <param name="wrappedException"> </param>
        /// <exception cref="NullPointerException"> if a parameter is null. </exception>
        public NeotysAPIException(ErrorType errorType, string details, Exception wrappedException)
        {
            this.errorType = Preconditions.CheckNotNull<ErrorType>(errorType);
            this.details = Preconditions.CheckNotNull<string>(details);
            this.wrappedException = Preconditions.CheckNotNull<Exception>(wrappedException);
        }

        /// <summary>
        /// Create a new NeotysAPIException based on an ErrorType and an exception. </summary>
        /// <param name="errorType"> </param>
        /// <param name="details"> </param>
        /// <param name="wrappedException"> </param>
        /// <exception cref="NullPointerException"> if a parameter is null. </exception>
        public NeotysAPIException(ErrorType errorType, Exception wrappedException)
        {
            this.errorType = Preconditions.CheckNotNull<ErrorType>(errorType);
            this.details = "";
            this.wrappedException = Preconditions.CheckNotNull<Exception>(wrappedException);
        }

        /// <summary>
        /// Create a new NeotysAPIException based on an ErrorType, and a details message. </summary>
        /// <param name="errorType"> </param>
        /// <param name="details"> </param>
        /// <exception cref="NullPointerException"> if a parameter is null. </exception>
        public NeotysAPIException(ErrorType errorType, string details)
        {
            this.errorType = Preconditions.CheckNotNull<ErrorType>(errorType);
            this.details = Preconditions.CheckNotNull<string>(details);
            this.wrappedException = null;
        }

        /// <summary>
        /// Create a new NeotysAPIException based on an ErrorType. </summary>
        /// <param name="errorType"> </param>
        /// <exception cref="NullPointerException"> if errorType is null. </exception>
        public NeotysAPIException(ErrorType errorType)
        {
            this.errorType = Preconditions.CheckNotNull<ErrorType>(errorType);
            this.details = "";
            this.wrappedException = null;
        }

        /// <summary>
        /// Create a new NeotysAPIException based on an exception. </summary>
        /// <param name="wrappedException"> </param>
        /// <exception cref="NullPointerException"> if wrappedException is null. </exception>
        public NeotysAPIException(Exception wrappedException)
        {
            this.errorType = ErrorType.NL_API_ERROR;
            this.details = "";
            this.wrappedException = Preconditions.CheckNotNull<Exception>(wrappedException);
        }

        /// <summary>
        /// Parse an error message to create a NeotysAPIException. </summary>
        /// <param name="errorMessage">
        /// @return </param>
        public static NeotysAPIException Parse(string errorMessage)
        {
            if (System.String.IsNullOrEmpty(errorMessage))
            {
                return new NeotysAPIException(ErrorType.NL_API_ERROR, "");
            }
            if (errorMessage.Contains(BEGIN_DETAILS) && errorMessage.Contains(END_DETAILS))
            {
                string strErrorType = errorMessage.Substring(0, errorMessage.IndexOf(BEGIN_DETAILS, StringComparison.Ordinal));
                ErrorType errorTypeLocal = ErrorType.fromString(strErrorType);
                int detailBegin = errorMessage.IndexOf(BEGIN_DETAILS) + 1;
                int detailLength = errorMessage.Length - 1 - detailBegin;
                string strErrorDetails = errorMessage.Substring(detailBegin, detailLength);
                return new NeotysAPIException(errorTypeLocal, strErrorDetails);
            }
            ErrorType errorType = ErrorType.fromString(errorMessage);
            return new NeotysAPIException(errorType, "");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Message);
            if (wrappedException != null)
            {
                sb.Append("---BEGIN Inner Exception---").Append(Environment.NewLine)
                  .Append(wrappedException.ToString()).Append(Environment.NewLine)
                  .Append("---END Inner Exception---");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Return the message.
        /// </summary>
        public override string Message
        {
            get
            {
                StringBuilder sb = new StringBuilder(this.errorType.ToString());
                if (!System.String.IsNullOrEmpty(details))
                {
                    sb.Append(" (").Append(details).Append(")");
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Return the exception.
        /// @return
        /// </summary>
        public virtual Exception WrappedException
        {
            get
            {
                return wrappedException;
            }
        }

        /// <summary>
        /// Return the error type.
        /// @return
        /// </summary>
        public virtual ErrorType getErrorType()
        {
            return errorType;
        }

        /// <summary>
        /// Return the details message.
        /// @return
        /// </summary>
        public virtual string Details
        {
            get
            {
                return details;
            }
        }
        public override int GetHashCode()
        {
            return new HashCodeBuilder<NeotysAPIException>(this)
                .With(m => m.errorType)
                .With(m => m.details)
                .With(m => m.wrappedException)
                .HashCode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NeotysAPIException))
            {
                return false;
            }

            NeotysAPIException exception = (NeotysAPIException)obj;

            return new EqualsBuilder<NeotysAPIException>(this, obj)
                .With(m => m.errorType)
                .With(m => m.details)
                .With(m => m.wrappedException)
                .Equals();
        }
    }

}