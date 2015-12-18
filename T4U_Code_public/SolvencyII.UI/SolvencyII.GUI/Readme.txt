Notes on SolvencyII POC Application


Overview

The application is made from a number of project each having a separate role.
They have been designed to allow implementation into other environments at a later stage with the least amount of disruption.


The Projects

SolvencyII.Data
This project provides all interaction with the SQLite database. It is the lowest common denominator so a DB change could be made only effecting this project. Similarly for a different envirnonment eg iOS key changes may be needed here that will not effect the remaining layers providing the interfaces remain consistant.

SolvencyII.Data.Shared
Again all data interaction does through this tier. There are two main classes than do the heavy lifting; GetSQLiteData and PutSQLiteData.
Along side are other classes that require direct access to SolvencyII.Data.

SolvencyII.Domain
Entities and functions that are required across the whole application are found here. This is needed for all active components except SolvencyII.Data and ObjectListView2010.

ObjectListView2010
The code for the virtual object list view is attached as a project. It is stand alone but there for completeness.

SolvencyII.POC
This is the UI for the POC. It is responsible for managing the tree diagram for selecting the appropriate templates, populating the combos for z axiss and maintaining the UserControls. (The functionality for the user controls is nested with ParentUserControls found in SolvencyII.UI.Shared.)

SolvencyII.UI.Shared
Where funcitonality can be simply removed from SolvencyII.GUI it is place here. The key difference between this one the the Domain project is that this one references System.Window.Forms. The significance of this is that code found here will be tied to the windows environment and will therfore require analysis to port to another platform.


Form Processes

When the main form is open the TreeView gets populated.

Saving data
When an item is selected from the tree view the frmMain is populated with the appropriate template and supporting object. 
In the case of open templates the grid is produced and shown on top. Below, the corresponding control of the same for as a closed template is also created with a ParentUserControl that allows editing of the individual rows. Essentially the mechanism for saving the row data is identical to that of a closed template.
When a closed template or the row template is seetup so to is the ParentUserControl. This ParentUserControl displays the save button. When it gets setup a delegate is give that is raised when the button is pressed. The event that fires is frmMain.SaveUserControlData.
SaveUserControlData in turn raises the ClosedTableManager.Save event.
This save event builds required information for the nPage fields and then passes it through to the control's event UpdateData. (It is also responsible to updating the filing indicator.)
The UpdateData event has been specifically created with known data types which use the appropriate instances of the GenericRepository to save the information.
The GenericRepository instances implement the BaseRepository that is db dependant to save the data.

Retrieving data
From the template backwards, the template is bound as a row on the data repeater. Its bindable property is UserData. The data repeater is on the class which inherits UserControlBase based upon ISolvencyUserControl. Its function SetupData is passed suitable queries which in turn are used via the GenericRepository. 
When the item is selected the event frmMain.SelectUserControlData is called creating the ClosedTableManager and raising the event Selected. Selected then sets up all nPage combos etc and raises PopulateDataForDataRepeater.  PopulateDataForDataRepeater builds the appropriate queries and send them through to the control's SetupData so that the GenericRepositories are populated correctly. PopulateDataForDataRepeater then runs the controls RefreshDR to refresh the data repeater.
