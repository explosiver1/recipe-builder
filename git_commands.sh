#8/29/2024
#Jared Holston

#This document goes over the workflow for git.

#Git is a Version Control System (VCS).
#It manages concurrent work on a project by forcing users to adhere to the same "history".
#Whenever a commit is made, it adds to the history of the project in the .git folder files. 
#Changes can't be pushed and branches can't be merged if another users history doesn't match. 
#This is prevented by always pulling the most recent history before committing changes. 
#If pulling causes a merge conflict, it usually must be manually resolved.

#Joining an existing project
git clone [repository URL]  #Can be done using ssh or https authentication. This changes the URL you will use.
git checkout [-b] [branch name] #-b means you're making a new branch and not accessing an existing one.
    #Security rules are setup so that you have to make your own branch, and then request it to be merged into the main branch with a pull request.
    #This gives an opportunity for code to be reviewed before affecting the main branch. 
#Make changes you plan on making.
git add .   #Adds all files to git tracking that aren't in the .gitignore file. .gitignore uses patterns, so not every file needs to be listed.
git pull    #Pulls changes that have been made while you're working, and prevents broken pull requests.
#Fix merge conflicts if any. These happen when people have edited the same lines in the same files. 
    #Git will list the files with conflicts and add lines marking the 2 conflicting entries that must be remediated.
    #Replace the lines with the code you want to keep in the merge, then continue.
git add [merge conflicted files that were fixed]
git commit -m "[Message Text]"  
git push origin [branch]    #origin normally refers to the repository address. You can actually add additional remotes to represent different repo locations.
#You should see deltas processed if the push finds the repo and authenticates successfully.

#Now, you can make a pull request on the github website. There's probably a way to do it in code, but I'm not sure how yet.