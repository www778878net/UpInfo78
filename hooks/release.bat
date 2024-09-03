@echo off
setlocal enabledelayedexpansion

echo Starting release process...

REM Change to the project root directory (one level up from the hooks directory)
cd /d "%~dp0\..\"

REM Get the current directory name as the project name
for %%I in (.) do set "project_name=%%~nxI"
echo Processing project: !project_name!

REM Set the project file path
set "project_file=src\!project_name!\!project_name!.csproj"

REM Check if the project file exists
if not exist "!project_file!" (
    echo Project file not found: !project_file!
    echo Exiting...
    goto :eof
)

echo Found project file: !project_file!

REM Fetch the latest changes from remote
git fetch origin

REM Update main branch
git checkout main
git pull origin main

REM Update develop branch
git checkout develop
git pull origin develop

REM Switch back to main branch
git checkout main

REM Get current version
for /f "tokens=3 delims=<>" %%a in ('findstr "<PackageVersion>" "!project_file!"') do set "current_version=%%a"
if not defined current_version (
    echo No PackageVersion found in !project_file!. Exiting...
    goto :eof
)
echo Current version: !current_version!

REM Build release version
echo Building release version...
dotnet build -c Release
if !errorlevel! neq 0 (
    echo Build failed. Exiting...
    goto :eof
)

REM Run tests
echo Running tests...
dotnet test
if !errorlevel! neq 0 (
    echo Tests failed. Exiting...
    goto :eof
)

REM If we get here, tests passed. Now we can update the version.

REM Increment patch version
for /f "tokens=1-3 delims=." %%a in ("!current_version!") do (
    set /a patch=%%c+1
    set "new_version=%%a.%%b.!patch!"
)
echo New version: !new_version!

REM Update version in project file
powershell -Command "(Get-Content '!project_file!') -replace '<PackageVersion>!current_version!</PackageVersion>', '<PackageVersion>!new_version!</PackageVersion>' | Set-Content '!project_file!'"

echo Version updated for !project_name! (!project_file!)

REM Stage the changed project file
git add "!project_file!"

REM Create an annotated tag with the changes
git tag -a v!new_version! -m "Bump version to !new_version!"

REM Push the tag to main (this includes the changes)
git push origin v!new_version!

REM Update main branch with the changes
git push origin main:main

REM Merge changes to develop
git checkout develop
git merge main
git push origin develop

REM Switch back to main
git checkout main

echo Release process completed. New version !new_version! has been tagged and pushed to main and develop branches.

endlocal