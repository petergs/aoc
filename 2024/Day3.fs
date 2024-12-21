module Day3

open System.Text.RegularExpressions

let getInput (inputPath: string option) =
    let testInput =
        // Part 1 Input
        // "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
        // Part 2 Input
        "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"

    match inputPath with
    | (Some p) ->
        if not (System.IO.File.Exists p) then
            printfn "Input path '%s' does not exist." p
            System.Environment.Exit(1)

        Array.toList (System.IO.File.ReadAllLines p)
    | None -> Array.toList (testInput.Split "\n")

let parseMul (s: string) =
    let pair = s.Replace("mul(", "").Replace(")", "").Split(",")
    int pair[0] * int pair[1]

let extractMatches memory checkDo =
    let rec extractEnabledBlocks (input: string) (enabled: bool) : string =
        if enabled then
            let nextDont = input.IndexOf "don't()"

            if nextDont = -1 then
                input
            else
                input.[0..nextDont] + (extractEnabledBlocks input.[nextDont..] false)

        else
            let nextDo = input.IndexOf "do()"

            if nextDo = -1 then
                ""
            else
                extractEnabledBlocks input.[nextDo..] true

    let input =
        let concat = String.concat "" memory
        if checkDo then extractEnabledBlocks concat true else concat

    let pattern = @"(mul\(\d{1,3},\d{1,3}\))"
    let regex = new Regex(pattern)
    let matches = regex.Matches input

    matches |> Seq.map (fun x -> parseMul (string x)) |> Seq.sum


let main test =
    let inputPath = if not test then Some("inputs/day3-input") else None

    printfn "Part 1: %d" (extractMatches (getInput inputPath) false)
    printfn "Part 2: %d" (extractMatches (getInput inputPath) true)
