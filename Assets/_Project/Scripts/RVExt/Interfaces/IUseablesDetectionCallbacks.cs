using System;

namespace RVExt
{
    /// <summary>
    /// Callbacks for useable-related events
    /// </summary>
    public interface IUseablesDetectionCallbacks
    {
        #region Properties

        /// <summary>
        /// Called when new IUseable was detected - one that wasn't detected in earlier scans
        /// </summary>
        Action<IUseable> OnNewUseableDetected { get; set; }

        /// <summary>
        /// Called when IUseable was visible in earlier scans, but is not anymore
        /// Note that passed in argument IUseable can be null - eg when it was destroyed/removed
        /// </summary>
        Action<IUseable> OnUseableNotSeenAnymore { get; set; }

        /// <summary>
        /// Called when IUseable was not visible(but not forgotten) and is visible again
        /// </summary>
        Action<IUseable> OnUseableVisibleAgain { get; set; }

        /// <summary>
        /// Called when IUseable hasn't been seen long enough to be removed from useables list
        /// /// Note that passed in argument IUseable can be null - eg when it was destroyed/removed
        /// </summary>
        Action<IUseable> OnUseableForget { get; set; }

        #endregion
    }
}