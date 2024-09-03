@echo off
setlocal enabledelayedexpansion

echo Merging develop branch to main...

REM Switch to main branch and update
git checkout main
git pull origin main

REM Get the last commit message from develop branch
for /f "delims=" %%i in ('git log develop -1 --pretty^=%%s') do set "last_commit_msg=%%i"

REM Merge develop branch, using --squash option to compress all commits into one
git merge --squash develop

REM Commit with the last commit message
git commit -m "!last_commit_msg!"

REM Uncomment the following line if you want to automatically push to main
REM git push origin main

echo Merge completed. Please review the changes and push manually if everything looks good.

endlocal