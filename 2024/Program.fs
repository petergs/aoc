module main

[<EntryPoint>]
let main args =

    let day = 3
    let test = false 
 
    match day with
    | 1 -> Day1.main ()
    | 2 -> Day2.main test
    | 3 -> Day3.main test
    | _ -> System.Environment.Exit 1

    0
