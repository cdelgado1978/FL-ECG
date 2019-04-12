using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECG_DB
{
    public partial class paciente
    {
        public string FullName
        {
            get { return $"{nombre} {apellidos}"; }
        }
    }
}
