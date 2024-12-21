module Day2

let getReports (inputPath: string option) =
    let testInput="""7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9"""
    let lines = 
        match inputPath with
        | (Some p) ->
            if not (System.IO.File.Exists p) then
                printfn "Input path '%s' does not exist." p 
                System.Environment.Exit(1)
            System.IO.File.ReadAllLines p 
        | None -> testInput.Split("\n")

    let rec castToIntList (list: string list) = 
        match list with
        | [] -> []
        | head::tail -> int head::(castToIntList tail)

    let rec splitLevels (lines: string list) =
        match lines with
        | [] -> []
        | head::tail -> 
            let report = Array.toList (head.Split(" ")) 
            (castToIntList report) :: (splitLevels tail)

    splitLevels (Array.toList lines)

let rec reportIsSafe report= 
    match report with
    | first::second::third::tail -> // list of length 3 or more
        let diff = abs(first - second)
        if (diff >= 1 && diff <=3) &&
            ((first>second && second>third) // decreasing 
                || (first<second && second<third)) then // increasing
                reportIsSafe (second::third::tail)
        else
            false
        
    | first::second::[] -> // list of length 2
        let diff = abs(first - second)
        if diff >= 1 && diff <=3 then 
            true
        else
            false

    | [_] -> true // list of length 1
    | [] -> true // list of length 0



let dampenedReport reports = 
    let d = 
        [for i in 0..(List.length reports)-1 -> List.removeAt i reports]

    let rec testDampenedReport report=
        match report with 
        | [] -> false
        | head::tail -> 
            match reportIsSafe head with
            | false -> testDampenedReport tail
            | true -> true

    testDampenedReport d

let rec countSafeReports reports dampen =
    match reports with
    | [] -> 0
    | head::tail -> 
        let inc = 
            if dampen then
                match dampenedReport head with
                | false -> 0
                | true -> 1
            else
                match reportIsSafe head with
                | false -> 0
                | true -> 1
        inc + countSafeReports tail dampen

let main (test: bool) =
    let inputPath = 
        if not test then Some("inputs/day2-input")
        else None
    let reports = getReports inputPath

    printfn "Safe Reports Count: %d" (countSafeReports reports false)
    printfn "Semi-Safe Reports Count (Problem Dampener): %d" (countSafeReports reports true)



