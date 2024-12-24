module main

open System

let parseArguments args =
    let mutable day = None
    let mutable test = false

    let rec parse (args: string list) =
        match args with
        | "--test" :: rest ->
            test <- true
            parse rest
        | dayValue :: rest when List.contains dayValue [ for i in 1..25 -> string i ] ->
            day <- Some(Int32.Parse(dayValue))
            parse rest
        | [] -> ()
        | _ ->
            printfn "Usage: aoc2024 <day> [--test]"
            Environment.Exit(1)

    parse args

    match day with
    | Some d -> d, test
    | None ->
        printfn "The 'day' argument is required and is expected to be one of 1..25. Usage: aoc2024 <day> [--test]"
        Environment.Exit(1)
        0, false // This will never be reached

[<EntryPoint>]
let main argv =

    let args = argv |> Array.toList

    let day, test = parseArguments args

    match day with
    | 1 -> Day1.main ()
    | 2 -> Day2.main test
    | 3 -> Day3.main test
    | _ -> System.Environment.Exit 1

    0 // Return exit code
