using System;
using System.Collections.Generic;
using System.Text;
using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class PictureId : Value<PictureId>
    {
        public Guid Value { get; private set; }
        public PictureId(Guid value)
        {
            this.Value = value;
        }
        
    }
}
