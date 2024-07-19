using System;

namespace MicroBase.Share.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomDataSetAttribute : Attribute
    {
        public CustomDataSetAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}