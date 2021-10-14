using System.Collections.Generic;

namespace RVExt
{
    public interface IUseableInfosProvider
    {
        #region Properties

        /// <summary>
        /// List of current useable info (useables we know about, not necessarily see them)
        /// </summary>
        List<UseableInfo> UseableInfos { get; }

        /// <summary>
        /// Dictionary of current useable info (useables we know about, not necessarily see them)
        /// </summary>
        Dictionary<IUseable, UseableInfo> UseableInfosDict { get; }

        #endregion
    }
}