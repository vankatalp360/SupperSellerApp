using System;
using System.ComponentModel.DataAnnotations;

namespace SuperSeller.Common.Admin.BindingModels
{
    public class BanUserBindingModel
    {
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}