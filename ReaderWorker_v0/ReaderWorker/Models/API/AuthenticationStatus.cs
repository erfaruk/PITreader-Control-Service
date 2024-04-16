using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWorker.Models.API
{
    public enum AuthenticationStatus
    {
        /// <summary>
        /// No transponder key
        /// </summary>
        NoTransponder = 0,

        /// <summary>
        /// Process completed
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Waiting for external authentication
        /// </summary>
        Waiting = 2
    }
}
