using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Loggers;

namespace SolvencyII.UI.Shared.Data
{
    /// <summary>
    /// Data access for the virtual object list view.
    /// </summary>
    public class vDataSource3 : IVirtualListDataSource, IDisposable
    {

        public GenericDelegates.Response refreshList { get; set; }
        private void RefreshList()
        {
            if (refreshList != null)
                refreshList();
        }

        private List<OpenTableDataRow2> _objects = new List<OpenTableDataRow2>();
        private readonly string _tableName;

        private readonly long _instanceID;
        private readonly List<OpenColInfo2> _columns;
        private int _first, _last;
        private readonly Dictionary<string, string> _specifiedColumnsFromCombos;
        private Type _dataType;
        private int _totalRecordCount;

        private string sortField;
        private SortOrder sortOrder = SortOrder.Ascending;

        /// <summary>
        /// Secondary Cache - used to speed up localised operations
        /// </summary>
        private int _secondaryCacheSize = 1000;
        private int _secondaryPreFirstRowBuffer = 100;
        private List<OpenTableDataRow2> _secondaryCache = new List<OpenTableDataRow2>();
        private int _secondaryFirstRow;
        private int _secondaryLastRow;
        private List<ISolvencyPageControl> _specifiedColumnsNPage = new List<ISolvencyPageControl>();


        // Used to refresh the form
        public bool CacheItemError { get; set; }


        public void UpdateSecondaryCache()
        {
            //int found = _secondaryCache.FindIndex(r => r.PK_ID == CacheItem.PK_ID);
            //if (found != -1)
            //    _secondaryCache[found] = CacheItem;
            //else
            //{
            //    // We have not found this item so insert it
            //    _secondaryCache.Add(CacheItem);
            //    _objects.Add(CacheItem);
            //}
        }
        public void DeleteRowSecondaryCache(int pkID)
        {
            int found = _secondaryCache.FindIndex(r => r.PK_ID == pkID);
            if (found != -1)
                _secondaryCache.RemoveAt(found);
        }

        public vDataSource3(Type dataType, string tableName, long instanceID, List<OpenColInfo2> columns, Dictionary<string, string> specifiedColumnsFromCombos, List<ISolvencyPageControl> specifiedColumnsNPage)
        {
            _tableName = tableName;
            _instanceID = instanceID;
            _columns = columns;
            _specifiedColumnsFromCombos = specifiedColumnsFromCombos;
            _specifiedColumnsNPage = specifiedColumnsNPage;
            _dataType = dataType;

        }

        public object GetNthObject(int n)
        {
            try
            {
                // Debug.WriteLine(string.Format("GetNthObject {0}", n));
                if (_objects.Count != 0)
                {
                    if (n >= _first && n <= _last)
                    {
                        // We have the item in the cache
                        if (_objects.Count > (n - _first))
                        {
                            return CheckCacheItem(_objects[n - _first]);
                        }
                    }
                }

                Debug.WriteLine(string.Format("Missed the cache {0}, first {1} last {2} items in the cache {3}", n, _first, _last, _objects.Count()));

                if (n >= _secondaryFirstRow && n <= _secondaryLastRow)
                {
                    // Try looking in the secondary cache
                    if (_secondaryCache.Count > (n - _secondaryFirstRow))
                    {
                        //Debug.WriteLine(string.Format("Found in secondary cache {0}, first {1} last {2} items in the cache {3}", n, _secondaryFirstRow, _secondaryLastRow, _secondaryCache.Count()));
                        return CheckCacheItem(_secondaryCache[n - _secondaryFirstRow]);
                    }
                }

                var blankRowForAppend = new OpenTableDataRow2();
                blankRowForAppend.ColValues.AddRange(new List<string>(Enumerable.Repeat("", _columns.Count)));
                return blankRowForAppend;
            }
            catch (Exception ex)
            {
                string message = string.Format("{0}\nRequested element{1}, object count{2}, object first{3}, object last{4}, _secondaryCache.Count{5},  _secondaryFirstRow{6}, _secondaryLastRow{7}\n\n{8}", ex.Message, n, _objects.Count, _first, _last, _secondaryCache.Count, _secondaryFirstRow, _secondaryLastRow, ex.StackTrace);
                Logger.WriteLog(eSeverity.Error, "vDataSource2.GetNthObject", message);
                throw new Exception(message);
            }

        }

        /// <summary>
        /// If we have a CacheItem it will have been changed from the original so we need to ensure we use it 
        /// to update the display. Here we are checking the NthObject for its primary key.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private object CheckCacheItem(object obj)
        {
            //if (CacheItem != null)
            //{
            //    if (((OpenTableDataRow2)obj).PK_ID == CacheItem.PK_ID)
            //        return CacheItem;
            //}
            return obj;
        }

        public int GetObjectCount()
        {
            int result;
            using (GetSQLData getData = new GetSQLData())
            {
                result = getData.GetVirtualObjectItemCount2(_tableName, _specifiedColumnsFromCombos, _instanceID, _specifiedColumnsNPage);
            }

            // Temp variable used below
            _totalRecordCount = result;

            return result;

        }

