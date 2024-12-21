#!/bin/bash

cookie=$(cat .aoc-cookie)

if [[ $1 != "" ]]; then
    day=$1
else
    day=$(date +'%-d')
fi

year="2024"

curl -s "https://adventofcode.com/$year/day/$day/input" \
    -H "User-Agent: Mozilla/5.0 (X11; Linux x86_64; rv:133.0) Gecko/20100101 Firefox/133.0" \
    -H "Cookie: session=$cookie" > "$year/inputs/day$day-input"
