/*
 * Copyright (c) 2017, Neotys
 * All rights reserved.
 */

using Neotys.CommonAPI.Utils;

namespace Neotys.CommonAPI.Data
{
    /// <summary>
	/// Binary data that contains file name and binary.
    /// 
    /// @author anouvel
    /// 
    /// </summary>
    public class BinaryData
    {
        private readonly string fileName;
        private readonly byte[] fileBinary;

        public BinaryData(string fileName, byte[] fileBinary)
        {
            this.fileName = fileName;
            this.fileBinary = fileBinary;
        }

        public virtual string FileName
        {
            get
            {
                return fileName;
            }
        }

        public virtual byte[] FileBinary
        {
            get
            {
                return fileBinary;
            }
        }

        public override string ToString()
        {
            return new ToStringBuilder<BinaryData>(this).ReflectionToString(this);
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder<BinaryData>(this)
                .With(m => m.fileName)
                .With(m => m.fileBinary)
                .HashCode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BinaryData))
            {
                return false;
            }

            return new EqualsBuilder<BinaryData>(this, obj)
                .With(m => m.fileName)
                .With(m => m.fileBinary)
                .Equals();
        }
    }
}
