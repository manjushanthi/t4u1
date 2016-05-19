# T4U_SourceCode
##T4U Source Code
​
**​SQLite Designer**  
In order to work with SQLite and EntityFramework without any errors, you need to install `System.Data.SQLite` library with proper components according to the version of Visual Studio installed. Link to download http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki
	
##Excel generation
1. Open `ExcelAndControlGenerator` solution.
2. Release mode should be set to `Debug` and architecture to `x86`.
3. Set `ExcelGenCmd` project as startup.
4. In `ExcelGenCmd project/Properties/Debug` provide `Command line arguments` as follows:
`-d <<directory>> -v <<version info>> -i <<input database>> [-a or -m <<module1, module2, etc>>] -t ["BASIC"|"BUSINESS"|"ENUMERATION"]`
 
   **Where:**  
`-d`: Path of the dictionary where file to be generated.  
`-v`: Version number of the file.  
`-i`: Input database file.  
`-a`: Optional parameter to generate all modules.  
`-m`: Optional parameter to select particular modules. The modules are separated by comma.  
`-t`: Type of template BASIC or BUSINESS or ENUMERATION
 
5. Start application `ExcelGenCmd`.
 
##Templates and poco generation
1. Open XBRT container in T4U.
2. Open `ExcelAndControlGenerator` solution.
3. Release mode should be set to `Debug` and architecture to `x86`.
4. Set `ucGUI` project as startup.
5. Start application `ucGUI`.