        public int GetObjectIndex(object model)
        {
            OpenTableDataRow2 item = (OpenTableDataRow2)model;

            // The interface requires an int to be returned rather than a long:

            // Find position in _objects then add the start pos.
            int position = _objects.FindIndex(r => r == item);
            position += _first;
            return position;

        }

        public void PrepareCache(int first, int last)
        {
            try
            {
                if (_totalRecordCount == 0) _last = 1;

                // There is a MSbug that the ListView.CacheVirtualItems is sending the wrong info
                if (_first == 0 && _last == 0 && _totalRecordCount > 0)
                    last = 1;

                // A MSBug was pushing through the last as being 1.
                if (last < first) last = first + 1;



                _first = first;
                _last = last;

                // Check secondary cache for rows.
                if (_first < _secondaryFirstRow || _last > _secondaryLastRow)
                {
                    // Re-Query to build secondary cache since required rows not present.
                    _secondaryFirstRow = 0;
                    if (_first > _secondaryPreFirstRowBuffer)
                        _secondaryFirstRow = _first - _secondaryPreFirstRowBuffer;
                    _secondaryLastRow = _secondaryLastRow = _secondaryFirstRow + _secondaryCacheSize;
                    Debug.WriteLine(string.Format("Building secondary cache {0} to {1}", _secondaryFirstRow, _secondaryLastRow));
                    using (GetSQLData getData = new GetSQLData())
                    {
                        string orderby = sortField;
                        string sortorder = sortOrder == SortOrder.Descending ? "Desc" : "Asc";
                        _secondaryCache = getData.GetVirtualObjectItemCache2(_tableName, _dataType, _specifiedColumnsFromCombos, _secondaryFirstRow, _secondaryLastRow, _instanceID, _columns, _specifiedColumnsNPage, orderby, sortorder);
                        Debug.WriteLine(string.Format("Total objects count in secondary cache {0} First:{1} Last:{2}", _objects.Count(), _secondaryFirstRow, _secondaryLastRow));
                    }
                }

                // Secondary cache contains required records
                int startRow = first - _secondaryFirstRow;
                int numberOfRows = last - first;
                if(_secondaryCache.Count != 0)
                    _objects = _secondaryCache.GetRange(startRow, numberOfRows);
                else
                    _objects = new List<OpenTableDataRow2>();

                //if (last >= _totalRecordCount)
                //{
                //    var blankRowForAppend = new OpenTableDataRow2();
                //    blankRowForAppend.ColValues.AddRange(new List<string>(Enumerable.Repeat("", _columns.Count)));
                //    _objects.Add(blankRowForAppend); // Blank row for additions
                //}
                Debug.WriteLine(string.Format("Total objects count {0} First:{1} Last:{2}", _objects.Count(), first, last));

            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e);
            }
        }

        public int SearchText(string value, int first, int last, OLVColumn column)
        {
            if (first <= last)
            {
                for (int i = first; i <= last; i++)
                {
                    string data = column.GetStringValue(GetNthObject(i));
                    if (data.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }
            else
            {
                for (int i = first; i >= last; i--)
                {
                    string data = column.GetStringValue(GetNthObject(i));
                    if (data.StartsWith(value, StringComparison.CurrentCultureIgnoreCase))
                        return i;
                }
            }

            return -1;
        }

        public void Sort(OLVColumn column, SortOrder order)
        {
            // Only process is there is a change - else we can end up with an iteration
            if(sortOrder == order && sortField == column.AspectName) return;

            // Grad sort parameters
            sortOrder = order;
            sortField = column.AspectName;
            // Clear the cache.
            _first = 0;
            _last = 0;
            _objects.Clear();
            _secondaryFirstRow = 0;
            _secondaryLastRow = 0;
            _secondaryCache.Clear();

            // Refresh the grid.
            RefreshList();
        }

        public void RefreshCache()
        {
            using (GetSQLData getData = new GetSQLData())
            {
                string orderby = sortField;
                string sortorder = sortOrder == SortOrder.Descending ? "Desc" : "Asc";
                _secondaryCache.Clear();
                _secondaryCache = getData.GetVirtualObjectItemCache2(_tableName, _dataType, _specifiedColumnsFromCombos, _secondaryFirstRow, _secondaryLastRow, _instanceID, _columns, _specifiedColumnsNPage, orderby, sortorder);
            }

            
            PrepareCache(_first, _last);
        }

        #region Not implemented

        public void AddObjects(ICollection modelObjects)
        {
            throw new NotImplementedException();
        }

        public void RemoveObjects(ICollection modelObjects)
        {
            throw new NotImplementedException();
        }

        public void SetObjects(IEnumerable collection)
        {
            throw new NotImplementedException();
        }

        public void UpdateObject(int index, object modelObject)
        {
            throw new NotImplementedException();
            //if(_objects.Count - 1 <= index)
            //    _objects[index] = (OpenTableDataRow2)modelObject;
            //else
            //{
            //    _objects.Add((OpenTableDataRow2)modelObject);
            //}
        }

        #endregion

        public void Dispose()
        {
            _objects = null;

        }



    }
}
