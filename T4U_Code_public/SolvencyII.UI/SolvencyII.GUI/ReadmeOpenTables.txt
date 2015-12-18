OpenTable selected
	SolvencyII.GUI.frmMain.CreateOpenUserControl
		OpenTemplateUserControl
			- Creates OpenTableColumnManager with TableVID
				- Builds column information (Setup)
					GetSQLiteData.GetTableAxisOrdinateColumns, lists ordinates
					GetSQLiteData.GetOrdinateDimensions, gets dims for each ordinate
					GetSQLiteData.GetOrdinateHierarchyID_MD, gets the HierarchyID is appropriate for an ordinate
					GetSQLiteData.GetOrdinateHierarchyID_HD
					GetSQLiteData.GetOrdinateType, works out the data time for the column
			OpenTemplateUserControl_Load
				SetupColumns, creates the columns in the virtual object list view
				SetupData, links the data source
					- Creates vDataSource 
						PrepareCache, Gets the data from the database
							GetSQLiteData.GetVirtualObjectItemCache
							GetSQLiteData.GetVirtualObjectItemData
							_colManager.GetColValues, takes the retrived valued and places them within a row's data
								Above sets row.ColValues and row.ColMembers from the data.
							_colManager.GetColDisplayLabels, takes the row's data and sets its appearance FORMATTING HERE

Editing events
SolvencyII.UI.Shared\Delegates\VirtualObjectViewDelegates.cs
	objectListView_CellEditStarting, pops the write control onto the top of the grid to edit the data FORMATTING HERE
	objectListView_OnCellEditValidating, validates the data
	objectListView_OnCellEditFinishing, initiates the save process
		- It makes sure the row contains the newest inform FORMATTING HERE
		- it calls the save routine from SolvencyII.GUI\UserControls\OpenTemplateUserControl.cs
			SaveRow
				PutSQLiteData.SaveOpenTableDataRow, saved the row to the database		
					OpenTableRowManager.BuildMetricXDimMem, building the metrics to save FORMATTING HERE
					OpenTableRowManager.BuildYDimMemZDimMem
					OpenTableRowManager.BuildYDimZDimMem
					Delete_dOpenTableSheetsRow
					Insert_dOpenTableSheetsRow
				PutSQLiteData.SaveDAvailabeTable, updates the support table


		