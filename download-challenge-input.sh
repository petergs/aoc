#!/bin/bash

cookie=$(cat .aoc-cookie)

if [[ $1 != "" ]]; then
    day=$1
else
    day=$(date +'%-d')
fi

curl -s "https://adventofcode.com/2024/day/$day/input" \
    --compressed -H "User-Agent: Mozilla/5.0 (X11; Linux x86_64; rv:133.0) Gecko/20100101 Firefox/133.0" \
    -H "Cookie: session=$cookie" > "inputs/day$day-input"
