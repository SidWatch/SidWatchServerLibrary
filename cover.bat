c:\projects\sidwatchserverlibrary\src\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe ^
	-register:user ^
	-output:c:\projects\sidwatchserverlibrary\sidwatchserverlibrary-coverage.xml ^
	"-filter:+[swLibrary]*  -[swLibrary]Sidwatch.Library.Properties.*" ^
	-excludebyattribute:"System.CodeDom.Compiler.GeneratedCodeAttribute" ^
	"-target:c:\projects\sidwatchserverlibrary\test.bat"