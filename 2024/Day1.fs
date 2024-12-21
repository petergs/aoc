module Day1

type EqualLengthLists =
    private
    | EqualLengthLists of int list * int list

    static member Create(list1: int list, list2: int list) =
        if List.length list1 = List.length list2 then
            Some(EqualLengthLists(list1, list2))
        else
            None

let distance (EqualLengthLists(list1, list2)) =
    let rec d l1 l2 =
        match l1, l2 with
        | head1 :: tail1, head2 :: tail2 -> abs (head1 - head2) + d tail1 tail2
        | [], [] -> 0
        | _, _ -> failwith "Unexpected case. Program should never reach here if lists are equal length." // exhaustive

    d (List.sort list1) (List.sort list2)

let similarity (list1, list2) =
    let list1 = List.sort list1
    let list2 = List.sort list2

    let rec countInstances l i =
        match l with
        | head :: tail ->
            if i > head then 0 + countInstances tail i
            elif i < head then 0 // assuming sorted
            else 1 + countInstances tail i
        | [] -> 0

    let rec getSimilarity l =
        match l with
        | head :: tail -> (head * (countInstances list2 head)) + getSimilarity tail
        | [] -> 0

    getSimilarity (list1)


let getArraysFromInput (lines: string list) =
    let mutable leftList = []
    let mutable rightList = []

    for line in lines do
        let cols = line.Split("   ")
        leftList <- int cols[0] :: leftList
        rightList <- int cols[1] :: rightList


    leftList, rightList

let getInput (inputPath: string option) =
    let testFile =
        """3   4
4   3
2   5
1   3
3   9
3   3"""

    let lines =
        match inputPath with
        | (Some p) ->
            if not (System.IO.File.Exists p) then
                printfn "Input path '%s' does not exist." p
                System.Environment.Exit(1)

            System.IO.File.ReadAllLines p
        | None -> testFile.Split("\n")

    getArraysFromInput (Array.toList lines)




let main () =
    let test: bool = false
    let inputPath = if not test then Some("inputs/day1-input") else None
    let list1, list2 = (getInput inputPath)
    let ls = EqualLengthLists.Create(list1, list2)

    // output challenges
    // calculate distance
    match ls with
    | Some lists -> printfn "Distance: %d" (distance lists)
    | None -> printfn "Lists must be of equal length"

    // calculate similarity
    printfn "Similarity: %d" (similarity (list1, list2))
