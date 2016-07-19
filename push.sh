#!/bin/sh
git add .
echo "Commit message: "
read -e desc
git commit -m "$desc"
git push origin head