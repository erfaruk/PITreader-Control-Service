using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWorker.Models.API
{
    public enum AuthenticationFailureReason
    {
        /// <summary>
        /// No error
        /// </summary>
        None = 0,

        /// <summary>
        /// No transponder key inserted
        /// </summary>
        NoTransponder = 1,

        /// <summary>
        /// Permission "0"
        /// The transponder key has no permissions for device groups.
        /// </summary>
        Permission_0 = 2,

        /// <summary>
        /// The validity of the transponder key is outside the validity period.
        /// (Start date/end date)
        /// </summary>
        TimeLimitation = 3,

        /// <summary>
        /// The transponder key is included in the block list.
        /// </summary>
        Blocklist = 4,

        /// <summary>
        /// No permission has been stored for the security-ID yet. ("External" authentication mode)
        /// </summary>
        ExternalModeWating = 5,

        /// <summary>
        /// Authentication is locked by the 24 V I/O port.
        /// </summary>
        IoPortLock = 6,

        /// <summary>
        /// The "Single authentication" authentication type is configured and authentication is locked by another registered transponder key.
        /// </summary>
        SingleAuthLock = 7,

        /// <summary>
        /// The "4 Eyes Principle" authentication type is configured and the second transponder key was not authenticated yes.
        /// </summary>
        FourEyesFirstKey = 8,

        /// <summary>
        /// Authentication mode "permission list" is used but the transponder is not part of the permission list on the device.
        /// </summary>
        NotInPermissionList = 9
    }
}
