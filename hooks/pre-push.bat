@echo off
setlocal enabledelayedexpansion

echo Pre-push hook is running...

REM Get current branch name
for /f "delims=" %%i in ('git rev-parse --abbrev-ref HEAD') do set "current_branch=%%i"

if "!current_branch!" == "develop" (
    echo Current branch is develop. Running pre-push checks...

    REM Add your original develop branch pre-push logic here
    REM For example, running tests or other checks

    echo Pre-push checks for develop branch completed.
) else if "!current_branch!" == "main" (
    echo Current branch is main. Merging changes to develop...

    REM Store the current commit hash
    for /f "delims=" %%i in ('git rev-parse HEAD') do set "current_commit=%%i"

    REM Switch to develop branch
    git checkout develop

    REM Pull latest changes from remote develop
    git pull origin develop

    REM Merge changes from main
    git merge !current_commit!

    echo Merged changes from main to develop. Please review and push manually if everything looks good.

   
) else (
    echo Current branch is !current_branch!. Skipping pre-push checks and merge.
)

endlocal