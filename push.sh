#!/bin/sh
git add .
echo "Commit description: "
read -e desc
git commit -m "$desc"
git push origin head